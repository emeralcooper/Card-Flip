using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] GameplayController gameplayController;

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gameplayController.GameStarted = false;
        Destroy(this.gameObject);
    }
}
