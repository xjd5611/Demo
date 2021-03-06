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
/// 鼠标进入卡牌范围
/// </summary>
public class MouseEnterCard : EventArgs<MouseEnterCard> { }
public class MouseExitCard : EventArgs<MouseExitCard> { }
public class MouseDownCard : EventArgs<MouseDownCard> { }
public class MouseUpCard : EventArgs<MouseUpCard> { }
public class MouseDragCard : EventArgs<MouseDragCard> { }

/// <summary>
/// 鼠标进入怪兽范围
/// </summary>
public class MouseOverMst : EventArgs<MouseOverMst> { }
public class MouseEnterMst : EventArgs<MouseEnterMst> { }
public class MouseExitMst : EventArgs<MouseExitMst> { }

/// <summary>
/// 卡牌打出时
/// </summary>
public class CardDeterminedEvent : EventArgs<CardDeterminedEvent> { }
public class CardPlayedEvent : EventArgs<CardPlayedEvent> { }

/// <summary>
/// 卡牌创建时
/// </summary>
public class CardCreatInDesk : EventArgs<CardCreatInDesk> { }

public class RoleTakeDemage : EventArgs<RoleTakeDemage> 
{

}

public class MonsterTakeDemage : EventArgs<MonsterTakeDemage>
{

}

/// <summary>
/// 卡牌生效时（视觉上）更新UI
/// </summary>
public class UpdateUnitUIEvent : EventArgs<UpdateUnitUIEvent> 
{

}


public class ActorSelectedEvent : EventArgs<ActorSelectedEvent> { }

/// <summary>
/// 敌方采取行动
/// </summary>
public class MstTakeActionEvent : EventArgs<MstTakeActionEvent> { }

public class MonsterTakeActionCompletedEvent : EventArgs<MonsterTakeActionCompletedEvent> { }

/// <summary>
/// 当一个单位死亡后，调用游戏解算
/// </summary>
public class GameSettlement : EventArgs<GameSettlement> { }

/// <summary>
/// 游戏结束
/// </summary>
public class GameOverEvent : EventArgs<GameOverEvent> 
{
    public bool IsWin;
}

public class RestartEvent : EventArgs<RestartEvent> { }

public class DrawCardEvent : EventArgs<DrawCardEvent> { }