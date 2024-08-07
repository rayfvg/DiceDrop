using TMPro;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private int _money;
    public TMP_Text textMoney;
    public Wallet wallet;

    private void Start()
    {
        _money = PlayerPrefs.GetInt("money");
    }
    public void ResetAllSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public void AddMyMany()
    {
        wallet.AddMoney(2);
        textMoney.text = PlayerPrefs.GetInt("money").ToString();
    }
}
