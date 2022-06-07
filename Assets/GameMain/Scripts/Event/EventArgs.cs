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
public class MouseEnterCard : EventArgs<MouseEnterCard> { }
public class MouseExitCard : EventArgs<MouseExitCard> { }
public class MouseDownCard : EventArgs<MouseDownCard> { }
public class MouseUpCard : EventArgs<MouseUpCard> { }
public class MouseDragCard : EventArgs<MouseDragCard> { }

/// <summary>
/// ��������޷�Χ
/// </summary>
public class MouseOverMst : EventArgs<MouseOverMst> { }
public class MouseEnterMst : EventArgs<MouseEnterMst> { }
public class MouseExitMst : EventArgs<MouseExitMst> { }

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
public class UpdateUnitUIEvent : EventArgs<UpdateUnitUIEvent> 
{

}


public class ActorSelectedEvent : EventArgs<ActorSelectedEvent> { }

/// <summary>
/// �з���ȡ�ж�
/// </summary>
public class MstTakeActionEvent : EventArgs<MstTakeActionEvent> { }

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