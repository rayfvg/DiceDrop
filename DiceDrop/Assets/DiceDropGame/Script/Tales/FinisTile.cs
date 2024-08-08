using UnityEngine;

public class FinishTile : MonoBehaviour
{
    private bool gameFinished = false;
    public Wallet wallet;

    public ParticleSystem Winner;
    public GameObject WinnerLable;
    public GameObject LoseLable;

    public AudioSource Winners;


    public CameraController cam;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameFinished)
        {
            gameFinished = true;

            if (collision.CompareTag("Player"))
            {
                cam.OnReachFinish();
                Winner.Play();
                Winners.Play();
                Invoke("PlayerWin", 2.3f);
                EndGame();
                // ����� ����� �������� �������������� ��������, ����� ��� ����������� UI ������
            }
            else if (collision.CompareTag("Opponent"))
            {
                cam.OnReachFinish();
                Invoke("EnemyWin", 0.3f);
                EndGame();

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
        wallet.Money += wallet.CurrentMoney;
        PlayerPrefs.SetInt("money", wallet.Money);
    }
}
