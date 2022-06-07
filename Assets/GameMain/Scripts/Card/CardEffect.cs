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
    /// 打出伤害
    /// </summary>
    /// <param name="cardLogic"></param>
    /// <param name="cardData"></param>
    public static void TakeDemage(this AtkCardLogic card, object cardData)
    {
        card.mstData.TakeDemage(card.m_AtkCardData.Demage);
    }

    public static void AddActions(this AtkCardLogic card, object cardData)
    {
        string actionStr = card.m_AtkCardData.Actions;

        string[] splitStr1 = actionStr.Split('|');
        if(splitStr1.Length > 0)
        {
            for (int i = 0; i < splitStr1.Length; i++)
            {
                string[] splitStr2 = splitStr1[i].Split('*');
                if(splitStr2.Length == 2)
                {
                    Definition.Enum.ActionType actionType = Definition.Enum.ActionType.unknown;
                    switch (splitStr2[0])
                    {
                        case "点":
                            actionType = Definition.Enum.ActionType.点;
                            break;
                        case "刺":
                            actionType = Definition.Enum.ActionType.刺;
                            break;
                        case "劈":
                            actionType = Definition.Enum.ActionType.劈;
                            break;
                        case "扫":
                            actionType = Definition.Enum.ActionType.扫;
                            break;
                        default:
                            break;
                    }
                    int count = int.Parse(splitStr2[1]);
                    card.actionData.AddAct(actionType, count);
                }
            }
        }
    }


    #region Card Effects
    public static void NoEffect(CardLogicBase m_CardLogic, int value)
    {

    }

    public static void AtkOppo(CardLogicBase m_CardLogic, int value)
    {

    }

    public static void MulAtk(CardLogicBase m_CardLogic, int value)
    {

    }

    public static void AtkSelf(CardLogicBase m_CardLogic, int value)
    {
        GameEntry.GameManager.m_role.TakeDemage(value);
    }

    public static void SelfBlood(CardLogicBase m_CardLogic, int value)
    {

    }

    public static void OppoBlood(CardLogicBase m_CardLogic, int value)
    {

    }

    /// <summary>
    /// 虚张声势
    /// </summary>
    /// <param name="card"></param>
    public static void Bravado(CardLogicBase m_CardLogic, int value)
    {

    }

    public static void DrawCard(CardLogicBase m_CardLogic, int value)
    {

    }

    public static void CreateCards(CardLogicBase m_CardLogic, int value)
    {

    }

    #endregion
}
