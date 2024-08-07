using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyWorld2 : MonoBehaviour
{
    public Button ButtonPLayWorld2;
    public GameObject ClosedWorld2;

    public TMP_Text textMoneyCount;
    public TMP_Text PriceForBuy;

    public int PriceForBuying = 10;
    private int _money;

    private void Start()
    {
        PriceForBuy.text = PriceForBuying.ToString();

        if (PlayerPrefs.GetInt("World2") == 1)
        {
            ClosedWorld2.SetActive(false);
            ButtonPLayWorld2.interactable = true;
        }
    }
    public void TryBuyWorld2()
    {
        _money = PlayerPrefs.GetInt("money");
        if (_money >= PriceForBuying)
        {
            _money -= PriceForBuying;
            PlayerPrefs.SetInt("money", _money);
            textMoneyCount.text = _money.ToString();
            ClosedWorld2.SetActive(false);
            ButtonPLayWorld2.interactable = true;
            PlayerPrefs.SetInt("World2", 1);
        }
    }
}
