using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayController : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] FlippingPauser flippingPauser;
    [SerializeField] CardPositioner cardPositioner;

    public int CardFlips { get; set; }
    public int MatchesMade { get; set; }

    public bool GameStarted { get; set; }
    private bool listeningForCardFlips;
    List<Card> flippedCardPair = new List<Card>();
    List<Card> unmatchedCards = new List<Card>();

    public static event Action OnCardFlipsUpdated; 

    private void Start()
    {
        Card.OnCardFlipped += IncrementCardFlips;
        Card.OnCardFlipped += HandleMatchAttempt;

        GameStarted = true;
    }

    private void Update()
    {
        ExitGameByEscKey();

        if(GameStarted == false)
        {
            StartGame();
        }
    }

    private void OnDestroy()
    {
        Card.OnCardFlipped -= IncrementCardFlips;
        Card.OnCardFlipped -= HandleMatchAttempt;
    }

    public void StartGame()
    {
        if(GameStarted == false)
        {   
            GameStarted = true;
            deck.AllCards.Clear();
            unmatchedCards.Clear();
            deck.SetUpDeckAndInstantiateCards();
            InitializeUnmatchedCards();
            cardPositioner.PositionCardsFromDeck();
            PeakBeforeGameStart();            
        }       
    }
    
    private void PeakBeforeGameStart()
    {
        listeningForCardFlips = false;
        PauseCardFlipping();
        Invoke("FlipAllCards", 2f);
        Invoke("UnflipAllCards", 7f);
        Invoke("UnpauseCardFlippng", 8f);
        Invoke("EnableListeningForCardFlipEvent", 8f);
    }

    private void HandleMatchAttempt()
    {
        if ((listeningForCardFlips && CardFlips >= 1 && CardFlips % 2 != 0))
        {
            PauseCardFlipping();
            Invoke("UnpauseCardFlippng", .5f);
        }

        else if (listeningForCardFlips && CardFlips >1 && CardFlips % 2 == 0)
        {
            PauseCardFlipping();
            FindFlippedCardPairInDeck();
            CompareFlippedCardPair();            
        }
    }
    private void CompareFlippedCardPair()
    {
        if (flippedCardPair[0].PairNumber == flippedCardPair[1].PairNumber)
        {
            MatchesMade++;
            RemoveMatchedPairFromUnmatchedCards();
            ClearFlippedCardPair();
            Invoke("UnpauseCardFlippng", 0.5f);
            CheckForGameRestart();
        }
        else
        {
            ClearFlippedCardPair();
            Invoke("UnflipUnmatchedCards", 1.5f);
            Invoke("UnpauseCardFlippng", 2f);
        }
    }

    private void CheckForGameRestart()
    {
        if(MatchesMade >= 12)
        {
            PauseCardFlipping();
            listeningForCardFlips = false;
            Invoke("ResetCardFlipsWithDelay", 1.5f);
            Invoke("UnflipAllCards", 3f);
            MatchesMade = 0;
            Invoke("DestroyOriginalTwentyFourCards", 5f);
            Invoke("SetGameStateToFalse", 7f);
        }
    }

    private void ResetCardFlipsWithDelay()
    {
        CardFlips = 0;
        OnCardFlipsUpdated?.Invoke();
    }

    private void RemoveMatchedPairFromUnmatchedCards()
    {
        unmatchedCards.Remove(flippedCardPair[0]);
        unmatchedCards.Remove(flippedCardPair[1]);
    }
    
    private void EnableListeningForCardFlipEvent()
    {
        listeningForCardFlips = true;
    }    
 
    private void InitializeUnmatchedCards()
    {
        foreach (var card in deck.AllCards)
        {
            unmatchedCards.Add(card);
        }
    }
    private void IncrementCardFlips()
    {
        if (listeningForCardFlips)
        {
            CardFlips++;
            OnCardFlipsUpdated?.Invoke();
        }

    }

    private void DestroyOriginalTwentyFourCards()
    {
        for(int i=0; i<= 23; i++)
        {
            Destroy(deck.AllCards[i].gameObject);
        }
    }

    private void SetGameStateToFalse()
    {
        GameStarted = false;
    }

    private void PauseCardFlipping()
    {
        flippingPauser.PauseFlipping();
    }

    private void UnpauseCardFlippng()
    {
        flippingPauser.UnpauseFlipping();
    }

    private void FindFlippedCardPairInDeck()
    {
        foreach (var card in unmatchedCards)
        {
            if (card.Flipped == true)
            {
                flippedCardPair.Add(card);
            }
        }
    }

    private void ClearFlippedCardPair()
    {
        flippedCardPair.Clear();
    }

    private void UnflipAllCards()
    {
        deck.UnflipAllCards();
    }

    private void UnflipUnmatchedCards()
    {
        deck.UnflipAllCardsExcept(unmatchedCards);
    }

    private void FlipAllCards()
    {
        deck.FlipAllCards();
    }       
    
    private static void ExitGameByEscKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
