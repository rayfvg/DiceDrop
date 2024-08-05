using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Money;

    public void AddMoney(int value)
    {
        Money += value;
        PlayerPrefs.SetInt("money", Money);
    }
}
