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
    private Vector3 lastPosition;
    private Queue<Vector3> kostil = new Queue<Vector3>();

    public float maxSwipeLenght, speed;
    private Vector3 touchPos, releasePos, swipeVector, seckondPoint, startPosition; //= Vector3.zero
    private Rigidbody rbody;
    public Transform reflectedObject;

    private void Start()
    {
        speed = 0.05f;
        rbody = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        isMoving.Enqueue(false);// пока 5 кадра пусть будет
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//


        kostil.Enqueue(Vector3.zero);// костыль
        kostil.Enqueue(Vector3.zero);// костыль
        kostil.Enqueue(Vector3.zero);// костыль
        kostil.Enqueue(Vector3.zero);// костыль
    }

    void Update()
    {
        kostil.Enqueue(transform.position);// костыль
        kostil.Dequeue();// костыль
        if (transform.position != lastPosition)
        {
            isMoving.Enqueue(true);
            isMoving.Dequeue();
            Debug.Log("двигаетс€");
        }
        else
        {
            isMoving.Enqueue(false);
            isMoving.Dequeue();
            Debug.Log("не двигаетс€");
        }

        lastPosition = transform.position;
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
                    touchPos.y = 0;  //y = 0 prevent flying 
                    break;

                case TouchPhase.Ended:

                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;
                    swipeVector = releasePos - touchPos; //movement vector
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
        switch (col.collider.tag)
        {
            case "wall":
                var kostil2 = kostil.Dequeue();// костыль
                transform.position = kostil2;// костыль
                seckondPoint = kostil2;// костыль
                kostil.Clear();// костыль
                kostil.Enqueue(transform.position);// костыль
                kostil.Enqueue(transform.position);// костыль
                kostil.Enqueue(transform.position);// костыль
                kostil.Enqueue(transform.position);// костыль

                break;

            case "enemy":
                foreach(var i in isMoving)
                {
                    if (i == true)
                    {
                        Debug.Log("ƒ¬»√ј≈“—я –яƒќћ — ¬–ј√ќћ, хп врага: " + col.collider.gameObject.GetComponent<enemyScript>().health);
                        col.collider.gameObject.GetComponent<enemyScript>().takeDamage(playerDamage);
                    }
                }
                break;
        }     
    }
}
