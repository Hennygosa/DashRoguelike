using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public double health = 10;
    public double playerDamage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
    
    public void takeDamage(double damage)
    {
        health -= damage;
    }
}
