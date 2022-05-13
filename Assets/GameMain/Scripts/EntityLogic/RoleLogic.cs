using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Entity;
using UnityGameFramework.Runtime;
using GameFramework.Event;

public class RoleLogic : EntityLogic
{
    private Animator m_animator;
    private RoleData m_roleData;


    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_animator = GetComponent<Animator>();
        if(userData != null)
        {
            m_roleData = userData as RoleData;
            transform.position = m_roleData.Position;
        }
    }
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        GameEntry.Event.Subscribe(RoleTakeDemage.EventId, OnRoleGetHit);
        if(m_roleData != null)
            transform.position = m_roleData.Position;
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GameEntry.Event.Unsubscribe(RoleTakeDemage.EventId, OnRoleGetHit);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

    }

    private void OnRoleGetHit(object sender, GameEventArgs e)
    {
        GameEntry.Entity.ShowGetHitEntity(transform.position, false, true);
    }
}
