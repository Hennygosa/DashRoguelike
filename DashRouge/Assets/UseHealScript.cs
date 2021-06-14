using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseHealScript : MonoBehaviour
{
    public PlayerBehaviour player;
    public Text TextField;
    public Text TextField2;
    public void useHeal()
    {
        if (player.health < player.maxHealth)
        {
            if (player.bonuses[1] > 0)
            {
                player.bonuses[1]--;
                TextField.text = player.bonuses[1] + "/3";
                TextField2.text = player.bonuses[1] + "/3";
                if (player.maxHealth - player.health <= player.healValue)
                {
                    player.health = player.maxHealth;
                }
                else
                {
                    player.health += player.healValue;
                }
                player.hpBar.value = (float)player.health;
                player.textHealth.text = Mathf.Round((float)player.health).ToString() + '/' + player.maxHealth;
            }
        }
    }
}
