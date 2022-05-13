using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardData : CardData
{
    /// <summary>
    /// ��ȡ����Ч��Id��
    /// </summary>
    public string AdditionalEffectId
    {
        get;
        private set;
    }

    public SkillCardData(int entityId, int typeId)
    : base(entityId, typeId)
    {
        DRSkillCards  dRSkillCard = GameEntry.DataTable.GetDataTable<DRSkillCards>().GetDataRow(typeId);

        this.AdditionalEffectId = dRSkillCard.AdditionalEffectId;
        this.CardName = dRSkillCard.CardName;
        this.CardType = (Definition.Enum.CardType)dRSkillCard.CardType;
        this.TimeCost = dRSkillCard.TimeCost;
    }
}
