using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcTurnStart : ProcedureBase
{
    private IFsm<IProcedureManager> procedureOwner;
    private int turnCount = 1;

    private ActionData m_actionData;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        this.procedureOwner = procedureOwner;
    
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        if(m_actionData == null)
        {
            m_actionData = GameEntry.DataNode.GetNode(Definition.Node.ActionDataNode).GetData<VarActionData>().Value;
        }

        GameEntry.GameManager.SelectActor();

        //GameEntry.Event.Fire(this, new TurnStartEvent(GameEntry.GameManager.));
        ChangeState<ProcStateSettle>(procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }
}
