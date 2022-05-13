using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCardData : CardData
{

    /// <summary>
    /// 获取护甲。
    /// </summary>
    public int Armor
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取治疗。
    /// </summary>
    public int Heal
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

    public DefenseCardData(int entityId, int typeId) : base(entityId, typeId)
    {
        DRDefenseCards  dRDefenseCard = GameEntry.DataTable.GetDataTable<DRDefenseCards>().GetDataRow(typeId);

        this.Armor = dRDefenseCard.Armor;
        this.Heal = dRDefenseCard.Heal;
        this.AdditionalEffectId = dRDefenseCard.AdditionalEffectId;

        this.CardName = dRDefenseCard.CardName;
        this.CardType = (Definition.Enum.CardType)dRDefenseCard.CardType;
        this.TimeCost = dRDefenseCard.TimeCost;
    }
}
