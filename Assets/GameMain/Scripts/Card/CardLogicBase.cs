using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Entity;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CardLogicBase : EntityLogic
{
    protected Text cardName;
    protected Text cardCost;
    protected Text cardDetail;
    protected Text cardType;

    public RoleData roleData { get; private set; }
    public MstData mstData { get; private set; }
    public ActionData actionData { get; private set; }

    public Definition.Enum.CardType m_CardType = Definition.Enum.CardType.Unknown;

    public int m_CardCost = 0;

    public Definition.Enum.CardState m_CardState = Definition.Enum.CardState.Unknown;

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
        roleData = GameEntry.DataNode.GetData<VarRoleData>(Definition.Node.RoleNode);
        //monsterData = GameEntry.DataNode.GetData<VarMstsData>(Definition.Node.MstsNode);
        actionData = GameEntry.DataNode.GetNode(Definition.Node.ActionDataNode).GetData<VarActionData>().Value;

        BeforeTakeEffect = null;
        TakeEffect = null;
        AfterTakeEffect = null;
    }

    private void OnMouseEnter() 
    {
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseEnterCard.Create());
    }

    private void OnMouseExit()
    {
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseExitCard.Create());
    }


    private void OnMouseDown() 
    {
        //Debug.Log("OnMouseDown : " + this.Entity.Id);        
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseDownCard.Create());
    }


    private void OnMouseUp()
    {
        //Debug.Log("OnMouseUp : " + this.Entity.Id);
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseUpCard.Create());

        if(GameEntry.GameManager.targetMst == null)
        {
            return;
        }

        mstData = GameEntry.GameManager.targetMst;

        if (cantPlay())
            return;

        OnDetermined();
        OnBeforeTakeEffect();
        OnTakeEffect();
        OnAfterTakeEffect();
        Debug.Log("play the card");
        m_CardState = Definition.Enum.CardState.InGrave;
    }

    private void OnMouseDrag()
    {
        //Debug.Log("OnMouseDrag : " + this.Entity.Id);
        if (m_CardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseDragCard.Create());
    }

    protected virtual bool cantPlay()
    {
        return m_CardState != Definition.Enum.CardState.InHands || 
            !actionData.CanCost(m_CardCost) ||
            !GameEntry.GameManager.isPlayerTurn||
            GameEntry.Procedure.CurrentProcedure != GameEntry.Procedure.GetProcedure<ProcAction>();
    }

    /// <summary>
    /// ������ȷ�ϳ��ƺ����ķ���
    /// </summary>
    public void OnDetermined()
    {
        //Debug.Log("OnDetermined");
        this.CostPower(this);
        GameEntry.Event.Fire(this.gameObject, CardDeterminedEvent.Create());
    }

    /// <summary>
    /// �ڳ���ǰ������з�����
    /// </summary>
    public void OnBeforeTakeEffect()
    {
        //Debug.Log("OnBeforeTakeEffect");
        if (BeforeTakeEffect != null) 
            BeforeTakeEffect.Invoke(this);
    }

    /// <summary>
    /// ʹ�ÿ���Ч��
    /// </summary>
    public void OnTakeEffect()
    {
        //Debug.Log("OnTakeEffect");
        if (TakeEffect != null) 
            TakeEffect.Invoke(this);
    }

    /// <summary>
    /// ������Ч��
    /// </summary>
    public void OnAfterTakeEffect()
    {
        //Debug.Log("OnAfterTakeEffect");
        if (AfterTakeEffect != null)
            AfterTakeEffect.Invoke(this);
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void GameSettlement()
    {

    }
}
