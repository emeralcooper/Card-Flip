using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour
{
    [SerializeField] Card[] originalTwelveCards;

    public List<Card> AllCards { get; private set; } = new List<Card>();

    private void Awake()
    {

    }

    public void SetUpDeckAndInstantiateCards()
    {
        LabelOriginalCardsAndDuplicate();
        ShuffleAllCards();
        InstantiateAllCards();
    }

    private void LabelOriginalCardsAndDuplicate()
    {
        for(int i=0; i<originalTwelveCards.Length; i++)
        {
            originalTwelveCards[i].PairNumber = (Card.CardPairs)i;
            AllCards.Add(originalTwelveCards[i]);
            AllCards.Add(originalTwelveCards[i]);
        }
    }

    private void ShuffleAllCards()
    {
        AllCards = AllCards.OrderBy(x => Guid.NewGuid()).ToList();
    }

    private void InstantiateAllCards()
    {
        for (int i = 0; i < AllCards.Count; i++)
        {
            AllCards[i] = Instantiate(AllCards[i], new Vector3(0,0,0) , Quaternion.identity);
        }
    }

    public void FlipAllCards()
    {
        for (int i = 0; i < AllCards.Count; i++)
        {
            AllCards[i].FlipCard();
        }
    }

    public void UnflipAllCards()
    {
        for (int i = 0; i < AllCards.Count; i++)
        {
            AllCards[i].UnflipCard();
        }
    }

    public void UnflipAllCardsExcept(List<Card> cards)
    {
        foreach(var card in AllCards)
        {
            foreach(var cardcard in cards)
            {
                if(card == cardcard)
                {
                    card.UnflipCard();
                }
            }
        }      
    }



}
