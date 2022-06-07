using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.UI;
using UnityGameFramework.Runtime;
using UnityEngine.UI;

public class GameOverUILogic : UIFormLogic
{
    private Button restartBtn;
    private Button nextStoreyBtn;
    private GameObject youWinTitle;
    private GameObject youLoseTitle;

    private StoreyData curStorey;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        youWinTitle = transform.Find("YouWin").gameObject;
        youLoseTitle = transform.Find("YouLose").gameObject;

        youWinTitle.SetActive(false);
        youLoseTitle.SetActive(false);

        restartBtn = transform.Find("Menu").Find("RestartBtn").GetComponent<Button>();

        restartBtn.onClick.AddListener(() =>
        {
            StoreyData newStorey = new StoreyData(1);
            GameEntry.DataNode.SetData<VarStoreyData>(Definition.Node.StoreyNode, newStorey);
            GameEntry.Event.Fire(this, RestartEvent.Create());
        });

        nextStoreyBtn = transform.Find("Menu").Find("NextStoreyBtn").GetComponent<Button>();
        nextStoreyBtn.onClick.AddListener(() =>
        {
            if (!curStorey.IsTop)
            {
                StoreyData newStorey = new StoreyData(curStorey.typeId + 1);
                GameEntry.DataNode.SetData<VarStoreyData>(Definition.Node.StoreyNode, newStorey);
                GameEntry.Event.Fire(this, RestartEvent.Create());
            }
        });
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        bool isWin = (bool)userData;
        youWinTitle.SetActive(isWin);
        youLoseTitle.SetActive(!isWin);

        curStorey = GameEntry.DataNode.GetData<VarStoreyData>(Definition.Node.StoreyNode);
        nextStoreyBtn.interactable = !curStorey.IsTop;
    }   
}
