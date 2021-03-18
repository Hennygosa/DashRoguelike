using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public double health = 3.3;
    public double playerDamage = 1.2;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        playerDamage = 1.2;
    }

    public double getplayerDamage()
    {
        return playerDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            transform.tag = "dead";
            Destroy(gameObject);
            Debug.LogError("ÑÌÝÐÒÜ");
        }
    }
    public void OnCollisionEnter(Collision col)
    {

    }

    public void getDamage(double damage)
    {
        health -= damage;
    }
}
