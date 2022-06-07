using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

public static class EntityExtension
{
    private static int m_EntityId = 0;

    public static int EntityId(this EntityComponent entityComponent)
    {
        return m_EntityId--;
    }

    //public static void CreateCardEntity(this EntityComponent entityComponent, CardData cardData)
    //{
    //    int id = entityComponent.EntityId();
    //    GameEntry.Entity.ShowEntity<CardLogicBase>(id, Definition.AssetPath.Card, Definition.Group.Cards, cardData);
    //}

    public static string CharacterPath(int typeId)
    {
        return string.Format(Definition.AssetPath.CharacterPath, typeId);
    }

    public static void CreateRoleEntity(this EntityComponent entityComponent, int entityId, object roleData)
    {
        RoleData data = roleData as RoleData;
        string assetPath = CharacterPath(data.TypeId);
        string group = Definition.Group.CharacterGroup;
        GameEntry.Entity.ShowEntity<RoleLogic>(entityId, assetPath, group, roleData);
        //GameEntry.Entity.AttachEntity(id, parentId);
    }

    public static void CreateMonsterEntity(this EntityComponent entityComponent, int entityId, object monsterData)
    {
        MstData data = monsterData as MstData;
        string assetPath = CharacterPath(data.TypeId);
        string group = Definition.Group.CharacterGroup;
        GameEntry.Entity.ShowEntity<MstAIBase>(entityId, assetPath, group, monsterData);
        //GameEntry.Entity.AttachEntity(id, parentId);
    }

    public static void CreateAttackCardEntity(this EntityComponent entityComponent, int entityId, AtkCardData cardData)
    {
        GameEntry.Entity.ShowEntity<AtkCardLogic>(entityId, Definition.AssetPath.Card, Definition.Group.Cards, cardData);
    }

    public static void CreateDefenseCardEntity(this EntityComponent entityComponent, int entityId, DefCardData cardData)
    {
        GameEntry.Entity.ShowEntity<SkiCardLogic>(entityId, Definition.AssetPath.Card, Definition.Group.Cards, cardData);
    }

    public static void CreateSkillCardEntity(this EntityComponent entityComponent, int entityId, SkiCardData cardData)
    {
        GameEntry.Entity.ShowEntity<MagCardLogic>(entityId, Definition.AssetPath.Card, Definition.Group.Cards, cardData);
    }

    public static void ShowGetHitEntity(this EntityComponent entityComponent, Vector3 position, bool FlipX, bool FlipY)
    {
        FXData fXData = new FXData(GameEntry.Entity.EntityId(), 123, position, FlipX, FlipY);
        GameEntry.Entity.ShowEntity<FXAutoKill>(
            GameEntry.Entity.EntityId(),
            Definition.AssetPath.GetHitFXPath,
            Definition.Group.FXGroup, fXData);
    }

    public static void ShowCard()
    {

    }
}
