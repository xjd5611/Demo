using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureGameOver : ProcedureBase
{
    private IFsm<IProcedureManager> procedureOwner;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        this.procedureOwner = procedureOwner;
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Event.Subscribe(RestartEvent.EventId, OnRestartGame);

        GameEntry.UI.OpenGameOverUI(true);
    }

    private void OnRestartGame(object sender, GameEventArgs e)
    {
        ChangeState<ProcedureLoadAsset>(procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.Event.Unsubscribe(RestartEvent.EventId, OnRestartGame);

        GameEntry.Entity.HideAllLoadedEntities();
        GameEntry.UI.CloseAllLoadedUIForms();
        GameEntry.GameManager.DestroyAllCards();
        //GameEntry.UI.CloseGameOverUI();
    }
}
