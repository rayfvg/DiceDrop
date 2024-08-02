using UnityEngine;

public class PenaltyTile : MonoBehaviour
{
    public int penaltySteps = 3;

    public void ActivatePenalty(GameObject character)
    {
        if (character.CompareTag("Player"))
        {
            character.GetComponent<PlayerMovement>().MovePlayerForPenalty(penaltySteps);
        }
        else if (character.CompareTag("Opponent"))
        {
            character.GetComponent<OpponentMovement>().MovePlayerForPenalty(penaltySteps);
        }
    }
}