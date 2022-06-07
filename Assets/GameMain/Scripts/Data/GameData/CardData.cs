using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : EntityData
{
    /// <summary>
    /// ��ȡ�������ơ�
    /// </summary>
    public string CardName
    {
        get;
        protected set;
    }

    /// <summary>
    /// ��ȡʱ�����ġ�
    /// </summary>
    public int TimeCost
    {
        get;
        protected set;
    }

    /// <summary>
    /// ��ȡ�������ࡣ
    /// </summary>
    public Definition.Enum.CardType CardType
    {
        get;
        protected set;
    }

    public GameObject cardEntity;

    /// <summary>
    /// �����ڿ��飬���ƣ�Ĺ�ص����
    /// </summary>
    public int Serial;


    public CardData(int entityId, int typeId)
    : base(entityId, typeId)
    {

    }
}
