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

    float traveledDistance = 0f;
    public static float Speed = 100f;
    public static float MaxSwipeLenght = 1f;
    public float DashTime = 1f;
    private Vector3 touchPos, releasePos, swipeVector, seckondPoint, initialPosition, lastPosition;
    private Rigidbody _rb;
    public LayerMask colMask;


    private void Start()
    {

        lastPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        isMoving.Enqueue(false);// пока 5 кадра пусть будет
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
            //Debug.Log("двигается");
        }
        else
        {
            isMoving.Enqueue(false);
            isMoving.Dequeue();
            //Debug.Log("не двигается");
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

            //read swipe coordinates
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (Physics.Raycast(ray, out hit))
                    {
                        touchPos = hit.point;
                    }
                    touchPos.y = 0;
                    break;

                case TouchPhase.Ended:

                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;
                    swipeVector = (releasePos - touchPos).normalized;
                    //seckondPoint = transform.position + Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    break;
            }
            //swipeVector = (releasePos - touchPos).normalized; 
        }
        else
        {
            //move
            _rb.velocity = swipeVector * Speed;
            //StopDashByTime(dashTime);
            StopDashByDistance(MaxSwipeLenght);
        }
    }

    private void StopDashByTime(float time)
    {
        Debug.Log(time);

        time -= Time.deltaTime;
        if (time <= 0)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void StopDashByDistance(float distance) //CANT MOVE AFTER FIRST STOP !?
    {
        Debug.Log("прошел: " + traveledDistance);
        // Debug.Log("current position: " + transform.position);
        Debug.Log("максимум: " + distance);
        if (traveledDistance >= distance)  //if moving && distance > max
        {
            _rb.velocity = Vector3.zero;//stop
            traveledDistance = 0f;
            Debug.Log("velocity: " + _rb.velocity);
            Debug.Log("traveled distance: " + traveledDistance);
        }
        if (_rb.velocity != Vector3.zero)
        {
            traveledDistance = Math.Abs(Vector3.Distance(initialPosition, transform.position));
            //Debug.Log("traveled distance зашло в цикл дистанция= "+ traveledDistance);
        }
        if (_rb.velocity == Vector3.zero)
        {
            initialPosition = transform.position; //remember position
            Debug.Log("initial position: " + initialPosition);
        }
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
            case "enemy":
                foreach (var i in isMoving)
                {
                    if (i == true)
                    {
                        Debug.Log("ДВИГАЕТСЯ РЯДОМ С ВРАГОМ, хп врага: " + col.collider.gameObject.GetComponent<enemyScript>().health);
                        col.collider.gameObject.GetComponent<enemyScript>().takeDamage(playerDamage);
                    }
                }
                break;

            case "wall":
                {
                    foreach (ContactPoint contact in col.contacts)
                    {
                        GameObject sparks = GameObject.Find("Sparks");
                        ParticleSystem emmiter = sparks.GetComponent<ParticleSystem>();
                        sparks.transform.position = contact.point;
                        emmiter.enableEmission = true;
                        StartCoroutine(stopSparkles());

                        IEnumerator stopSparkles()
                        {
                            yield return new WaitForSeconds(1f);
                            emmiter.enableEmission = false;
                        }
                    }
                }
                break;
        }
    }

}
