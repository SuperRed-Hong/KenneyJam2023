using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using UnityEditor.UIElements;
using TMPro;

public class PlayerStateBar : MonoBehaviour
{
    public PlayerStatus player;
    public Image healthImage;
    public Image healthDelayImage;
    public Image waterImage;
    public float delaySpeed;
    public TextMeshProUGUI money; 
    private void Awake()
    {
        
    }
    private void Update()
    {
        
        OnHealthChange();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OnHealthChange()
    {
        var hpPersentage = player.currentHP / player.maxHP;
        var waterPersentage = player.currentWater / player.maxWater;
        healthImage.fillAmount = hpPersentage;
        waterImage.fillAmount = waterPersentage;
        money.text = player.money.ToString();
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime * delaySpeed;
        }
        
    }

}
