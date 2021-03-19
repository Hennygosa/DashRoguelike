using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class DashMove : MonoBehaviour
{
    public enemyScript enemy;
    public PlayerBehaviour player;
    private double playerDamage = 1.2;


    public float maxSwipeLenght, speed;
    Vector3 touchPos, releasePos, swipeVector, seckondPoint;
    private Rigidbody rbody;
    private Vector3 startPosition = Vector3.zero;
    public Transform reflectedObject;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        startPosition = transform.position;
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
                    touchPos.y = 0; //y = 0 prevent flying 
                    break;

                case TouchPhase.Ended:

                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;
                    swipeVector = releasePos - touchPos;//movement vector
                    seckondPoint = transform.position + Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    break;
            }
        }
    }
    public void FixedUpdate()
    {
        Vector3 postMove = Vector3.Lerp(transform.position, seckondPoint, speed);
        transform.position = postMove;
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "wall")
        {
            transform.position = startPosition;
            seckondPoint = startPosition;
        }

        if (col.collider.tag == "enemy")
        {
            col.collider.gameObject.GetComponent<enemyScript>().takeDamage(playerDamage); 
        }
    }
}
