using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureStateSettle : ProcedureBase
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
        if (GameEntry.GameManager.isPlayerTurn)
        {
            ChangeState<ProcedureAction>(procedureOwner);
        }
        else
        {
            ChangeState<ProcedureEnemyAction>(procedureOwner);
        }
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }
}
