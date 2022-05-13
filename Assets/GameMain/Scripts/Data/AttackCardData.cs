using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardData : CardData
{
    /// <summary>
    /// ��ȡ�˺���
    /// </summary>
    public int Demage
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ��ʽ��
    /// </summary>
    public string Actions
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����Ч��Id��
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
