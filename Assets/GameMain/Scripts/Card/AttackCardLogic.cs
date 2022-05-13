using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AttackCardLogic : CardLogicBase
{
    public AttackCardData m_AttackCardData { get; private set; }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        if (userData != null)
        {
            m_AttackCardData = userData as AttackCardData;
            CardInit();
        }
            
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        if (userData != null)
        {
            m_AttackCardData = userData as AttackCardData;
            CardInit();
        }
    }

    private void CardInit()
    {
        m_AttackCardData.cardEntity = this.gameObject;
        m_CardType = Definition.Enum.CardType.AttackCard;
        m_CardCost = m_AttackCardData.TimeCost;

        TakeEffect += this.TakeDemage;
        TakeEffect += this.AddActions;
        TakeEffect += AdditionalEffect;

        cardDetail.text = string.Format("造成{0}点伤害，生成招式{1}，附加效果{2}", 
            m_AttackCardData.Demage, m_AttackCardData.Actions, m_AttackCardData.AdditionalEffectId);
        cardName.text = m_AttackCardData.CardName;
        cardCost.text = m_AttackCardData.TimeCost.ToString();
        cardType.text = "攻击牌";
    }

    public void AdditionalEffect(object cardData)
    {
        Type type = typeof(CardEffect);
        MethodInfo method = type.GetMethod(m_AttackCardData.AdditionalEffectId);
        method.Invoke(null, new object[] { this });
    }
}
