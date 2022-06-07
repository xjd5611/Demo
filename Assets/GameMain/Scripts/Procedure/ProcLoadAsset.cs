using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFramework.DataNode;
using GameFramework.DataTable;
using GameFramework.Entity;
using GameFramework.Event;
using ShowEntitySuccessEventArgs = UnityGameFramework.Runtime.ShowEntitySuccessEventArgs;
using System;

public class ProcLoadAsset : ProcedureBase
{

    private IFsm<IProcedureManager> m_procedureOwner;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        m_procedureOwner = procedureOwner;
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnEntityShowSuccess);
        GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUISuccess);

        LoadCharacterDic = new Dictionary<int, bool>();
        LoadCardsDic = new Dictionary<int, bool>();

        LoadData();
        LoadCharacter();
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnEntityShowSuccess);
        GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUISuccess);
        GameEntry.UI.CloseLoadingUI();
    }


    private Dictionary<int, bool> LoadCharacterDic;
    private Dictionary<int, bool> LoadCardsDic;

    private void OnEntityShowSuccess(object sender, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs showEntitySuccessEventArgs = e as ShowEntitySuccessEventArgs;

        if (LoadCharacterDic.ContainsKey(showEntitySuccessEventArgs.Entity.Id))
        {
            Debug.Log(string.Format("加载角色{0}成功", showEntitySuccessEventArgs.Entity.Id));
            LoadCharacterDic[showEntitySuccessEventArgs.Entity.Id] = true;
            foreach (var item in LoadCharacterDic)
            {
                if (!item.Value)
                    return;
            }
            LoadPlayerCards();
        }

        if (LoadCardsDic.ContainsKey(showEntitySuccessEventArgs.Entity.Id))
        {
            Debug.Log(string.Format("加载卡牌{0}成功", showEntitySuccessEventArgs.Entity.Id));
            LoadCardsDic[showEntitySuccessEventArgs.Entity.Id] = true;        
            foreach (var item in LoadCardsDic)
            {
                if (!item.Value)
                    return;
            }
            LoadUI(); 
        }

    }

    private Dictionary<string, bool> UIOpenDic;
    private void OnOpenUISuccess(object sender, GameEventArgs e)
    {
        OpenUIFormSuccessEventArgs openUIFormSuccessEventArgs = e as OpenUIFormSuccessEventArgs;

        if (UIOpenDic.ContainsKey(openUIFormSuccessEventArgs.UIForm.UIFormAssetName))
        {
            UIOpenDic[openUIFormSuccessEventArgs.UIForm.UIFormAssetName] = true;
            Debug.Log(string.Format("加载UI{0}成功", openUIFormSuccessEventArgs.UIForm.name));
            if (!UIOpenDic.ContainsValue(false))
            {
                GameEntry.GameManager.GameManagerInit();
                ChangeState<ProcTurnStart>(m_procedureOwner);
            }
        }
    }
    #region 加载数据
    private void LoadData()
    {
        //dataTableLoadDic = new Dictionary<string, bool>();
        LoadGameData();
        LoadCardData();
    }

    private StoreyData m_storeyData;
    private RoleData m_role;
    private MstData[] m_msts;
    private CardGather deskCards;
    private CardGather handCards;
    private CardGather graveCards;
    /// <summary>
    /// 加载游戏数据，包括对局双方，能量及行动条上限
    /// </summary>
    private void LoadGameData()
    {
        if(m_storeyData == null)
        {
            m_storeyData = new StoreyData(1);
            GameEntry.DataNode.SetData(Definition.Node.StoreyNode, new VarStoreyData(m_storeyData));
        }
        else
        {
            m_storeyData = GameEntry.DataNode.GetData<VarStoreyData>(Definition.Node.StoreyNode);
        }

        m_role = new RoleData(GameEntry.Entity.EntityId(), 1) { Position = new Vector3(-4.5f, 0.5f, 0) };
        m_msts = m_storeyData.msts;
      
        GameEntry.DataNode.SetData(Definition.Node.RoleNode, new VarRoleData(m_role));
        GameEntry.DataNode.SetData(Definition.Node.MstsNode, new VarMstsData(m_msts));
        GameEntry.DataNode.SetData(Definition.Node.ActionDataNode, new VarActionData(5, 5));

        deskCards = new CardGather();
        handCards = new CardGather();
        graveCards = new CardGather();

        IDataNode PlayerCardsNode = GameEntry.DataNode.GetOrAddNode(Definition.Node.PlayerCardsNode);
        IDataNode DeskCardsNode = PlayerCardsNode.GetOrAddChild(Definition.Node.DeskCardsNode);
        IDataNode HandCardsNode = PlayerCardsNode.GetOrAddChild(Definition.Node.HandCardsNode);
        IDataNode GraveCardsNode = PlayerCardsNode.GetOrAddChild(Definition.Node.GraveCardsNode);

        HandCardsNode.SetData((VarCardGather)handCards);
        GraveCardsNode.SetData((VarCardGather)graveCards);
        DeskCardsNode.SetData((VarCardGather)deskCards);
    }

    /// <summary>
    /// 加载卡牌数据
    /// </summary>
    private void LoadCardData()
    {                  
        IDataNode cardPoolNode = GameEntry.DataNode.GetOrAddNode(Definition.Node.AllCardPool);
        IDataNode attackCardsNode = cardPoolNode.GetOrAddChild(Definition.Node.AttackCardPool);
        IDataNode defenseCardsNode = cardPoolNode.GetOrAddChild(Definition.Node.DefenseCardPool);
        IDataNode skillCardsNode = cardPoolNode.GetOrAddChild(Definition.Node.SkillCardPool);

        IDataTable<DRAtkCards> attackCardsDT = GameEntry.DataTable.GetDataTable<DRAtkCards>();
        DRAtkCards[] dRAttackCards = GameEntry.DataTable.GetDataTable<DRAtkCards>().GetAllDataRows();

        IDataTable<DRDefCards> defenseCardsDT = GameEntry.DataTable.GetDataTable<DRDefCards>();
        DRDefCards[] dRDefenseCards = GameEntry.DataTable.GetDataTable<DRDefCards>().GetAllDataRows();

        IDataTable<DRSkiCards> skillCardsDT = GameEntry.DataTable.GetDataTable<DRSkiCards>();
        DRSkiCards[] dRSkillCards = GameEntry.DataTable.GetDataTable<DRSkiCards>().GetAllDataRows();

        VarCardPool attackCardPool = new VarCardPool(GameEntry.Entity.EntityId());
        VarCardPool defenseCardPool = new VarCardPool(GameEntry.Entity.EntityId());
        VarCardPool skillCardPool = new VarCardPool(GameEntry.Entity.EntityId());
        VarCardPool allCardPool = new VarCardPool(GameEntry.Entity.EntityId());

        for (int i = 0; i < dRAttackCards.Length; i++)
        {
            int cardId = GameEntry.Entity.EntityId();
            VarAttackCardData varAttackCardData = new VarAttackCardData(cardId, dRAttackCards[i].Id);
            attackCardPool.Value.AddCard(varAttackCardData.Value);
        }

        for (int i = 0; i < dRDefenseCards.Length; i++)
        {
            int cardId = GameEntry.Entity.EntityId();
            VarDefenseCardData varDefenseCardData = new VarDefenseCardData(cardId, dRDefenseCards[i].Id);
            defenseCardPool.Value.AddCard(varDefenseCardData.Value);
        }

        for (int i = 0; i < dRSkillCards.Length; i++)
        {
            int cardId = GameEntry.Entity.EntityId();
            VarSkillCardData varSkillCardData = new VarSkillCardData(GameEntry.Entity.EntityId(), dRSkillCards[i].Id);
            skillCardPool.Value.AddCard(varSkillCardData.Value);
        }

        attackCardsNode.SetData(attackCardPool);
        defenseCardsNode.SetData(defenseCardPool);
        skillCardsNode.SetData(skillCardPool);

        allCardPool.Value.AddChild(attackCardPool.Value);
        allCardPool.Value.AddChild(defenseCardPool.Value);
        allCardPool.Value.AddChild(skillCardPool.Value);

        cardPoolNode.SetData(allCardPool);
    
    }

    #endregion

    #region 加载实体
    private void LoadCharacter()
    {
        int roleId = GameEntry.Entity.EntityId();
        int monsterId = GameEntry.Entity.EntityId();
        LoadCharacterDic.Add(roleId, false);
        LoadCharacterDic.Add(monsterId, false);
        GameEntry.Entity.CreateRoleEntity(roleId, m_role);
        for (int i = 0; i < m_msts.Length; i++)
        {
            GameEntry.Entity.CreateMonsterEntity(monsterId, m_msts[i]);
            monsterId = GameEntry.Entity.EntityId();
        }
    }

    private int[] cardsId = new int[] {10001,10001,10001,10002,10003,10004,
            10005,10006,10006,11001,11002,11003,12002,12003,12004,19001};


    /// <summary>
    /// 加载玩家持有卡牌
    /// </summary>
    private void LoadPlayerCards()
    {
        IDataTable<DRAtkCards> attackCardsDT = GameEntry.DataTable.GetDataTable<DRAtkCards>();
        //DRAttackCards[] dRAttackCards = GameEntry.DataTable.GetDataTable<DRAttackCards>().GetAllDataRows();
        IDataTable<DRDefCards> defenseCardsDT = GameEntry.DataTable.GetDataTable<DRDefCards>();
        //DRDefenseCards[] dRDefenseCards = GameEntry.DataTable.GetDataTable<DRDefenseCards>().GetAllDataRows();
        IDataTable<DRSkiCards> skillCardsDT = GameEntry.DataTable.GetDataTable<DRSkiCards>();
        //DRSkillCards[] dRSkillCards = GameEntry.DataTable.GetDataTable<DRSkillCards>().GetAllDataRows();

        for (int i = 0; i < cardsId.Length; i++)
        {
            if (attackCardsDT.HasDataRow(cardsId[i]))
            {
                int cardDataId = GameEntry.Entity.EntityId();
                //VarAttackCardData varAttackCardData = new VarAttackCardData(cardDataId, cardsId[i]);
                AtkCardData attackCardData = new AtkCardData(cardDataId, cardsId[i]);
                int cardId = GameEntry.Entity.EntityId();
                LoadCardsDic.Add(cardId, false);
                GameEntry.Entity.CreateAttackCardEntity(cardId, attackCardData);
            }
            //if (defenseCardsDT.HasDataRow(cardsId[i]))
            //{
            //    int cardId = GameEntry.Entity.EntityId();
            //    VarDefenseCardData varDefenseCardData = new VarDefenseCardData(cardId, cardsId[i]);
            //    //deskCards.AddCard(varDefenseCardData.Value.cardEntity);
            //    GameEntry.Entity.CreateDefenseCardEntity(varDefenseCardData.Value);
            //}
            //if (skillCardsDT.HasDataRow(cardsId[i]))
            //{
            //    int cardId = GameEntry.Entity.EntityId();
            //    VarSkillCardData varSkillCardData = new VarSkillCardData(cardId, cardsId[i]);
            //    //deskCards.AddCard(varSkillCardData.Value.cardEntity);
            //    GameEntry.Entity.CreateSkillCardEntity(varSkillCardData.Value);
            //}
        }

    }

    #endregion

    #region 加载UI
    private void LoadUI()
    {
        UIOpenDic = new Dictionary<string, bool>();
        GameEntry.UI.OpenCharacterUI();
        GameEntry.UI.OpenPlayerInteractUI();

        UIOpenDic.Add(Definition.UIPath.CharacterUI, false);
        UIOpenDic.Add(Definition.UIPath.PlayerInteractUI, false);
    }
    #endregion
}
