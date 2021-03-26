using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public double health = 10;
    public static double playerDamage = 1;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("PlayerSpawnPoint");
        transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            transform.tag = "dead";
            Destroy(gameObject);
            Debug.LogError("СМЭРТЬ");
        }
    }
    
    public void takeDamage(double damage)
    {
        health -= damage;
        Debug.Log("получил урон игрок, хп =" + health);
    }
}
