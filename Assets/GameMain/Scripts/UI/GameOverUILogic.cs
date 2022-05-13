using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.UI;
using UnityGameFramework.Runtime;
using UnityEngine.UI;

public class GameOverUILogic : UIFormLogic
{
    private Button restartBtn;
    private GameObject youWinTitle;
    private GameObject youLoseTitle;

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
            GameEntry.Event.Fire(this, RestartEvent.Create());
        });
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        bool isWin = (bool)userData;
        youWinTitle.SetActive(isWin);
        youLoseTitle.SetActive(!isWin);
    }

    
}
