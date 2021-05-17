using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class DashMove : MonoBehaviour
{
    private Queue<bool> isMoving = new Queue<bool>();
    private double playerDamage = 1.2;

    public static float Speed = 100f;
    public static float MaxSwipeLenght = 1f;
    private Vector3 touchPos, releasePos, swipeVector, lastPosition;
    private Rigidbody _rb;
    public LayerMask colMask;

    private ParticleSystem HitSparks;

    private void Start()
    {
        HitSparks = GetComponentInChildren<ParticleSystem>();
        lastPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        isMoving.Enqueue(false);// ïîêà 5 êàäðà ïóñòü áóäåò
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
            //Debug.Log("äâèãàåòñÿ");
        }
        else
        {
            isMoving.Enqueue(false);
            isMoving.Dequeue();
            //Debug.Log("íå äâèãàåòñÿ");
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
                    if (_rb.velocity.magnitude < 5f)
                    {
                        _rb.AddForce(swipeVector * Speed, ForceMode.Impulse);
                    }
                    break;
            }
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
                        Debug.Log("ÄÂÈÃÀÅÒÑß ÐßÄÎÌ Ñ ÂÐÀÃÎÌ, õï âðàãà: " + col.collider.gameObject.GetComponent<enemyScript>().health);
                        col.collider.gameObject.GetComponent<enemyScript>().takeDamage(playerDamage);
                    }
                }
                break;

            case "wall":
                {
                    HitSparks.Play();
                }
                break;
        }
    }

}