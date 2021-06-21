using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class BossScripts : MonoBehaviour
{
    public PlayerBehaviour player;
    NavMeshAgent agent;
    public List<DropItem> dropList;
    public GameObject boots;
    public GameObject claws;
    public GameObject jar;
    public GameObject headphones;
    public List<GameObject> SpawnObjects;

    public double health = 2;
    private double aggroRange = 30;
    private double attackRange = 20;
    private double attackRate = 0.5;
    private double attackCountdown = 0;
    private Animator animator;
    private bool костыл€мбаЌа”дар = false;
    void Start()
    {
        dropList.Add(new DropItem(jar, 0));
        dropList.Add(new DropItem(boots, 0));

        if (GameObject.FindGameObjectsWithTag("player").Length != 0)
            player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerBehaviour>();

        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Move", 0, .5f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        double distance;
        //if (health <= 0)
        //{
        //    transform.tag = "dead";
        //    Destroy(gameObject);
        //}

        if (GameObject.FindGameObjectsWithTag("player").Length != 0)
            distance = Vector3.Distance(transform.position, player.transform.position);
        else
            distance = attackRange + 1;
        if (attackCountdown <= 0 && distance <= attackRange)
        {
            Attack();
            attackCountdown = 1 / attackRate;
        }
        attackCountdown -= Time.deltaTime;
        if (distance < attackRange)
        {
            Stop();
            костыл€мбаЌа”дар = true;
        }
        else костыл€мбаЌа”дар = false;
    }

    public void takeDamage(double damage)
    {
        health -= damage;
        if (health <= 0)
        {
            transform.tag = "dead";
            Destroy(gameObject);
            CheckDrop();
        }
    }

    void Attack()
    {
        animator.Play("Crashing");
        System.Random random = new System.Random();
        Instantiate(SpawnObjects[random.Next(0, 3)], transform.position, Quaternion.identity);
    }


    void Move()
    {
        double distanceToPlayer;
        if (GameObject.FindGameObjectsWithTag("player").Length != 0)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= aggroRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                if (костыл€мбаЌа”дар == false)
                    animator.Play("Walk");
            }
        }
        else
        {
            Stop();
            animator.Play("Idle");
        }
    }

    void Stop()
    {
        agent.isStopped = true;
    }

    public void CheckDrop()
    {
        if (dropList.Count > 0)
        {
            int rnd = (int)Random.Range(0, 100);

            foreach (var item in dropList)
            {
                if (item.chance < rnd)
                {
                    item.CreateDropItem(gameObject.transform.position);
                    return;
                }
            }
        }
    }
}
