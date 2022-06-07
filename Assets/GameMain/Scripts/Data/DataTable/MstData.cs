using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MstData : CharacterData
{
    /// <summary>
    /// ��ȡ���ơ�
    /// </summary>
    public string NickName
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ�������ޡ�
    /// </summary>
    public int HPMax
    {
        get;
        private set;
    }

    public int CurHP;

    /// <summary>
    /// ��ȡ����Ȩ�ء�
    /// </summary>
    public int Attack
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����Ȩ�ء�
    /// </summary>
    public int AttackWeight
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����Ȩ�ء�
    /// </summary>
    public int SkillWeight1
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����ID��
    /// </summary>
    public int SkillId1
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����Ȩ�ء�
    /// </summary>
    public int SkillWeight2
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����ID��
    /// </summary>
    public int SkillId2
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����Ȩ�ء�
    /// </summary>
    public int SkillWeight3
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����ID��
    /// </summary>
    public int SkillId3
    {
        get;
        private set;
    }

    public MstData(int entityId,int typeId) : base(entityId, typeId)
    {
        DRMsts dRMonsters = GameEntry.DataTable.GetDataTable<DRMsts>().GetDataRow(typeId);
        this.NickName = dRMonsters.NickName;
        this.HPMax = dRMonsters.HPMax;
        this.Attack = dRMonsters.Attack;
        this.AttackWeight = dRMonsters.AttackWeight;
        this.SkillWeight1 = dRMonsters.SkillWeight1;
        this.SkillId1 = dRMonsters.SkillId1;
        this.SkillWeight2 = dRMonsters.SkillWeight2;
        this.SkillId2 = dRMonsters.SkillId2;
        this.SkillWeight3 = dRMonsters.SkillWeight3;
        this.SkillId3 = dRMonsters.SkillId3;

        CurHP = HPMax;
    }

    public void TakeDemage(int demage)
    {
        CurHP = Mathf.Clamp(CurHP - demage, 0, HPMax);
        if (CurHP <= 0)
        {
            isDead = true;
            GameEntry.Event.Fire(this, GameSettlement.Create());         
        }
    }
}
