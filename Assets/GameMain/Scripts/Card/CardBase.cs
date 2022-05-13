using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    public Definition.Enum.CardState cardState { get; protected set; }


    private void OnMouseEnter() => OnMouseEnterCard();
    private void OnMouseDown() => OnMouseDownCard();
    private void OnMouseExit() => OnMouseExitCard();
    private void OnMouseDrag() => OnMouseDownCard();

    protected virtual void OnMouseEnterCard()
    {
        if(cardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseEnter.Create());
    }

    protected virtual void OnMouseExitCard()
    {
        if (cardState == Definition.Enum.CardState.InHands)
            GameEntry.Event.Fire(this.gameObject, MouseExit.Create());
    }
    protected virtual void OnMouseDownCard()
    {
        if (canPlay())
            GameEntry.Event.Fire(this, CardDeterminedEvent.Create());
    }
    protected virtual void OnMouseDragCard()
    {

    }

    protected virtual bool canPlay()
    {
        return true;
    }

    /// <summary>
    /// 当卡牌确认出牌后，消耗费用
    /// </summary>
    public virtual void OnDetermined()
    {
        Debug.Log("OnDetermined");
    }

    /// <summary>
    /// 在出牌前，处理敌方反击
    /// </summary>
    public virtual void OnBeforeTakeEffect()
    {
        Debug.Log("OnBeforeTakeEffect");
    }

    /// <summary>
    /// 使用卡牌效果
    /// </summary>
    public virtual void OnTakeEffect()
    {
        Debug.Log("OnTakeEffect");
    }

    /// <summary>
    /// 卡牌生效后
    /// </summary>
    public virtual void OnAfterTakeEffect()
    {
        Debug.Log("OnAfterTakeEffect");
    }

}
