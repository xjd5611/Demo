using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌确认出牌,消耗费用
/// </summary>
public class Determined : FsmState<CardBase>
{
    protected override void OnEnter(IFsm<CardBase> fsm)
    {
        base.OnEnter(fsm);
        fsm.Owner.OnDetermined();
        ChangeState<BeforeTakeEffect>(fsm);
    }

    protected override void OnLeave(IFsm<CardBase> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}

/// <summary>
/// 卡牌生效前，先判断反击，后判断其他
/// </summary>
public class BeforeTakeEffect: FsmState<CardBase>
{
    protected override void OnEnter(IFsm<CardBase> fsm)
    {
        base.OnEnter(fsm);
        fsm.Owner.OnBeforeTakeEffect();
        ChangeState<TakeEffect>(fsm);
    }

    protected override void OnLeave(IFsm<CardBase> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}

/// <summary>
/// 卡牌处理效果
/// </summary>
public class TakeEffect : FsmState<CardBase>
{
    protected override void OnEnter(IFsm<CardBase> fsm)
    {
        base.OnEnter(fsm);
        fsm.Owner.OnTakeEffect();
        ChangeState<AfterTakeEffect>(fsm);
    }

    protected override void OnLeave(IFsm<CardBase> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}

/// <summary>
/// 卡牌处理完效果
/// </summary>
public class AfterTakeEffect : FsmState<CardBase>
{
    protected override void OnEnter(IFsm<CardBase> fsm)
    {
        base.OnEnter(fsm);
        fsm.Owner.OnAfterTakeEffect();
    }

    protected override void OnLeave(IFsm<CardBase> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        GameEntry.Event.Fire(fsm.Owner, CardPlayedEvent.Create());
    }
}

