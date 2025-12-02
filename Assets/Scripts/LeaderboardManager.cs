using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard Row Data")]
    public int visibleCount = 15;
    public List<GameObject> rowPool = new List<GameObject>();
    public GameObject rowPrefab;

    
    [Header("Scroll View")]
    public ScrollRect scrollRect;
    public RectTransform content;
    float rowHeight;
    int totalRows;
    
    [Header("Leaderboard Entries")]
    public List<LeaderboardEntryData> entries = new List<LeaderboardEntryData>();
    
    void Start()
    {
        GenerateFakeData();
        CreateUIRowPool();

        rowHeight = rowPrefab.GetComponent<RectTransform>().sizeDelta.y;
        totalRows = entries.Count;

        float contentHeight = visibleCount * rowHeight;
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);

        scrollRect.onValueChanged.AddListener(OnScroll);
        OnScroll(Vector2.zero);
    }

    void GenerateFakeData()
    {
        for (int i = 1; i <= 5000; i++)
        {
            var ds = Random.Range(0, 100000);
            var ws = Random.Range(ds, 100000);
            var ats = Random.Range(ws, 100000);
            entries.Add(new LeaderboardEntryData
            {
                name = "Player" + i,
                dailyScore = ds,
                weeklyScore = ws,
                allTimeScore = ats,
            });
        }

        SortByDailyScore();
    }

    void CreateUIRowPool()
    {
        for (int i = 0; i < visibleCount; i++)
        {
            GameObject row = Instantiate(rowPrefab, content);
            rowPool.Add(row);
        }
    }

    void OnScroll(Vector2 pos)
    {
        float scrollY = content.anchoredPosition.y;
        int firstVisibleIndex = Mathf.FloorToInt(scrollY / rowHeight);

        for (int i = 0; i < rowPool.Count; i++)
        {
            int dataIndex = firstVisibleIndex + i;
            if (dataIndex >= 0 && dataIndex < entries.Count)
            {
                var rowData = entries[dataIndex];
                var row = rowPool[i];

                row.GetComponent<LeaderboardRow>().SetData(
                    dataIndex + 1,
                    rowData.name,
                    rowData.dailyScore
                );

                row.SetActive(true);
            }
            else
            {
                rowPool[i].SetActive(false);
            }
        }
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
}
