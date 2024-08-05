using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinnerMenu : MonoBehaviour
{
    public TMP_Text CountMoneyText;
    public Wallet wallet;
    public Button AdsBut;

    private void OnEnable()
    {
        CountMoneyText.text = wallet.Money.ToString();
        AdsBut.gameObject.SetActive(true);
    }

    public void GiveMeX2Money()
    {
        wallet.AddMoney(wallet.Money * 2);
        AdsBut.gameObject.SetActive(false);
    }
}
