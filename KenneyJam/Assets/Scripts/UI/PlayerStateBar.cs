using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateBar : MonoBehaviour
{
    public PlayerStatus player;
    public Image healthImage;
    public Image healthDelayImage;
    public Image waterImage;
    public float delaySpeed;
    public TextMeshProUGUI money;
    public GameObject water;

    private void Update()
    {
        
        OnHealthChange(GameObject.FindGameObjectWithTag("Home").GetComponent<BaseController>().HP / 1500.0f);
        if(player.currentWater <= 0)
        {
            water.SetActive(true);
        }
        else
        {
            water.SetActive(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnHealthChange(float hpPersentage)
    {
        float waterPersentage = player.currentWater / player.maxWater;
        healthImage.fillAmount = hpPersentage;
        waterImage.fillAmount = waterPersentage;
        money.text = player.money.ToString();
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime * delaySpeed;
        }


    }

}
