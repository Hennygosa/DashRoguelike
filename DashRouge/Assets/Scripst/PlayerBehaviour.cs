using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Slider hpBar;
    public Text textHealth;
    public double health = 50;
    public static double playerDamage = 10;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        textHealth.text = health.ToString();
        hpBar.maxValue = (float)health;
        hpBar.value = (float)health;//Изменить для сохранения хп между уровнями
        spawnPoint = GameObject.Find("PlayerSpawnPoint");
        transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
 
    public void takeDamage(double damage)
    {      
        health -= damage;
        hpBar.value = (float)health;
        textHealth.text = Mathf.Round((float)health).ToString() + '/' + hpBar.maxValue;
        if (health < 0)
        {
            transform.tag = "dead";
            Destroy(gameObject);
            Debug.LogError("СМЭРТЬ");
        }

        //Debug.Log("получил урон игрок, хп =" + health);
    }
    private void OnCollisionEnter(Collision other)
    {
        GameObject enemy = other.gameObject;
        if (enemy.tag == "enemy")
        {
            enemy.GetComponent<enemyScript>().takeDamage(playerDamage);

            //Debug.Log(enemy.GetComponent<enemyScript>().health);
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
