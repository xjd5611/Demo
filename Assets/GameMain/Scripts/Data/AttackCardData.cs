using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardData : CardData
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
    /// 获取附加效果Id。
    /// </summary>
    public string AdditionalEffectId
    {
        get;
        private set;
    }


    public AttackCardData(int entityId, int typeId)
        : base(entityId, typeId)
    {
        DRAttackCards dRAttackCards = GameEntry.DataTable.GetDataTable<DRAttackCards>().GetDataRow(typeId);

        this.Demage = dRAttackCards.Demage;
        this.Actions = dRAttackCards.Actions;
        this.AdditionalEffectId = dRAttackCards.AdditionalEffectId;

        this.CardName = dRAttackCards.CardName;
        this.CardType = (Definition.Enum.CardType)dRAttackCards.CardType;
        this.TimeCost = dRAttackCards.TimeCost;
    }
}
