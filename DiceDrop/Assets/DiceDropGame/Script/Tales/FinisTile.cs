using UnityEngine;

public class FinishTile : MonoBehaviour
{
    private bool gameFinished = false;
    public ParticleSystem Winner;
    public GameObject WinnerLable;
    public GameObject LoseLable;

    public AudioSource Winners;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameFinished)
        {
            gameFinished = true;

            if (collision.CompareTag("Player"))
            {
                Winner.Play();
                Winners.Play();
                Invoke("PlayerWin", 2.3f);
                // ����� ����� �������� �������������� ��������, ����� ��� ����������� UI ������
            }
            else if (collision.CompareTag("Opponent"))
            {
                Invoke("EnemyWin", 2.3f);
                // ����� ����� �������� �������������� ��������, ����� ��� ����������� UI ������
            }

            // ���������� ����
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
        // ������ ���������� ����
        // ��������, ����� ���������� ��� ��������, ��������� ���������� � �������� ����� � ������������
        Time.timeScale = 0f; // ��������� ������� � ����
    }
}
