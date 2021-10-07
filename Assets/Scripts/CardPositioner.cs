using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositioner : MonoBehaviour
{
    [SerializeField] Vector3 firstCardPosition = new Vector3(11.4f,4.5f,20f);
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingBottom = 0.5f;
    [SerializeField] Deck deck;

    const float cardWidth = 2.5f;
    const float cardHeight = 3.5f;

    private void Start()
    {

    }

    public void PositionCardsFromDeck()
    {
        PositionFirstRow();
        PositionSecondRow();
        PositionThirdRow();
    }

    private void PositionFirstRow()
    {
        deck.AllCards[0].transform.position = firstCardPosition;
        for (int i = 1; i < 8; i++)
        {
            deck.AllCards[i].transform.position = deck.AllCards[i-1].transform.position + new Vector3(cardWidth + paddingRight, 0, 0);
        }
    }

    private void PositionSecondRow()
    {
        Vector3 ninthCardPosition = firstCardPosition - new Vector3(0, cardHeight + paddingBottom, 0);
        deck.AllCards[8].transform.position = ninthCardPosition;
        for (int i = 9; i < 16; i++)
        {
            deck.AllCards[i].transform.position = deck.AllCards[i-1].transform.position + new Vector3(cardWidth + paddingRight, 0, 0);
        }
    }

    private void PositionThirdRow()
    {
        Vector3 seventeenthCardPosition = firstCardPosition - new Vector3(0, cardHeight*2 + paddingBottom*2, 0);
        deck.AllCards[16].transform.position = seventeenthCardPosition;
        for (int i = 17; i < 24; i++)
        {
            deck.AllCards[i].transform.position = deck.AllCards[i-1].transform.position + new Vector3(cardWidth + paddingRight, 0, 0);
        }
    }
}
