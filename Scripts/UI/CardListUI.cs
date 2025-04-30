using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardListUI : MonoBehaviour
{
    public List<Card> cardList;

    public void ShowCardList()
    {
        GetComponent<RectTransform>().DOLocalMoveY(179,1);
        EnableCardList();
    }

    private void Start()
    {
        DisableCardList();
        //ShowCardList();
    }

    public void DisableCardList()
    {
        foreach(Card card in cardList)
        {
            card.DisableCard();
        }
    }

    public void EnableCardList()
    {
        foreach (Card card in cardList)
        {
            card.EnableCard();
        }
    }
}
