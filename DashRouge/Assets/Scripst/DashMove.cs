using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine.EventSystems;

public class DashMove : MonoBehaviour
{
    // private Queue<bool> isMoving = new Queue<bool>(); vosmozhno ubrat' nado, no poka pust' budet
    public PlayerBehaviour player;
    private double playerDamage = 1.2;
    public static float MaxSwipeLenght = 1f;
    private Vector3 touchPos, releasePos, swipeVector, lastPosition, currPos;
    private Rigidbody _rb;
    public LayerMask colMask;

    [SerializeField]
    private float minDistanceToSwipe = 2f;

    private ParticleSystem HitSparks;

    private Animator animator;

    private void Start()
    {
        HitSparks = GetComponentInChildren<ParticleSystem>();
        lastPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //isMoving.Enqueue(false);// vosmozhno ubrat' nado, no poka pust' budet
        //isMoving.Enqueue(false);//
        //isMoving.Enqueue(false);//
        //isMoving.Enqueue(false);//
        //isMoving.Enqueue(false);//
    }

    void Update()
    {
        if (player.speedBoostFlag)
        {
            player.timeLeft -= Time.deltaTime;
            if (player.timeLeft < 0)
            {
                player.Speed = 100f;
                player.speedBoostFlag = false;
                player.timeLeft = 0;
            }
        }
        if (_rb.velocity.magnitude > 1f) animator.SetBool("Dash", false);
        //if (transform.position != lastPosition)
        //{
        //isMoving.Enqueue(true);
        //isMoving.Dequeue();
        //Debug.Log("äâèãàåòñÿ");
        //}
        //else
        //{
        //isMoving.Enqueue(false);
        //isMoving.Dequeue();
        //Debug.Log("íå äâèãàåòñÿ");
        //}

        lastPosition = transform.position;
        Dash();
    }

    void Dash()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.point.z == 0)
                        {
                            goto LoopEnd;
                        }
                        touchPos = hit.point;
                    }
                    touchPos.y = 0;
                    break;

                case TouchPhase.Moved:
                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;
                    swipeVector = releasePos - touchPos;                   
                    animator.SetFloat("value", swipeVector.magnitude/5);
                    swipeVector.Normalize();
                    _rb.transform.LookAt(swipeVector * player.Speed);
                    break;

                case TouchPhase.Ended:

                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;
                    swipeVector = releasePos - touchPos;
                    if (swipeVector.magnitude > minDistanceToSwipe)
                    {
                        swipeVector.Normalize();
                        if (_rb.velocity.magnitude < 5f)
                        {
                            _rb.AddForce(swipeVector * player.Speed, ForceMode.Impulse);
                            System.Random random = new System.Random();
                            animator.SetInteger("RandomAnimation", random.Next(1, 4));
                            animator.SetBool("Dash", true);                
                            _rb.transform.LookAt(swipeVector * player.Speed);
                            animator.SetFloat("value", 0);
                        }
                        break;
                    }
                    //else
                    //{
                    //    //swipeVector.Normalize();
                    //    var point = (releasePos - transform.position);
                    //    _rb.MovePosition()
                    //    break;
                    //}
                    swipeVector = Vector3.zero;
                    break;
            }
        }
    LoopEnd:
        return;
    }

    void FixedUpdate()
    {
        //Vector3 postMove = Vector3.Lerp(transform.position, seckondPoint, Speed);
        //transform.position = postMove;
        //{
        //_rb.velocity = swipeVector;

    }



    public void OnCollisionEnter(Collision col)
    {
        switch (col.collider.tag)
        {
            //case "enemy":
            //    foreach (var i in isMoving)
            //    {
            //        if (i == true)
            //        {
            //            Debug.Log("ÄÂÈÃÀÅÒÑß ÐßÄÎÌ Ñ ÂÐÀÃÎÌ, õï âðàãà: " + col.collider.gameObject.GetComponent<enemyScript>().health);
            //            col.collider.gameObject.GetComponent<enemyScript>().takeDamage(playerDamage);
            //        }
            //    }
            //    break; vosmozhno ubrat' nado, no poka pust' budet

            case "wall":
                {
                    HitSparks.Play();
                }
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "heal")
        {
            if (player.health < player.maxHealth)
            {
                Destroy(other.gameObject);
                if (player.maxHealth - player.health <= player.healValue)
                {
                    player.health = player.maxHealth;
                }
                else
                {
                    player.health += player.healValue;
                }
                player.hpBar.value = (float)player.health;
                player.textHealth.text = Mathf.Round((float)player.health).ToString() + '/' + player.maxHealth;
            }
        }
    }
}