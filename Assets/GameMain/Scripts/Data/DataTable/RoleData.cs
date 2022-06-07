using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData : CharacterData
{

    /// <summary>
    /// 获取名称。
    /// </summary>
    public string NickName
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取生命上限。
    /// </summary>
    public int HPMax
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取技能ID。
    /// </summary>
    public string SkillCardsId
    {
        get;
        private set;
    }

    public int CurHP;


    public RoleData(int entityId, int typeId) 
        : base(entityId,typeId)
    {
        DRRoles dRRoles = GameEntry.DataTable.GetDataTable<DRRoles>().GetDataRow(typeId);
        this.NickName = dRRoles.NickName;
        this.HPMax = dRRoles.HPMax;
        this.SkillCardsId = dRRoles.SkillCardsId;
        CurHP = HPMax;
    }

    public void TakeDemage(int demage)
    {
        CurHP = Mathf.Clamp(CurHP - demage, 0, HPMax);
        if(CurHP <= 0)
        {
            isDead = true;
            GameEntry.Event.Fire(this, GameSettlement.Create());
        }
    }
}
