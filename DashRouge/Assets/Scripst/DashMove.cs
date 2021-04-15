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
    
    public float Speed = 1f;
    public float MaxSwipeLenght = 10f;
    public float DashTime = 1f;
    private Vector3 touchPos, releasePos, swipeVector, seckondPoint, initialPosition, lastPosition;
    private Rigidbody _rb;
    public LayerMask colMask;
    

    private void Start()
    {
        
        lastPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        isMoving.Enqueue(false);// ���� 5 ����� ����� �����
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
            //Debug.Log("���������");
        }
        else
        {
            isMoving.Enqueue(false);
            isMoving.Dequeue();
            //Debug.Log("�� ���������");
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
                    //seckondPoint = transform.position + Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    break;
            }
            swipeVector = (releasePos - touchPos).normalized; 
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
        float traveledDistance = 0f;
        Debug.Log("distance: " + traveledDistance);
        Debug.Log("current position: " + _rb.position);

        if (traveledDistance > distance)  //if moving && distance > max
        {
            _rb.velocity = Vector3.zero;//stop
            Debug.Log("velocity: " + _rb.velocity);
            Debug.Log("traveled distance: " + traveledDistance);
        }
        //while (_rb.velocity != Vector3.zero)
        //    traveledDistance = Vector3.Distance(initialPosition, _rb.position);
        if (_rb.velocity == Vector3.zero)
        {
            initialPosition = _rb.position; //remember position
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
                foreach(var i in isMoving)
                {
                    if (i == true)
                    {
                        Debug.Log("��������� ����� � ������, �� �����: " + col.collider.gameObject.GetComponent<enemyScript>().health);
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
