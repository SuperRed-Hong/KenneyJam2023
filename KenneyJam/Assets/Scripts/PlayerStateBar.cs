using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using UnityEditor.UIElements;

public class PlayerStateBar : MonoBehaviour
{
    //public Character player
    public Image healthImage;
    public Image healthDelayImage;
    public Image waterImage;
    public float delaySpeed;
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime * delaySpeed;
        }
        /*        if (isRecovering)
                {
                    float persentage = currentCharacter.currentPower / currentCharacter.maxPower;
                    powerImage.fillAmount = persentage;
                    if (persentage >= 1)
                    {
                        isRecovering = false;
                        return;
                    }
                }*/

    }
    /// <summary>
    /// 
    /// </summary>
/*    public void OnHealthChange()
    {
        var hpPersentage = player.currentHealth/player.maxHealth;
        var waterPersentage = player.currentWater/player.maxWater;
        healthImage.fillAmount = hpPersentage;
        waterImage.fillAmount = waterPersentage;
    }*/

}
