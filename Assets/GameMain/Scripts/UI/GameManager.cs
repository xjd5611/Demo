using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;
using GameFramework.Event;
using System;
using DG.Tweening;
using GameFramework.DataNode;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

public class GameManager : GameFrameworkComponent
{
    public float radius = 2f;
    public float angleStep = 10f;

    private Sequence m_Sequence;

    private Transform deskTF;
    private Transform handTF;
    private Transform graveTF;
    private Transform cardUseTF;

    private CardGather m_deskCards;
    private CardGather m_handCards;
    private CardGather m_graveCards;

    private bool initSuccess = false;

    private CharacterData m_actor;
    private CharacterData m_role;
    private CharacterData m_monster;

    private ActionData m_actionData;

    #region 初始化
    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        deskTF = transform.Find("Desk");
        handTF = transform.Find("Hand");
        graveTF = transform.Find("Garve");
        cardUseTF = transform.Find("CardUseTF");

        //GameEntry.Event.Subscribe(CardCreatInDesk.EventId, OnCardCreateInDesk);
        GameEntry.Event.Subscribe(CardDeterminedEvent.EventId, OnCardDetermined);
        GameEntry.Event.Subscribe(GameSettlement.EventId, OnGameSettlement);
        //GameEntry.Event.Subscribe(GameOverEvent.EventId, OnGameOver);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void GameManagerInit()
    {
        IDataNode PlayerCardsNode = GameEntry.DataNode.GetOrAddNode(Definition.Node.PlayerCardsNode);
        m_deskCards = PlayerCardsNode.GetOrAddChild(Definition.Node.DeskCardsNode).GetData<VarCardGather>().Value;
        m_handCards = PlayerCardsNode.GetOrAddChild(Definition.Node.HandCardsNode).GetData<VarCardGather>().Value;
        m_graveCards = PlayerCardsNode.GetOrAddChild(Definition.Node.GraveCardsNode).GetData<VarCardGather>().Value;
        m_actionData = GameEntry.DataNode.GetNode(Definition.Node.ActionDataNode).GetData<VarActionData>().Value;
        GetDeskCards();

        m_role = GameEntry.DataNode.GetData<VarRoleData>(Definition.Node.RoleNode);
        m_monster = GameEntry.DataNode.GetData<VarMonsterData>(Definition.Node.MonsterNode);

        m_Sequence = DOTween.Sequence();
        m_Sequence.SetAutoKill(false);

        initSuccess = true;
    }

    private void OnGameSettlement(object sender, GameEventArgs e)
    {
        if(m_role.isDead)
        {
            GameEntry.Event.Fire(false, GameOverEvent.Create());
        }
        else if (m_monster.isDead)
        {
            GameEntry.Event.Fire(true, GameOverEvent.Create());
        }
    }

    private void OnGameOver(object sender, GameEventArgs e)
    {
        GameOverEvent gameOverEvent = e as GameOverEvent;
        GameEntry.UI.OpenGameOverUI(gameOverEvent.IsWin);
    }
    #endregion

    #region 卡牌控制

    private int[] cardsId = new int[] {10001,10002,10003,10004,
            10005,10006,11001,11002,11003,12002,12003,12004,19001};

    private void Update()
    {
        if (!initSuccess)
            return;
        UpdateCardsInHandPos();
    }


    /// <summary>
    /// 获取已经创建的卡牌
    /// </summary>
    private void GetDeskCards()
    {
        Entity[] cards = GameEntry.Entity.GetEntities(Definition.AssetPath.Card);
        for (int i = 0; i < cards.Length; i++)
        {
            //cards[i].gameObject.transform.SetParent(deskTF);
            cards[i].gameObject.transform.position = deskTF.position;
            cards[i].gameObject.SetActive(false);
            m_deskCards.AddCard(cards[i].gameObject);
        }
        m_deskCards.Shuffle();
    }

    /// <summary>
    /// 当卡牌打出时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCardDetermined(object sender, GameEventArgs e)
    {
        m_Sequence = DOTween.Sequence();
        GameObject card = sender as GameObject;
        m_handCards.RemoveCard(card);
        //card.transform.SetParent(cardUseTF);
        Tween tween = card.transform.DOMove(cardUseTF.position, 1f);
        Tween tween1 = card.transform.DOMove(graveTF.position, 1f);
        tween.onComplete = () =>
        {
            if (card.GetComponent<CardLogicBase>().m_CardType == Definition.Enum.CardType.AttackCard)
            {
                GameEntry.Event.Fire(this, MonsterTakeDemage.Create());
            }
            GameEntry.Event.Fire(this, UpdateUIEvent.Create());
            //m_handCards.RemoveCard(card);
            //m_graveCards.AddCard(card);
        };
        tween1.onComplete = () =>
        {
            m_graveCards.AddCard(card);
            //card.transform.SetParent(graveTF);
            card.SetActive(false);
        };
        m_Sequence.Append(tween);
        m_Sequence.Append(tween1);
    }

    /// <summary>
    /// 更新卡牌在手中的位置
    /// </summary>
    private void UpdateCardsInHandPos()
    {
        for (int i = 0; i < m_handCards.CardCount; i++)
        {
            GameObject child = m_handCards.m_cards[i].gameObject;
            float angle = -angleStep * (m_handCards.CardCount - 1) + i * 2 * angleStep;
            child.transform.position = handTF.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius - radius, 100 + i);
            child.transform.rotation = Quaternion.Euler(0, 0, -angle);
        }

        //for (int i = 0; i < handTF.childCount; i++)
        //{
        //    GameObject child = handTF.GetChild(i).gameObject;
        //    float angle = -angleStep * (handTF.childCount - 1) + i * 2 * angleStep;
        //    child.transform.position = handTF.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
        //        Mathf.Cos(angle * Mathf.Deg2Rad) * radius - radius, 100 + i);
        //    child.transform.rotation = Quaternion.Euler(0, 0, -angle);
        //}
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="count">抽牌数</param>
    public void DrawCards(int count)
    {
        m_Sequence = DOTween.Sequence();
        for (int i = 0; i < count; i++)
        {
            if (m_deskCards.CardCount == 0)
            {
                RecycleCards();
            }
            if (m_deskCards.CardCount == 0)
            {
                Debug.Log("No card in desk!");
                break;
            }

            GameObject card = m_deskCards.DrawCard();
            //m_handCards.AddCard(card);

            Tween tween = card.transform.DOMove(handTF.position, 0.8f);
            tween.OnStart(() =>
            {
                card.SetActive(true);
                //card = GameEntry.Entity.ShowCardEntity(cardData);
            });
            tween.onComplete = () =>
            {
                m_handCards.AddCard(card);
                //card.transform.SetParent(handTF);
                card.GetComponent<CardLogicBase>().m_CardState = Definition.Enum.CardState.InHands;
            };
            m_Sequence.Append(tween);
        }
        if (m_deskCards.CardCount == 0)
        {
            RecycleCards();
        }
    }

    /// <summary>
    /// 回收卡牌
    /// </summary>
    public void RecycleCards()
    {
        for (int i = 0; i < m_graveCards.CardCount; i++)
        {
            GameObject card = m_graveCards.DrawCard();
            card.transform.position = deskTF.position;            
            m_deskCards.AddCard(card);
        }
    }

    public void DestroyAllCards()
    {
        for (int i = 0; i < m_deskCards.CardCount; i++)
        {
            m_deskCards.m_cards[i].SetActive(false);
            //m_deskCards.m_cards[i].transform.SetParent(deskTF);
            //Destroy(m_deskCards.m_cards[i]);
        }
        for (int i = 0; i < m_graveCards.CardCount; i++)
        {
            m_graveCards.m_cards[i].SetActive(false);
            //m_deskCards.m_cards[i].transform.SetParent(deskTF);
            //Destroy(m_graveCards.m_cards[i]);
        }
        for (int i = 0; i < m_handCards.CardCount; i++)
        {
            m_handCards.m_cards[i].SetActive(false);
            //m_deskCards.m_cards[i].transform.SetParent(deskTF);
            //Destroy(m_handCards.m_cards[i]);
        }

        m_deskCards.Clear();
        m_handCards.Clear();
        m_graveCards.Clear();
    }

    #endregion

    /// <summary>
    /// 测试
    /// </summary>
    private void OnGUI()
    {
        //if (GUI.Button(new Rect(new Vector2(100, 100), new Vector2(200, 80)), "洗牌", new GUIStyle() { fontSize = 40 }))
        //{

        //}
        //if (GUI.Button(new Rect(new Vector2(100, 200), new Vector2(200, 80)), "抽1张牌", new GUIStyle() { fontSize = 40 }))
        //{
        //    DrawCards(1);
        //}
    }

    #region 控制回合
    /// <summary>
    /// 是否为玩家回合
    /// </summary>
    public bool isPlayerTurn => m_actor == m_role;

    /// <summary>
    /// 重新选择回合行动者
    /// </summary>
    public void SelectActor()
    {
        m_actor = isPlayerTurn ? m_monster : m_role;
        GameEntry.Event.Fire(this, ActorSelectedEvent.Create());
    }

    public void ResetPower()
    {
        m_actionData.ResetPower();
    }

    #endregion

    
}
