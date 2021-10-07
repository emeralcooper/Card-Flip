using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Card : MonoBehaviour
{
     public enum CardPairs
    {
        pairZero,
        pairOne,
        pairTwo,
        pairThree,
        pairFour,
        pairFive,
        pairSix,
        pairSeven,
        pairEight,
        pairNine,
        pairTen,
        pairEleven
    }

    public static event Action OnCardFlipped;
    
    public CardPairs PairNumber;
    public bool Flipped { get; private set; }
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        FlipCard();   
    }

    public void FlipCard()
    {
        animator.SetBool("flipped", true);
        Flipped = true;
        OnCardFlipped?.Invoke();
    }

    public void UnflipCard()
    {
        Flipped = false;
        animator.SetBool("flipped", false);     
    }
}
