using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using DG.Tweening;
using GameFramework.Event;
using GameFramework.DataNode;
using System;

public class InteractUILogic : UIFormLogic
{
    private Text powerText;
    private Text actionText;
    private Text deskCount;
    private Text graveCount;

    private GameObject yourTurnTitle;
    private GameObject enemyTurnTitle;

    private Button endTurnBtn;

    private ActionData m_actionData;

    private GameObject[] points;
    private GameObject arrow;

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

        Transform pointer = transform.Find("Pointer");
        points = new GameObject[10]
        {
            pointer.Find("Point1").gameObject,
            pointer.Find("Point2").gameObject,
            pointer.Find("Point3").gameObject,
            pointer.Find("Point4").gameObject,
            pointer.Find("Point5").gameObject,
            pointer.Find("Point6").gameObject,
            pointer.Find("Point7").gameObject,
            pointer.Find("Point8").gameObject,
            pointer.Find("Point9").gameObject,
            pointer.Find("Point10").gameObject,
        };
        arrow = pointer.Find("Arrow").gameObject;

        arrow.SetActive(false);
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
        }
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        GameEntry.Event.Subscribe(ActorSelectedEvent.EventId, OnActorSelected);

        GameEntry.Event.Subscribe(MouseDownCard.EventId, OnMouseDownCard);
        GameEntry.Event.Subscribe(MouseUpCard.EventId, OnMouseUpCard);
        GameEntry.Event.Subscribe(MouseDragCard.EventId, OnDragCard);
        //GameEntry.Event.Subscribe(GameOverEvent.EventId, OnGameOver);

        m_actionData = GameEntry.DataNode.GetNode(Definition.Node.ActionDataNode).GetData<VarActionData>().Value;
        IDataNode PlayerCardsNode = GameEntry.DataNode.GetOrAddNode(Definition.Node.PlayerCardsNode);
        m_deskCards = PlayerCardsNode.GetOrAddChild(Definition.Node.DeskCardsNode).GetData<VarCardGather>().Value;
        m_handCards = PlayerCardsNode.GetOrAddChild(Definition.Node.HandCardsNode).GetData<VarCardGather>().Value;
        m_graveCards = PlayerCardsNode.GetOrAddChild(Definition.Node.GraveCardsNode).GetData<VarCardGather>().Value;
    }

    protected override void OnClose(bool isShutdown, object userData)                                                                                                                       
    {
        base.OnClose(isShutdown, userData);
        //GameEntry.Event.Unsubscribe(GameOverEvent.EventId, OnGameOver);
        GameEntry.Event.Unsubscribe(ActorSelectedEvent.EventId, OnActorSelected);
        GameEntry.Event.Unsubscribe(MouseDownCard.EventId, OnMouseDownCard);
        GameEntry.Event.Unsubscribe(MouseUpCard.EventId, OnMouseUpCard);
        GameEntry.Event.Unsubscribe(MouseDragCard.EventId, OnDragCard);
    }

    private void OnMouseUpCard(object sender, GameEventArgs e)
    {
        Vector3 defaultPos = Camera.main.WorldToScreenPoint(new Vector3(100, 100, 0));
        arrow.transform.position = defaultPos;
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = defaultPos;
        }
        arrow.SetActive(false);
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
        }
    }

    private void OnMouseDownCard(object sender, GameEventArgs e)
    {
        arrow.SetActive(true);
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(true);
        }
    }

    private void OnDragCard(object sender, GameEventArgs e)
    {
        GameObject card = sender as GameObject;
        Vector3 mousePos = Input.mousePosition;
        Vector3 cardPosInUI = Camera.main.WorldToScreenPoint(card.transform.position);
        arrow.transform.position = mousePos;
        arrow.transform.localScale = mousePos.x >= cardPosInUI.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = quardaticBezier(cardPosInUI, mousePos, i * 0.1f);
        }
    }

    public Vector3 quardaticBezier(Vector3 from, Vector3 to, float t)
    {
        Vector3 middle = new Vector3(from.x, to.y, 0);
        Vector3 line1 = from + (middle - from) * t;
        Vector3 line2 = middle + (to - middle) * t;
        return line1 + (line2 - line1) * t;
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
