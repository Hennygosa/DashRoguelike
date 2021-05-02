using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class DashMove : MonoBehaviour
{
    private Queue<bool> isMoving = new Queue<bool>();
    public enemyScript enemy;
    public PlayerBehaviour player;
    private double playerDamage = 1.2;
    private ParticleSystem HitSparks;

    Vector3 lastPosition;
    public static float Speed = 100f;
    public float maxSwipeLenght;
    private float dashTime;
    public static float StartDashTime = 0.5f;
    private Vector3 touchPos, releasePos, swipeVector, seckondPoint;
    private Rigidbody _rb;
    public LayerMask colMask;

    private void Start()
    {
        HitSparks = GetComponentInChildren<ParticleSystem>();
        lastPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        dashTime = StartDashTime;
        isMoving.Enqueue(false);// 
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            isMoving.Enqueue(true);
            isMoving.Dequeue();
        }
        else
        {
            isMoving.Enqueue(false);
            isMoving.Dequeue();
        }

        lastPosition = transform.position;
        Dash();
    }

    void Dash()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (Physics.Raycast(ray, out hit))
                    {
                        touchPos = hit.point;  //touch position
                    }
                    touchPos.y = 0;  //y = 0 prevent flying 
                    break;

                case TouchPhase.Ended:

                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;
                    swipeVector = (releasePos - touchPos).normalized; //movement vector
                    Debug.Log(swipeVector);
                    //seckondPoint = transform.position + Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    break;
            }
        }

        else
        {
            if (dashTime <= 0)
            {
                swipeVector = Vector3.zero;
                //Speed = 0;
                dashTime = StartDashTime;
                //_rb.AddForce(Vector3.zero);
                //_rb.velocity = Vector3.zero;
            }
            else
            {
                //Speed = 1f;
                dashTime -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        //Vector3 postMove = Vector3.Lerp(transform.position, seckondPoint, Speed);
        //transform.position = postMove;
        //{
        _rb.velocity = swipeVector * Speed;
        //_rb.AddForce(swipeVector * Speed, ForceMode.VelocityChange);
    }

    public void OnCollisionEnter(Collision col)
    {
        switch (col.collider.tag)
        {
            case "enemy":
                {
                    HitSparks.Play();
                    foreach (var i in isMoving)
                    {
                        if (i == true)
                        {
                            Debug.Log("ÄÂÈÃÀÅÒÑß ÐßÄÎÌ Ñ ÂÐÀÃÎÌ, õï âðàãà: " + col.collider.gameObject.GetComponent<enemyScript>().health);
                            col.collider.gameObject.GetComponent<enemyScript>().takeDamage(playerDamage);
                        }
                    }
                }
                break;

            case "Respawn":
                {
                    HitSparks.Play();
                }
                break;
        }
    }

}
