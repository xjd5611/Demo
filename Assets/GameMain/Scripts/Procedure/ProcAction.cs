using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcAction : ProcedureBase
{
    private IFsm<IProcedureManager> procedureOwner;
    private IFsm<CardBase> m_cardFSM;
    private CardBase m_card;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        this.procedureOwner = procedureOwner;
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Event.Subscribe(CardDeterminedEvent.EventId, OnCardDetermined);
        GameEntry.Event.Subscribe(CardPlayedEvent.EventId, OnCardPlayed);
        GameEntry.Event.Subscribe(TurnEnd.EventId, OnTurnEnd);
        GameEntry.Event.Subscribe(GameOverEvent.EventId, OnGameOver);

        GameEntry.GameManager.DrawCards(3);
        GameEntry.GameManager.ResetPower();
        //GameEntry.Event.Fire(this, UpdateUnitUIEvent.Create());
    }

    private void OnGameOver(object sender, GameEventArgs e)
    {
        ChangeState<ProcGameOver>(procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);

        GameEntry.Event.Unsubscribe(CardDeterminedEvent.EventId, OnCardDetermined);
        GameEntry.Event.Unsubscribe(CardPlayedEvent.EventId, OnCardPlayed);
        GameEntry.Event.Unsubscribe(TurnEnd.EventId, OnTurnEnd);
        GameEntry.Event.Unsubscribe(GameOverEvent.EventId, OnGameOver);
    }

    private void OnTurnEnd(object sender, GameEventArgs e)
    {
        ChangeState<ProcTurnEnd>(procedureOwner);
    }

    private void OnCardPlayed(object sender, GameEventArgs e)
    {
        //CardBase card = sender as CardBase;
        //if (card != m_card) return;
        //GameEntry.Fsm.DestroyFsm<CardBase>();
        //m_card = null;
        //m_cardFSM = null;
    }

    private void OnCardDetermined(object sender, GameEventArgs e)
    {
        //if (m_card != null) return;
        //m_card = sender as CardBase;
        //FsmState<CardBase>[] fsmStates = new FsmState<CardBase>[]{
        //    new Determined(),new BeforeTakeEffect(),new TakeEffect(),new AfterTakeEffect()};
        //m_cardFSM = GameEntry.Fsm.CreateFsm(m_card, fsmStates);
        //m_cardFSM.Start<Determined>();

    }
}
