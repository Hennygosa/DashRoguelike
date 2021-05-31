using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedButtonScript : MonoBehaviour
{
    public PlayerBehaviour player;
    public Text TextField;
    public Text TextField2;
    // Start is called before the first frame update

    public void ChangeText()
    {
        int value = player.bonuses[0];
        if (player.Gold >= 3 && player.bonuses[0] < 3)
        {
            player.bonuses[0]++;
            TextField.text = player.bonuses[0] + "/3";
            TextField2.text = player.bonuses[0] + "/3";
            player.Gold -= 3;
        }
    }
}
