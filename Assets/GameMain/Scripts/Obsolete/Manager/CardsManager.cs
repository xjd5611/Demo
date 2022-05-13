using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameFramework.UI;
using UnityGameFramework.Runtime;
using GameFramework.Event;

public class CardsManager : UIFormLogic
{
    public List<GameObject> cardsInDesk;
    public List<GameObject> cardsInHand;
    public List<GameObject> cardsInGrave;

    private Sequence m_Sequence;

    private Transform deskTF;
    private Transform handTF;
    private Transform graveTF;
    private Transform cardEffectTF;
    private void CardsInit()
    {
        cardsInDesk = new List<GameObject>();
        cardsInHand = new List<GameObject>();
        cardsInGrave = new List<GameObject>();
        m_Sequence = DOTween.Sequence();
        m_Sequence.SetAutoKill(false);

        deskTF = transform.Find("deskTF");
        handTF = transform.Find("handTF");
        graveTF = transform.Find("graveTF");
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        CardsInit();
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        GameEntry.Event.Subscribe(CardDeterminedEvent.EventId, OnCardDetermined);

        
    }

    private void OnCardDetermined(object sender, GameEventArgs e)
    {
        GameObject card = sender as GameObject;
        card.transform.SetParent(cardEffectTF);
        Tween tween = card.transform.DOMove(cardEffectTF.position, 1f);
        Tween tween1 = card.transform.DOMove(graveTF.position, 1f);
        tween1.onComplete = () =>
        {
            card.transform.SetParent(graveTF);
        };
        m_Sequence.Append(tween);
        m_Sequence.Append(tween1);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }


    /// <summary>
    /// ≥È≈∆
    /// </summary>
    /// <param name="count">≥È≈∆ ˝</param>
    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if(cardsInDesk.Count == 0)
            {
                RecycleCards();
            }
            if(cardsInDesk.Count == 0)
            {
                Debug.LogError("No card in desk!");
            }

            GameObject cardDrew = cardsInDesk[0];
            cardDrew.transform.position = deskTF.position;
            cardDrew.SetActive(true);

            Tween tween = cardDrew.transform.DOMove(handTF.position, 1f);
            tween.onComplete = () =>
            {
                cardDrew.transform.SetParent(handTF);
            };
            m_Sequence.Append(tween);
        }
        GameEntry.Event.Fire(this, UpdateUIEvent.Create());
    }

    /// <summary>
    /// œ¥≈∆
    /// </summary>
    public void Shuffle()
    {
        for (int i = 0; i < cardsInDesk.Count; i++)
        {
            int r = Random.Range(i, cardsInDesk.Count);
            GameObject temp = cardsInDesk[i];
            cardsInDesk[i] = cardsInDesk[r];
            cardsInDesk[r] = temp;
        }
        GameEntry.Event.Fire(this, UpdateUIEvent.Create());
    }

    /// <summary>
    /// ªÿ ’ø®≈∆
    /// </summary>
    public void RecycleCards()
    {
        for (int i = 0; i < cardsInGrave.Count; i++)
        {
            cardsInDesk.Add(cardsInGrave[i]);
        }
        cardsInGrave.RemoveAll(it => true);
        Shuffle();
        GameEntry.Event.Fire(this, UpdateUIEvent.Create());
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(100, 100), new Vector2(200, 80)), "œ¥≈∆"))
        {
            Debug.Log("Before shuffle : ");
            var beforecards = "";
            for (int i = 0; i < cardsInDesk.Count; i++)
            {
                beforecards += cardsInDesk[i].name + ",";
            }
            Debug.Log(beforecards);
            Shuffle();
            var aftercards = "";
            for (int i = 0; i < cardsInDesk.Count; i++)
            {
                aftercards += cardsInDesk[i].name + ",";
            }
            Debug.Log(aftercards);
        }
        if (GUI.Button(new Rect(new Vector2(100, 200), new Vector2(200, 80)), "≥È1’≈≈∆"))
        {
            DrawCards(1);
        }
    }


}
