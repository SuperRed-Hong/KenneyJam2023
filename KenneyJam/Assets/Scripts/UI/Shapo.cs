using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shapo : MonoBehaviour
{
    public PlayerStatus status;
    public TextMeshProUGUI priceString;
    public int price;
    public GameObject buyButton;
    public GameObject soldOutImage;
    public GameObject shop;

    private void Awake()
    {
        priceString.text = price.ToString();
    }

    public void Buy()
    {
        if(status.money > price)
        {
            //更新 数值
            status.money -= price;
            soldOutImage.SetActive(true);

            buyButton.SetActive(false);
        }
    }
    public void returnButton()
    {
        shop.SetActive(false);
    }
}
