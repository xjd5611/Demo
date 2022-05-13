using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ȷ�ϳ���,���ķ���
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
/// ������Чǰ�����жϷ��������ж�����
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
/// ���ƴ���Ч��
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
/// ���ƴ�����Ч��
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

