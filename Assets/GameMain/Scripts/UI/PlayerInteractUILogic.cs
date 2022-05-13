using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using DG.Tweening;
using GameFramework.Event;
using GameFramework.DataNode;
using System;

public class PlayerInteractUILogic : UIFormLogic
{
    private Text powerText;
    private Text actionText;
    private Text deskCount;
    private Text graveCount;

    private GameObject yourTurnTitle;
    private GameObject enemyTurnTitle;

    private Button endTurnBtn;

    private ActionData m_actionData;

    private CardGather m_deskCards;
    private CardGather m_handCards;
    private CardGather m_graveCards;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Init();
    }

    private void Init()
    {
        powerText = transform.Find("PowerPanel").Find("PowerText").GetComponent<Text>();
        actionText = transform.Find("PowerPanel").Find("ActionText").GetComponent<Text>();
        deskCount = transform.Find("PowerPanel").Find("DeskCount").GetComponent<Text>();
        graveCount = transform.Find("GravePanel").Find("GraveCount").GetComponent<Text>();

        yourTurnTitle = transform.Find("MenuPanel").Find("YourTurn").gameObject;
        enemyTurnTitle = transform.Find("MenuPanel").Find("EnemyTurn").gameObject;
        yourTurnTitle.SetActive(true);
        enemyTurnTitle.SetActive(false);

        endTurnBtn = transform.Find("MenuPanel").Find("EndTurnBtn").GetComponent<Button>();
        endTurnBtn.onClick.AddListener(() =>
        {
            GameEntry.Event.Fire(this, TurnEnd.Create());
        });
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        GameEntry.Event.Subscribe(ActorSelectedEvent.EventId, OnActorSelected);
        //GameEntry.Event.Subscribe(GameOverEvent.EventId, OnGameOver);

        m_actionData = GameEntry.DataNode.GetNode(Definition.Node.ActionDataNode).GetData<VarActionData>().Value;
        IDataNode PlayerCardsNode = GameEntry.DataNode.GetOrAddNode(Definition.Node.PlayerCardsNode);
        m_deskCards = PlayerCardsNode.GetOrAddChild(Definition.Node.DeskCardsNode).GetData<VarCardGather>().Value;
        m_handCards = PlayerCardsNode.GetOrAddChild(Definition.Node.HandCardsNode).GetData<VarCardGather>().Value;
        m_graveCards = PlayerCardsNode.GetOrAddChild(Definition.Node.GraveCardsNode).GetData<VarCardGather>().Value;
    }

    private void OnActorSelected(object sender, GameEventArgs e)
    {
        if(GameEntry.GameManager.isPlayerTurn)
        {
            yourTurnTitle.SetActive(true);
            enemyTurnTitle.SetActive(false);
        }
        else
        {
            yourTurnTitle.SetActive(false);
            enemyTurnTitle.SetActive(true);
        }
    }

    protected override void OnClose(bool isShutdown, object userData)                                                                                                                       
    {
        base.OnClose(isShutdown, userData);
        //GameEntry.Event.Unsubscribe(GameOverEvent.EventId, OnGameOver);
        GameEntry.Event.Unsubscribe(ActorSelectedEvent.EventId, OnActorSelected);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        powerText.text = m_actionData.curPower + "/" + m_actionData.powerMax;
        string actStr = "";
        for (int i = 0; i < m_actionData.actList.Count; i++)
        {
            actStr += m_actionData.actList[i].ToString() + " ";
        }
        actionText.text = actStr;
        deskCount.text = string.Format("Ê£Óà¿¨ÅÆÊý£º{0}", m_deskCards.CardCount);
        graveCount.text = string.Format("¿¨ÅÆÊý£º{0}", m_graveCards.CardCount);
    }
}
