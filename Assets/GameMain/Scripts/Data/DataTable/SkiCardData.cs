using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiCardData : CardData
{
    /// <summary>
    /// 获取附加效果Id。
    /// </summary>
    public string Effects
    {
        get;
        private set;
    }

    public SkiCardData(int entityId, int typeId)
    : base(entityId, typeId)
    {
        DRSkiCards  dRSkillCard = GameEntry.DataTable.GetDataTable<DRSkiCards>().GetDataRow(typeId);

        this.Effects = dRSkillCard.Effects;
        this.CardName = dRSkillCard.CardName;
        this.CardType = (Definition.Enum.CardType)dRSkillCard.CardType;
        this.TimeCost = dRSkillCard.TimeCost;
    }
}
