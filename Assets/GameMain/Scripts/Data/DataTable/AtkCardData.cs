using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AtkCardData : CardData
{
    /// <summary>
    /// 获取伤害。
    /// </summary>
    public int Demage
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取招式。
    /// </summary>
    public string Actions
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取所有附加效果。
    /// </summary>
    public string Effects
    {
        get;
        private set;
    }

    /// <summary>
    /// 效果描述
    /// </summary>
    public string EffectDescribe 
    {
        get;
        private set;
    }

    public Dictionary<MethodInfo, int> CardEffects;

    public AtkCardData(int entityId, int typeId)
        : base(entityId, typeId)
    {
        DRAtkCards dRAtkCards = GameEntry.DataTable.GetDataTable<DRAtkCards>().GetDataRow(typeId);

        this.Demage = dRAtkCards.Demage;
        this.Actions = dRAtkCards.Actions;
        this.Effects = dRAtkCards.Effects;

        this.CardName = dRAtkCards.CardName;
        this.CardType = (Definition.Enum.CardType)dRAtkCards.CardType;
        this.TimeCost = dRAtkCards.TimeCost;

        this.CardEffects = new Dictionary<MethodInfo, int>();

        string[] effects = dRAtkCards.Effects.Split('|');
        for (int i = 0; i < effects.Length; i++)
        {
            string[] effect = effects[i].Split('*');
            if(effect.Length == 2)
            {
                Debug.Log(effect[0] + "   " + effect[1]);
                int effectId = int.Parse(effect[0]);
                int effectIndex = int.Parse(effect[1]);
                DREffects dREffects = GameEntry.DataTable.GetDataTable<DREffects>().GetDataRow(effectId);
                string methodName = dREffects.EffectName;
                EffectDescribe += string.Format(dREffects.Describe + ",", effectIndex);
                Type type = typeof(CardEffect);
                MethodInfo methodInfo = type.GetMethod(methodName);
                CardEffects.Add(methodInfo, effectIndex);
            }
        }
    }

    public void TakeCardEffects(object cardLogic)
    {
        CardLogicBase m_CardLogic = cardLogic as CardLogicBase;
        foreach (var effect in CardEffects)
        {
            Debug.Log(effect.Key + "   " + effect.Value);
            effect.Key.Invoke(null, new object[] { m_CardLogic, effect.Value });           
        }
    }
}
