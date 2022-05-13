using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Entity;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardLogicBase : EntityLogic
{
    protected Text cardName;
    protected Text cardCost;
    protected Text cardDetail;
    protected Text cardType;

    public RoleData roleData { get; private set; }
    public MonsterData monsterData { get; private set; }
    public ActionData actionData { get; private set; }

    public Definition.Enum.CardType m_CardType = Definition.Enum.CardType.Unknown;

    public int m_CardCost = 0;

    public Definition.Enum.CardState m_CardState = Definition.Enum.CardState.Unknown;


    private void OnMouseEnter() => OnMouseEnterCard();
    private void OnMouseDown() => OnMouseDownCard();
    private void OnMouseExit() => OnMouseExitCard();
    private void OnMouseDrag() => OnMouseDownCard();

    protected UnityAction<object> BeforeTakeEffect;
    protected UnityAction<object> TakeEffect;
    protected UnityAction<object> AfterTakeEffect;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        Transform cardPanel = transform.Find("CardUI").Find("CardPanel");
        cardDetail = cardPanel.Find("CardTetail").GetComponent<Text>();
        cardName = cardPanel.Find("CardNameBg").Find("Text").GetComponent<Text>();
        cardCost = cardPanel.Find("CostBG").Find("CostText").GetComponent<Text>();
        cardType = cardPanel.Find("CardTypeBG").Find("CardTypeText").GetComponent<Text>();

        m_CardState = Definition.Enum.CardState.InDesk;       
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        monsterData = GameEntry.DataNode.GetData<VarMonsterData>(Definition.Node.MonsterNode);
        actionData = GameEntry.DataNode.GetNode(Definition.Node.ActionDataNode).GetData<VarActionData>().Value;

        BeforeTakeEffect = null;
        TakeEffect = null;
        AfterTakeEffect = null;
    }

    protected virtual void OnMouseEnterCard()
    {
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseEnter.Create());
    }

    protected virtual void OnMouseExitCard()
    {
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseExit.Create());
    }
    protected virtual void OnMouseDownCard()
    {
        if (cantPlay())
            return;

        OnDetermined();
        OnBeforeTakeEffect();
        OnTakeEffect();
        OnAfterTakeEffect();
        Debug.Log("play the card");
        m_CardState = Definition.Enum.CardState.InGrave;
    }

    protected virtual bool cantPlay()
    {
        return m_CardState != Definition.Enum.CardState.InHands || 
            !actionData.CanCost(m_CardCost) ||
            !GameEntry.GameManager.isPlayerTurn||
            GameEntry.Procedure.CurrentProcedure != GameEntry.Procedure.GetProcedure<ProcedureAction>();
    }

    /// <summary>
    /// 当卡牌确认出牌后，消耗费用
    /// </summary>
    public void OnDetermined()
    {
        //Debug.Log("OnDetermined");
        this.CostPower(this);
        GameEntry.Event.Fire(this.gameObject, CardDeterminedEvent.Create());
    }

    /// <summary>
    /// 在出牌前，处理敌方反击
    /// </summary>
    public void OnBeforeTakeEffect()
    {
        //Debug.Log("OnBeforeTakeEffect");
        if (BeforeTakeEffect != null) 
            BeforeTakeEffect.Invoke(this);
    }

    /// <summary>
    /// 使用卡牌效果
    /// </summary>
    public void OnTakeEffect()
    {
        //Debug.Log("OnTakeEffect");
        if (TakeEffect != null) 
            TakeEffect.Invoke(this);
    }

    /// <summary>
    /// 卡牌生效后
    /// </summary>
    public void OnAfterTakeEffect()
    {
        //Debug.Log("OnAfterTakeEffect");
        if (AfterTakeEffect != null)
            AfterTakeEffect.Invoke(this);
    }

    /// <summary>
    /// 游戏结算
    /// </summary>
    public void GameSettlement()
    {

    }
}
