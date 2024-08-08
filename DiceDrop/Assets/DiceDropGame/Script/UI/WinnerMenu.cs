using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WinnerMenu : MonoBehaviour
{
    public TMP_Text CountMoneyText;
    public Wallet wallet;
    public Button AdsBut;

    private void OnEnable()
    {
        CountMoneyText.text = wallet.CurrentMoney.ToString();
        AdsBut.gameObject.SetActive(true);

        YandexGame.RewardVideoEvent += Rewarded;
    }

    public void GiveMeX2Money()
    {
        wallet.CurrentMoney *= 2;
        CountMoneyText.text = wallet.CurrentMoney.ToString();
        wallet.Money += wallet.CurrentMoney / 2;
        PlayerPrefs.SetInt("money", wallet.Money);
        AdsBut.gameObject.SetActive(false);
    }

    // ������������ �� ������� �������� ������� � OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // ����������� ����� ��������� �������
    void Rewarded(int id)
    {
        // ���� ID = 1, �� ����� "+100 �����"
        if (id == 1)
            GiveMeX2Money();
    }

    // ����� ��� ������ ����� �������
    public void ExampleOpenRewardAd(int id)
    {
        // �������� ����� �������� ����� �������
        YandexGame.RewVideoShow(id);
    }
}
