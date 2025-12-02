using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuMovementManager : MonoBehaviour
{
    public Transform panelParent;
    public RectTransform canvas;
    public Transform highlight;
    
    public Transform shopButton;
    public Transform startButton;
    public Transform leaderboardButton;

    //shop = 0; start = 1; leaderboard = 2;
    private int panelsParentState = 1;
    
    public void OnShopButtonClicked()
    {
        if(panelsParentState == 0)
            return;
        
        Vector3 newPos = Vector3.zero;
        switch (panelsParentState)
        {
            case 1:
                newPos = new Vector3((panelParent.position.x + canvas.rect.width * canvas.localScale.x) , panelParent.position.y, panelParent.position.z);
                break;
            case 2:
                newPos = new Vector3(panelParent.position.x + canvas.rect.width * canvas.localScale.x * 2, panelParent.position.y, panelParent.position.z);
                break;
        }

        ChangePosition(panelParent, newPos);
        panelsParentState = 0;
        ChangePosition(highlight, shopButton.position);
    }

    public void OnStartButtonClicked()
    {
        if(panelsParentState == 1)
            return;
        
        Vector3 newPos = Vector3.zero;
        switch (panelsParentState)
        {
            case 0:
                newPos = new Vector3(panelParent.position.x - canvas.rect.width * canvas.localScale.x, panelParent.position.y, panelParent.position.z);
                break;
            case 2:
                newPos = new Vector3(panelParent.position.x + canvas.rect.width * canvas.localScale.x, panelParent.position.y, panelParent.position.z);
                break;
        }

        ChangePosition(panelParent, newPos);
        panelsParentState = 1;
        ChangePosition(highlight, startButton.position);
    }

    public void OnLeaderboardButtonClicked()
    {
        if(panelsParentState == 2)
            return;
        
        Vector3 newPos = Vector3.zero;
        switch (panelsParentState)
        {
            case 0:
                newPos = new Vector3(panelParent.position.x - canvas.rect.width * canvas.localScale.x * 2, panelParent.position.y, panelParent.position.z);
                break;
            case 1:
                newPos = new Vector3(panelParent.position.x - canvas.rect.width * canvas.localScale.x, panelParent.position.y, panelParent.position.z);
                break;
        }
        
        ChangePosition(panelParent, newPos);
        panelsParentState = 2;
        ChangePosition(highlight, leaderboardButton.position);
    }

    private void ChangePosition(Transform objectTransform, Vector3 position)
    {
        objectTransform.transform.DOMove(position, 0.75f)
            .SetEase(Ease.OutQuint);
    }
}
