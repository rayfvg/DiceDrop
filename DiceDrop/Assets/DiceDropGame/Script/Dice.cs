using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public void OnDiceRollComplete()
    {
        playerMovement.MoversPlayer();
        this.gameObject.SetActive(false);
    }
}
