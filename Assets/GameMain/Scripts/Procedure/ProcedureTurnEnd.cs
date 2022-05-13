using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Fsm;

public class ProcedureTurnEnd : ProcedureBase
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
        //ChangeState<>(procedureOwner);

        ChangeState<ProcedureTurnStart>(procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }
}
