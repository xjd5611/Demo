using GameFramework;
using GameFramework.Event;
using System.Collections.Generic;

public class EventArgs<T> : GameEventArgs where T : EventArgs<T>
{
    public static readonly int EventId = typeof(T).GetHashCode();

    public override int Id { get { return EventId; } }

    public static T Create() { return (T)ReferencePool.Acquire(typeof(T)); }

    public override void Clear() { }
}

public class TurnStartEvent : EventArgs<TurnStartEvent> 
{
    public CharacterData actor { get; private set; }
    public TurnStartEvent(CharacterData actor)
    {
        this.actor = actor;
    }
}
public class TurnEnd : EventArgs<TurnEnd> { }


/// <summary>
/// �����뿨�Ʒ�Χ
/// </summary>
public class MouseEnter : EventArgs<MouseEnter> { }
public class MouseExit : EventArgs<MouseExit> { }


/// <summary>
/// ���ƴ��ʱ
/// </summary>
public class CardDeterminedEvent : EventArgs<CardDeterminedEvent> { }
public class CardPlayedEvent : EventArgs<CardPlayedEvent> { }

/// <summary>
/// ���ƴ���ʱ
/// </summary>
public class CardCreatInDesk : EventArgs<CardCreatInDesk> { }

public class RoleTakeDemage : EventArgs<RoleTakeDemage> 
{

}

public class MonsterTakeDemage : EventArgs<MonsterTakeDemage>
{

}

/// <summary>
/// ������Чʱ���Ӿ��ϣ�����UI
/// </summary>
public class UpdateUIEvent : EventArgs<UpdateUIEvent> { }


public class ActorSelectedEvent : EventArgs<ActorSelectedEvent> { }

/// <summary>
/// �з���ȡ�ж�
/// </summary>
public class MonsterTakeActionEvent : EventArgs<MonsterTakeActionEvent> { }

public class MonsterTakeActionCompletedEvent : EventArgs<MonsterTakeActionCompletedEvent> { }

/// <summary>
/// ��һ����λ�����󣬵�����Ϸ����
/// </summary>
public class GameSettlement : EventArgs<GameSettlement> { }

/// <summary>
/// ��Ϸ����
/// </summary>
public class GameOverEvent : EventArgs<GameOverEvent> 
{
    public bool IsWin;
}

public class RestartEvent : EventArgs<RestartEvent> { }

public class DrawCardEvent : EventArgs<DrawCardEvent> { }