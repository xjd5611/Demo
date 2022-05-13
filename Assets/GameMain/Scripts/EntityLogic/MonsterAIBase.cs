using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityGameFramework.Runtime;
using GameFramework.Entity;
using GameFramework.Event;

public class MonsterAIBase : EntityLogic
{
    private MonsterData m_monsterData;
    private RoleData m_roleData;
    private int weightSum;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        if(userData != null)
        {
            m_monsterData = userData as MonsterData;
            m_roleData = GameEntry.DataNode.GetData<VarRoleData>(Definition.Node.RoleNode);
            weightSum = m_monsterData.AttackWeight + m_monsterData.SkillWeight1 + m_monsterData.SkillWeight2 + m_monsterData.SkillWeight3;
            transform.position = m_monsterData.Position;
        }
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        GameEntry.Event.Subscribe(MonsterTakeDemage.EventId, OnMonsterGetHit);
        GameEntry.Event.Subscribe(MonsterTakeActionEvent.EventId, OnMonsterTakeAction);

        if(m_monsterData !=null)
            transform.position = m_monsterData.Position;
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GameEntry.Event.Unsubscribe(MonsterTakeDemage.EventId, OnMonsterGetHit);
        GameEntry.Event.Unsubscribe(MonsterTakeActionEvent.EventId, OnMonsterTakeAction);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    private void OnMonsterTakeAction(object sender, GameEventArgs e)
    {
        TakeAction();
    }

    private void OnMonsterGetHit(object sender, GameEventArgs e)
    {
        GameEntry.Entity.ShowGetHitEntity(transform.position, false, false);
    }

    public void TakeAction()
    {
        int random = Random.Range(0, weightSum);
        if (random < m_monsterData.AttackWeight)
        {
            Attack();         
        }
        else if (random < m_monsterData.SkillWeight1)
        {

        }
        else if (random < m_monsterData.SkillWeight2)
        {

        }
        else if (random < m_monsterData.SkillWeight3)
        {

        }
    }

    public void Attack()
    {
        m_roleData.TakeDemage(m_monsterData.Attack);
        GameEntry.Event.Fire(this, RoleTakeDemage.Create());
        GameEntry.Event.Fire(this, MonsterTakeActionCompletedEvent.Create());
    }

}
