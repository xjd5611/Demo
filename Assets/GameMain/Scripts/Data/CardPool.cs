using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool
{
    [SerializeField]
    private int m_Id = 0;
    public int Id
    {
        get
        {
            return m_Id;
        }
    }

    public List<CardPool> childPool { get; private set; }
    public List<CardData> cards { get;private set; }

    public CardPool(int id)
    {
        m_Id = id;

        childPool = new List<CardPool>();
        cards = new List<CardData>();
    }

    public void AddCard(CardData newCard)
    {
        if (cards.Contains(newCard))
            return;
        cards.Add(newCard);
    }

    public void AddChild(CardPool Pool)
    {
        if (childPool.Contains(Pool))
            return;

        childPool.Add(Pool);
    }
}


public class VarCardPool : Variable<CardPool>
{

    public VarCardPool(int id)
    {
        Value = new CardPool(id);
    }

    /// <summary>
    /// 获取该卡池以及其子卡池中的所有卡牌
    /// </summary>
    /// <returns></returns>
    public List<CardData> GetCardsInPool()
    {
        List<CardData> cards = new List<CardData>();

        Value.childPool.ForEach((pool) =>
        {
            pool.cards.ForEach((card) =>
            {
                cards.Add(card);
            });
        });

        Value.cards.ForEach((card) =>
        {
            cards.Add(card);
        });

        return cards;
    }

}