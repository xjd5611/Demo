using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityGameFramework.Runtime;
using GameFramework.Entity;
using GameFramework.Event;

public class MstAIBase : EntityLogic
{
    public MstData m_mstData { get; private set; }
    private RoleData m_roleData;
    private int weightSum;

    private GameObject actorBG;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        if(userData != null)
        {
            m_mstData = userData as MstData;
            m_roleData = GameEntry.DataNode.GetData<VarRoleData>(Definition.Node.RoleNode);
            weightSum = m_mstData.AttackWeight + m_mstData.SkillWeight1 + m_mstData.SkillWeight2 + m_mstData.SkillWeight3;
            transform.position = m_mstData.Position;
        }

        actorBG = transform.Find("ActorBG").gameObject;
        actorBG.SetActive(false);
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        GameEntry.Event.Subscribe(MonsterTakeDemage.EventId, OnMonsterGetHit);
        GameEntry.Event.Subscribe(MstTakeActionEvent.EventId, OnMonsterTakeAction);
        GameEntry.Event.Subscribe(ActorSelectedEvent.EventId, OnActorSelected);

        if(m_mstData !=null)
            transform.position = m_mstData.Position;
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GameEntry.Event.Unsubscribe(MonsterTakeDemage.EventId, OnMonsterGetHit);
        GameEntry.Event.Unsubscribe(MstTakeActionEvent.EventId, OnMonsterTakeAction);
        GameEntry.Event.Unsubscribe(ActorSelectedEvent.EventId, OnActorSelected);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    private void OnActorSelected(object sender, GameEventArgs e)
    {
        int id = (int)sender;
        actorBG.SetActive(id == m_mstData.Id);
    }

    private void OnMonsterTakeAction(object sender, GameEventArgs e)
    {
        int id = (int)sender;
        if (id == m_mstData.Id)
            TakeAction();
    }

    private void OnMonsterGetHit(object sender, GameEventArgs e)
    {
        GameObject target = sender as GameObject;
        if (target == this.gameObject)
        {
            GameEntry.Entity.ShowGetHitEntity(transform.position, false, false);
        }      
    }

    public void TakeAction()
    {
        int random = Random.Range(0, weightSum);
        if (random < m_mstData.AttackWeight)
        {
            Attack();         
        }
        else if (random < m_mstData.SkillWeight1)
        {

        }
        else if (random < m_mstData.SkillWeight2)
        {

        }
        else if (random < m_mstData.SkillWeight3)
        {

        }
    }

    public void Attack()
    {
        m_roleData.TakeDemage(m_mstData.Attack);
        GameEntry.Event.Fire(this, UpdateUnitUIEvent.Create());
        GameEntry.Event.Fire(this, RoleTakeDemage.Create());
        GameEntry.Event.Fire(this, MonsterTakeActionCompletedEvent.Create());
    }

    private void OnMouseOver()
    {
        GameEntry.Event.Fire(this.gameObject, MouseOverMst.Create());
    }
    private void OnMouseEnter()
    {
        GameEntry.Event.Fire(this.gameObject, MouseEnterMst.Create());
    }
    private void OnMouseExit()
    {
        GameEntry.Event.Fire(this.gameObject, MouseExitMst.Create());
    }
}
