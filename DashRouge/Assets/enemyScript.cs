using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using System;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    public PlayerBehaviour player;
    NavMeshAgent agent;

    public double health = 2;
    public double attackDamage = 3.1;
    public double aggroRange = 5;
    public double attackRange = 3;
    public double attackRate = 1;
    private double attackCountdown = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Move", 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            transform.tag = "dead";
            Destroy(gameObject);
        }
        double distance = Vector3.Distance(transform.position, player.transform.position);
        if (attackCountdown <= 0 && distance <= attackRange)
        {
            Attack();
            attackCountdown = 1 / attackRate;
        }
        attackCountdown -= Time.deltaTime;
    }

    //public void OnCollisionEnter(Collision col)
    //{
    //    if ((col.collider.tag == "player") && (new System.Random().Next(0, 100) > 50))
    //    {
    //        Debug.Log("ударило");
    //        col.collider.gameObject.GetComponent<PlayerBehaviour>().getDamage(enemyDamage);
    //    }
    //}

    public void takeDamage(double damage)
    {
        health -= damage;
    }

    void Attack()
    {
        Debug.Log("Enemy attacks for " + attackDamage);
        player.takeDamage(attackDamage);
    }

    
    void Move()
    {
        double distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= aggroRange)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
