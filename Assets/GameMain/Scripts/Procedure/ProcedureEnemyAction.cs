using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using GameFramework.Fsm;
using GameFramework.Event;
using System;

public class ProcedureEnemyAction : ProcedureBase
{
    private IFsm<IProcedureManager> m_procedureOwner;
    private float delayTime = 2f;
    private float time = 0;
    private bool isTakeAction = false;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        m_procedureOwner = procedureOwner;
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Event.Subscribe(MonsterTakeActionCompletedEvent.EventId, OnTakeActionCompleted);
        GameEntry.Event.Subscribe(GameOverEvent.EventId, OnGameOver);

        time = 0;
        isTakeAction = false;
    }

    private void OnGameOver(object sender, GameEventArgs e)
    {
        ChangeState<ProcedureGameOver>(m_procedureOwner);
    }

    private void TakeAction()
    {
        GameEntry.Event.Fire(this, MonsterTakeActionEvent.Create());
    }

    private void OnTakeActionCompleted(object sender, GameEventArgs e)
    {
        ChangeState<ProcedureTurnEnd>(m_procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.Event.Unsubscribe(MonsterTakeActionCompletedEvent.EventId, OnTakeActionCompleted);
        GameEntry.Event.Unsubscribe(GameOverEvent.EventId, OnGameOver);

    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        time += elapseSeconds;

        if(time >= delayTime && !isTakeAction)
        {
            isTakeAction = true;
            TakeAction();
        }
    }
}
