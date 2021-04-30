using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public double health = 10;
    public static double playerDamage = 10;
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
    private void OnCollisionEnter(Collision other)
    {
        GameObject enemy = other.gameObject;
        if (enemy.tag == "enemy")
        {
            enemy.GetComponent<enemyScript>().takeDamage(playerDamage);
            Debug.Log(enemy.GetComponent<enemyScript>().health);
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
