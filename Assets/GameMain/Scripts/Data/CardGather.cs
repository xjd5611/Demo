using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保存手牌，卡组，墓地的卡牌数据
/// </summary>
public class CardGather 
{
    //public List<AttackCardData> m_attackCards { get;private set; }
    //public List<DefenseCardData> m_defenseCards { get; private set; }
    //public List<SkillCardData> m_skillCards { get; private set; }

    //public List<CardData> cardSort;

    //public CardGather()
    //{
    //    m_attackCards = new List<AttackCardData>();
    //    m_defenseCards = new List<DefenseCardData>();
    //    m_skillCards = new List<SkillCardData>();
    //}

    //public void AddCard(AttackCardData newCard)
    //{
    //    m_attackCards.Add(newCard);
    //    cardSort.Add(newCard);
    //}

    //public void AddCard(DefenseCardData newCard)
    //{
    //    m_defenseCards.Add(newCard);
    //    cardSort.Add(newCard);
    //}

    //public void AddCard(SkillCardData newCard)
    //{
    //    m_skillCards.Add(newCard);
    //    cardSort.Add(newCard);
    //}

    //public void RemoveCard(AttackCardData cardRemove)
    //{
    //    if (!m_attackCards.Contains(cardRemove))
    //        Debug.LogError("the removecard is not in");
    //    m_attackCards.Remove(cardRemove);
    //    cardSort.Remove(cardRemove);
    //}

    //public void RemoveCard(DefenseCardData cardRemove)
    //{
    //    if (!m_defenseCards.Contains(cardRemove))
    //        Debug.LogError("the removecard is not in");
    //    m_defenseCards.Remove(cardRemove);
    //    cardSort.Remove(cardRemove);
    //}

    //public void RemoveCard(SkillCardData cardRemove)
    //{
    //    if (!m_skillCards.Contains(cardRemove))
    //        Debug.LogError("the removecard is not in");
    //    m_skillCards.Remove(cardRemove);
    //    cardSort.Remove(cardRemove);
    //}

    //public void PopCard()
    //{
    //    if (m_attackCards.Count == 0)
    //        Debug.LogError("No attack card");

    //    CardData popCard = cardSort[0];
    //    for (int i = 0; i < m_attackCards.Count; i++)
    //    {
    //        if (m_attackCards[i].Id == popCard.Id)
    //        {
    //            m_attackCards.Remove(m_attackCards[i]);         
    //        }
    //    }
    //    for (int i = 0; i < m_defenseCards.Count; i++)
    //    {
    //        if (m_defenseCards[i].Id == popCard.Id)
    //        {
    //            m_defenseCards.Remove(m_defenseCards[i]);
    //        }
    //    }
    //    for (int i = 0; i < m_attackCards.Count; i++)
    //    {
    //        if (m_attackCards[i].Id == popCard.Id)
    //        {
    //            m_attackCards.Remove(m_attackCards[i]);
    //        }
    //    }
    //    cardSort.Remove(popCard);

    //}

    //public AttackCardData PopAttackCard()
    //{
    //    if (m_attackCards.Count == 0)
    //        Debug.LogError("No attack card");

    //    AttackCardData attackCardData = null;
    //    for (int i = 0; i < m_attackCards.Count; i++)
    //    {
    //        if(m_attackCards[i].Id == cardSort[0].Id)
    //        {
    //            attackCardData = m_attackCards[i];
    //            m_attackCards.Remove(attackCardData);
    //            cardSort.RemoveAt(0);
    //        }
    //    }

    //    return attackCardData;
    //}

    //public DefenseCardData PopDefenseCard()
    //{
    //    if (m_defenseCards.Count == 0)
    //        Debug.LogError("No attack card");

    //    DefenseCardData defenseCardData = m_defenseCards[Random.Range(0, m_defenseCards.Count)];
    //    m_defenseCards.Remove(defenseCardData);
    //    return defenseCardData;
    //}

    //public SkillCardData PopSkillCard()
    //{
    //    if (m_skillCards.Count == 0)
    //        Debug.LogError("No attack card");

    //    SkillCardData skillCardData = m_skillCards[Random.Range(0, m_skillCards.Count)];
    //    m_skillCards.Remove(skillCardData);
    //    return skillCardData;
    //}

    //public CardType GetCard()
    //{
    //    int random = Random.Range(0, CardCount);
    //    if (0 < CardCount && CardCount < m_attackCards.Count)
    //    {
    //        return CardType.AttackCard;
    //    }
    //    else if (m_attackCards.Count <= CardCount && CardCount < m_attackCards.Count + m_defenseCards.Count)
    //    {
    //        return CardType.DefenseCard;
    //    }
    //    else if (m_attackCards.Count + m_defenseCards.Count <= CardCount && CardCount < CardCount)
    //    {
    //        return CardType.SkillCard;
    //    }

    //    Debug.LogError("cannot get card");
    //    return CardType.AttackCard;
    //}

    public List<GameObject> m_cards;

    public CardGather()
    {
        m_cards = new List<GameObject>();
    }

    public void AddCard(GameObject card)
    {
        m_cards.Add(card);
    }

    public GameObject DrawCard()
    {     
        GameObject card = m_cards[0];
        m_cards.Remove(card);
        return card;

    }

    public void RemoveCard(GameObject card)
    {
        m_cards.Remove(card);
    }

    public void Clear()
    {
        m_cards.Clear();
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        for (int i = 0; i < m_cards.Count; i++)
        {
            int r = Random.Range(i, m_cards.Count);
            GameObject temp = m_cards[i];
            m_cards[i] = m_cards[r];
            m_cards[r] = temp;
        }
    }

    public int CardCount => m_cards.Count;
}

public class VarCardGather : Variable<CardGather>
{
    public VarCardGather()
    {
    }

    public static implicit operator VarCardGather(CardGather value)
    {
        VarCardGather varValue = ReferencePool.Acquire<VarCardGather>();
        varValue.Value = value;
        return varValue;
    }

    public static implicit operator CardGather(VarCardGather value)
    {
        return value.Value;
    }
}
