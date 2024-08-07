using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyWorld3 : MonoBehaviour
{
    public Button ButtonPLayWorld3;
    public GameObject ClosedWorld3;

    public TMP_Text textMoneyCount;

    public int PriceForBuying = 30;
    public TMP_Text PriceForBuy;

    private int _money;

    public GameObject NewImage;
    public GameObject ClosedImage;

    private void Start()
    {
        PriceForBuy.text = PriceForBuying.ToString();

        if (PlayerPrefs.GetInt("World3") == 1)
        {
            ClosedWorld3.SetActive(false);
            ButtonPLayWorld3.interactable = true;
        }
        if(PlayerPrefs.GetInt("W3") == 1)
        {
            NewImage.SetActive(true);
            ClosedImage.SetActive(false);
        }
    }
    public void TryBuyWorld3()
    {
        _money = PlayerPrefs.GetInt("money");

        if (_money >= PriceForBuying)
        {
            _money -= PriceForBuying;
            PlayerPrefs.SetInt("money", _money);
            textMoneyCount.text = _money.ToString();
            ClosedWorld3.SetActive(false);
            ButtonPLayWorld3.interactable = true;
            PlayerPrefs.SetInt("World3", 1);
            NewImage.SetActive(true);
            ClosedImage.SetActive(false);
            PlayerPrefs.SetInt("W3", 1);
        }
    }
}