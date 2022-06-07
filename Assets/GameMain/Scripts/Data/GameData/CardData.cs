using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : EntityData
{
    /// <summary>
    /// 获取卡牌名称。
    /// </summary>
    public string CardName
    {
        get;
        protected set;
    }

    /// <summary>
    /// 获取时间消耗。
    /// </summary>
    public int TimeCost
    {
        get;
        protected set;
    }

    /// <summary>
    /// 获取卡牌种类。
    /// </summary>
    public Definition.Enum.CardType CardType
    {
        get;
        protected set;
    }

    public GameObject cardEntity;

    /// <summary>
    /// 卡牌在卡组，手牌，墓地的序号
    /// </summary>
    public int Serial;


    public CardData(int entityId, int typeId)
    : base(entityId, typeId)
    {

    }
}
