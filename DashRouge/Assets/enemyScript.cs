using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using System;

public class enemyScript : MonoBehaviour
{
    PlayerBehaviour player;
    public double health = 2;
    public double enemyDamage = 1.1;
    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        enemyDamage = 1.1;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            transform.tag = "dead";
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if ((col.collider.tag == "player") && (new System.Random().Next(0, 100) > 50))
        {
            Debug.Log("ударило");
            col.collider.gameObject.GetComponent<PlayerBehaviour>().getDamage(enemyDamage);
        }
    }

    public void getDamage(double damage)
    {
        health -= damage;
    }
}
