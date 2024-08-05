using UnityEngine;

public class FinishTile : MonoBehaviour
{
    private bool gameFinished = false;
    public ParticleSystem Winner;
    public GameObject WinnerLable;
    public GameObject LoseLable;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameFinished)
        {
            gameFinished = true;

            if (collision.CompareTag("Player"))
            {
                Winner.Play();
                Invoke("PlayerWin", 2.3f);
                // Здесь можно добавить дополнительные действия, такие как отображение UI победы
            }
            else if (collision.CompareTag("Opponent"))
            {
                Invoke("EnemyWin", 2.3f);
                // Здесь можно добавить дополнительные действия, такие как отображение UI победы
            }

            // Завершение игры
            //EndGame();
        }
    }
    private void PlayerWin()
    {
        WinnerLable.SetActive(true);
    }
    private void EnemyWin()
    {
        LoseLable.SetActive(true);
    }

    private void EndGame()
    {
        // Логика завершения игры
        // Например, можно остановить все движения, выключить управление и показать экран с результатами
        Time.timeScale = 0f; // Остановка времени в игре
    }
}
