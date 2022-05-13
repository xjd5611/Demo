using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardEffect
{


    public static void CardInit(this CardLogicBase card)
    {

    }

    public static void CostPower(this CardLogicBase card,object cardData)
    {
        card.actionData.CostPower(card.m_CardCost);
    }

    /// <summary>
    /// ´ò³öÉËº¦
    /// </summary>
    /// <param name="cardLogic"></param>
    /// <param name="cardData"></param>
    public static void TakeDemage(this AttackCardLogic card, object cardData)
    {
        card.monsterData.TakeDemage(card.m_AttackCardData.Demage);
    }

    public static void AddActions(this AttackCardLogic card, object cardData)
    {
        string actionStr = card.m_AttackCardData.Actions;

        string[] splitStr1 = actionStr.Split('|');
        if(splitStr1.Length > 0)
        {
            for (int i = 0; i < splitStr1.Length; i++)
            {
                string[] splitStr2 = splitStr1[i].Split('*');
                if(splitStr2.Length == 2)
                {
                    Definition.Enum.ActionType actionType = (Definition.Enum.ActionType)int.Parse(splitStr2[0]);
                    int count = int.Parse(splitStr2[1]);
                    card.actionData.AddAct(actionType, count);
                }
            }
        }
    }


    #region Additional Effect

    public static void HurtSelf(this AttackCardLogic card)
    {

    }

    #endregion
}
