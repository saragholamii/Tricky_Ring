using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LeaderboardManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Data / Pool")]
    public int totalRows = 5000;
    public int visibleRows = 15;
    public GameObject rowPrefab;         // prefab with LeaderboardRow component
    public RectTransform maskParent;     // the viewport / mask parent (optional for calculation)

    [Header("Drag & Momentum")]
    public float dragSpeed = 1.0f;       // finger/mouse sensitivity
    public float inertiaDeceleration = 6f; // bigger = quicker stop
    public float velocityDamp = 10f;     // smoothing when dragging

    // runtime
    private RectTransform rtContent;
    List<LeaderboardEntryData> entries = new List<LeaderboardEntryData>();
    List<RectTransform> rowPool = new List<RectTransform>();
    List<int> rowDataIndex = new List<int>(); // data index for each pooled row

    float rowHeight;
    bool dragging = false;
    float velocity = 0f; // pixels per second (positive = upward)
    Vector2 lastDragPosition;

    // thresholds for wrapping (center-based)
    float topThreshold;    // if center Y > topThreshold -> wrap to bottom
    float bottomThreshold; // if center Y < bottomThreshold -> wrap to top

    private void Awake()
    {
        rtContent = GetComponent<RectTransform>();
    }

    void Start()
    {
        if (rowPrefab == null) { Debug.LogError("Row prefab not assigned."); enabled = false; return; }

        // generate fake data
        GenerateFakeData();

        // measure row height from prefab's RectTransform
        var prt = rowPrefab.GetComponent<RectTransform>();
        rowHeight = prt.rect.height;

        // compute thresholds: half-row outside top/bottom
        topThreshold = rowHeight * 0.5f;
        bottomThreshold = -rowHeight * (visibleRows - 0.5f);

        // ensure content size (height) is equal to visibleRows * rowHeight
        Vector2 size = rtContent.sizeDelta;
        size.y = visibleRows * rowHeight;
        rtContent.sizeDelta = size;

        CreateRowPool();
    }
    
    void GenerateFakeData()
    {
        for (int i = 0; i < totalRows; i++)
        {
            var ds = Random.Range(0, 100000);
            var ws = Random.Range(ds, 100000);
            var ats = Random.Range(ws, 100000);
            
            entries.Add(new LeaderboardEntryData
            {
                name = "Player" + (i + 1),
                dailyScore = ds,
                weeklyScore = ws,
                allTimeScore = ats
            });
        }
        
        entries.Add(GenerateLocalPlayerLeaderboardEntry());
        totalRows = entries.Count;
        
        SortByDailyScore();
    }

    private LeaderboardEntryData GenerateLocalPlayerLeaderboardEntry()
    {
        return new LeaderboardEntryData
        {
            name = PlayerPrefs.GetString(PlayerPrefsDictionary.PlayerName, "Sara"),
            dailyScore = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerDailyScore, 0),
            weeklyScore = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerWeeklyScore, 0),
            allTimeScore = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerHighScore, 0)
        };
    }
    
    public void CreateRowPool()
    {
        rowPool.Clear();
        rowDataIndex.Clear();

        if (rtContent.childCount == 0)
        {
            for (int i = 0; i < visibleRows; i++)
            {
                Instantiate(rowPrefab, rtContent, false);
            }
        }
        
        // create rows and position them top-down
        for (int i = 0; i < visibleRows; i++)
        {
            var go = rtContent.GetChild(i).gameObject;
            RectTransform r = go.GetComponent<RectTransform>();

            // top anchored layout => y = -i * rowHeight
            r.anchoredPosition = new Vector2(0f, -i * rowHeight);

            // store
            rowPool.Add(r);

            int dataIndex = i % totalRows;
            rowDataIndex.Add(dataIndex);

            // set initial data
            var rowComp = go.GetComponent<LeaderboardRow>();
            if (rowComp != null)
                rowComp.SetData(dataIndex + 1, entries[dataIndex].name, entries[dataIndex].dailyScore);
            
            //check to change card UI
            if (entries[dataIndex].name == (PlayerPrefs.GetString(PlayerPrefsDictionary.PlayerName, "Sara")))
            {
                r.gameObject.GetComponent<LeaderboardRow>().ChangeColorForLocalPlayer();
            }
            else
            {
                r.gameObject.GetComponent<LeaderboardRow>().ChangeColorForOtherPlayer();
            }
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;

        // apply inertia when not dragging
        if (!dragging)
        {
            if (Mathf.Abs(velocity) > 0.01f)
            {
                // move by velocity * dt
                float move = velocity * dt;
                MoveRowsBy(move);
                // decay velocity
                velocity = Mathf.MoveTowards(velocity, 0f, inertiaDeceleration * dt * Mathf.Sign(velocity));
            }
        }

        // handle wrapping checks each frame (also occurs during drag since MoveRowsBy does checks)
    }

    // Moves all pooled rows by delta pixels (positive => move up)
    void MoveRowsBy(float deltaY)
    {
        // move each row's anchoredPosition
        for (int i = 0; i < rowPool.Count; i++)
        {
            var r = rowPool[i];
            r.anchoredPosition += new Vector2(0f, deltaY);
        }

        // check wrapping (rows that left top or bottom)
        for (int i = 0; i < rowPool.Count; i++)
        {
            var r = rowPool[i];
            int oldIndex = rowDataIndex[i];

            // went above top (center moved above topThreshold)
            int newIndex = 0;
            if (r.anchoredPosition.y > topThreshold)
            {
                // move down by visibleRows * rowHeight
                r.anchoredPosition -= new Vector2(0f, visibleRows * rowHeight);

                newIndex = (oldIndex + visibleRows) % totalRows;
                rowDataIndex[i] = newIndex;

                var comp = r.GetComponent<LeaderboardRow>();
                if (comp != null)
                    comp.SetData(newIndex + 1, entries[newIndex].name, entries[newIndex].dailyScore);
            }
            // went below bottom (center moved below bottomThreshold)
            else if (r.anchoredPosition.y < bottomThreshold)
            {
                // move up by visibleRows * rowHeight
                r.anchoredPosition += new Vector2(0f, visibleRows * rowHeight);

                newIndex = (oldIndex - visibleRows) % totalRows;
                if (newIndex < 0) newIndex += totalRows;
                rowDataIndex[i] = newIndex;

                var comp = r.GetComponent<LeaderboardRow>();
                if (comp != null)
                    comp.SetData(newIndex + 1, entries[newIndex].name, entries[newIndex].dailyScore);
            }
            
            //check to change card UI
            if (entries[newIndex].name == "Sara")
            {
                r.gameObject.GetComponent<LeaderboardRow>().ChangeColorForLocalPlayer();
            }
            else
            {
                r.gameObject.GetComponent<LeaderboardRow>().ChangeColorForOtherPlayer();
            }
        }
    }

    
    // ---- Drag interfaces ----

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        velocity = 0f;
        lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate drag delta in screen pixels
        Vector2 cur = eventData.position;
        Vector2 delta = cur - lastDragPosition;
        lastDragPosition = cur;

        // Convert delta to content movement (UI y is up)
        float move = delta.y * dragSpeed;

        // Smooth velocity while dragging
        float targetVel = move / Time.deltaTime;
        velocity = Mathf.Lerp(velocity, targetVel, Mathf.Clamp01(velocityDamp * Time.deltaTime));

        MoveRowsBy(move);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        // velocity already set during last OnDrag; inertia will apply in Update
    }
    
    

    public void SortByDailyScore()
    {
        entries.Sort((a, b) => b.dailyScore.CompareTo(a.dailyScore));
    }

    public void SortByWeeklyScore()
    {
        entries.Sort((a, b) => b.weeklyScore.CompareTo(a.weeklyScore));
    }

    public void SortByAllTimeScore()
    {
        entries.Sort((a, b) => b.allTimeScore.CompareTo(a.allTimeScore));
    }
    
#if UNITY_EDITOR
    // draw helpful gizmos in editor for thresholds (optional)
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying && rowPrefab != null && rtContent != null)
        {
            Gizmos.color = Color.cyan;
            Vector3 top = rtContent.TransformPoint(new Vector3(0f, topThreshold, 0f));
            Vector3 bot = rtContent.TransformPoint(new Vector3(0f, bottomThreshold, 0f));
            Gizmos.DrawLine(top + Vector3.left * 1000f, top + Vector3.right * 1000f);
            Gizmos.DrawLine(bot + Vector3.left * 1000f, bot + Vector3.right * 1000f);
        }
    }
#endif


}
