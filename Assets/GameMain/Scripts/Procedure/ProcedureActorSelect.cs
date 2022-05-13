using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 决定这个回合的行动者（玩家或敌人）/// 
/// </summary>
public class ProcedureActorSelect : ProcedureBase
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
        ChangeState<ProcedureTurnStart>(procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }
}
