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

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
        // Если ID = 1, то выдаём "+100 монет"
        if (id == 1)
            GiveMeX2Money();
    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
}
