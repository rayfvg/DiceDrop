using UnityEngine;

public class BonusTile : MonoBehaviour
{
    public int bonusSteps = 3;

    public void ActivateBonus(GameObject character)
    {
        if (character.CompareTag("Player"))
        {
            character.GetComponent<PlayerMovement>().MovePlayerForBonus(bonusSteps);
        }
        else if (character.CompareTag("Opponent"))
        {
            character.GetComponent<OpponentMovement>().MovePlayerForBonus(bonusSteps);
        }
    }
    

}
