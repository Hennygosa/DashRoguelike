using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healButtonScript : MonoBehaviour
{
    public PlayerBehaviour player;
    public Text TextField;
    public Text TextField2;
    // Start is called before the first frame update

    public void ChangeText()
    {
        int value = player.bonuses[1];
        if (player.Gold >= 3 && player.bonuses[1] < 3)
        {
            player.bonuses[1]++;
            TextField.text = player.bonuses[1] + "/3";
            TextField2.text = player.bonuses[1] + "/3";
            player.Gold -= 3;
            player.UpdateMoney();
        }
    }
}
