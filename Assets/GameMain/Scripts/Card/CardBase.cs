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
    /// ������ȷ�ϳ��ƺ����ķ���
    /// </summary>
    public virtual void OnDetermined()
    {
        Debug.Log("OnDetermined");
    }

    /// <summary>
    /// �ڳ���ǰ������з�����
    /// </summary>
    public virtual void OnBeforeTakeEffect()
    {
        Debug.Log("OnBeforeTakeEffect");
    }

    /// <summary>
    /// ʹ�ÿ���Ч��
    /// </summary>
    public virtual void OnTakeEffect()
    {
        Debug.Log("OnTakeEffect");
    }

    /// <summary>
    /// ������Ч��
    /// </summary>
    public virtual void OnAfterTakeEffect()
    {
        Debug.Log("OnAfterTakeEffect");
    }

}
