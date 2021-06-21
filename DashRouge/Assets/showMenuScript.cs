using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showMenuScript : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel2;
    public PlayerBehaviour player;
    public void showMenu()
    {
        Panel.SetActive(true);
        Panel2.SetActive(false);
        player.UpdateMoney();
    }
}
