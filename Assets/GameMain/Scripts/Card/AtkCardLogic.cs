using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AtkCardLogic : CardLogicBase
{
    public AtkCardData m_AtkCardData { get; private set; }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        if (userData != null)
        {
            m_AtkCardData = userData as AtkCardData;
            CardInit();
        }
            
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        if (userData != null)
        {
            m_AtkCardData = userData as AtkCardData;
            CardInit();
        }
    }

    private void CardInit()
    {
        m_AtkCardData.cardEntity = this.gameObject;
        m_CardType = Definition.Enum.CardType.AttackCard;
        m_CardCost = m_AtkCardData.TimeCost;

        TakeEffect += this.TakeDemage;
        TakeEffect += this.AddActions;
        if(m_AtkCardData.Effects != "20000")
            TakeEffect += m_AtkCardData.TakeCardEffects;

        cardDetail.text = string.Format("造成{0}点伤害，生成招式{1}。{2}", 
            m_AtkCardData.Demage, m_AtkCardData.Actions.Replace('|',','), m_AtkCardData.EffectDescribe);
        cardName.text = m_AtkCardData.CardName;
        cardCost.text = m_AtkCardData.TimeCost.ToString();
        cardType.text = "攻击牌";
    }

}
