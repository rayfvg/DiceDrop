using UnityEngine;

public class FinishTile : MonoBehaviour
{
    private bool gameFinished = false;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameFinished)
        {
            gameFinished = true;

            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player wins!");
                // Здесь можно добавить дополнительные действия, такие как отображение UI победы
            }
            else if (collision.CompareTag("Opponent"))
            {
                Debug.Log("Opponent wins!");
                // Здесь можно добавить дополнительные действия, такие как отображение UI победы
            }

            // Завершение игры
            EndGame();
        }
    }

    private void EndGame()
    {
        // Логика завершения игры
        // Например, можно остановить все движения, выключить управление и показать экран с результатами
        Time.timeScale = 0f; // Остановка времени в игре
    }
}
