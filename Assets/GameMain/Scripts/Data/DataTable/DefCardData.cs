using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefCardData : CardData
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
    public string Effects
    {
        get;
        private set;
    }

    public DefCardData(int entityId, int typeId) : base(entityId, typeId)
    {
        DRDefCards  dRDefenseCard = GameEntry.DataTable.GetDataTable<DRDefCards>().GetDataRow(typeId);

        this.Armor = dRDefenseCard.Armor;
        this.Heal = dRDefenseCard.Heal;
        this.Effects = dRDefenseCard.Effects;

        this.CardName = dRDefenseCard.CardName;
        this.CardType = (Definition.Enum.CardType)dRDefenseCard.CardType;
        this.TimeCost = dRDefenseCard.TimeCost;
    }
}
