using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingPauser : MonoBehaviour
{
    BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    public void PauseFlipping()
    {
        boxCollider.enabled = true;
    }

    public void UnpauseFlipping()
    {
        boxCollider.enabled = false;
    }
}
