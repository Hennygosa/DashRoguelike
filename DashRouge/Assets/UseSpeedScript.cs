using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSpeedScript : MonoBehaviour
{
    public PlayerBehaviour player;
    public Text TextField;
    public void useSpeed()
    {
        if (player.bonuses[0] > 0)
        {
            player.bonuses[0]--;
            TextField.text = player.bonuses[0] + "/3";

            player.timeLeft = 15;
            player.Speed += 80;
            player.speedBoostFlag = true;
        }
    }
}
