using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] GameplayController gamePlayController;

    TextMeshProUGUI MatchAttemptsText;

    private void Start()
    {
        GameplayController.OnCardFlipsUpdated += UpdateMatchAttempts;
        
        MatchAttemptsText = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        GameplayController.OnCardFlipsUpdated -= UpdateMatchAttempts;
    }

    private void UpdateMatchAttempts()
    {
        Invoke("DelayUpdateMatchAttempts", 1.5f);
    }

    private void DelayUpdateMatchAttempts()
    {
        int cardFlips = gamePlayController.CardFlips;

        if (cardFlips % 2 == 0)
        {
            MatchAttemptsText.text = (cardFlips / 2).ToString();
        }
    }
}
