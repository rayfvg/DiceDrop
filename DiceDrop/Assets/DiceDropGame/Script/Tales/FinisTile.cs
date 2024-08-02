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
                // ����� ����� �������� �������������� ��������, ����� ��� ����������� UI ������
            }
            else if (collision.CompareTag("Opponent"))
            {
                Debug.Log("Opponent wins!");
                // ����� ����� �������� �������������� ��������, ����� ��� ����������� UI ������
            }

            // ���������� ����
            EndGame();
        }
    }

    private void EndGame()
    {
        // ������ ���������� ����
        // ��������, ����� ���������� ��� ��������, ��������� ���������� � �������� ����� � ������������
        Time.timeScale = 0f; // ��������� ������� � ����
    }
}
