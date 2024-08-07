using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Money;

    private void Awake()
    {
        Money = PlayerPrefs.GetInt("money");
    }

    public void AddMoney(int value)
    {
        Money += value;
        PlayerPrefs.SetInt("money", Money);
    }
}
