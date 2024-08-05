using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TMP_Text MoneyText;
    private int _coin;

    private void Start()
    {
        _coin = PlayerPrefs.GetInt("money");
        MoneyText.text = _coin.ToString();
    }
}
