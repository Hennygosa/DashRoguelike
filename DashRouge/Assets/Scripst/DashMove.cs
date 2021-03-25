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
    private Vector3 touchPos, releasePos, swipeVector, seckondPoint, startPosition, oldPos, maximaseSwipe; //= Vector3.zero
    private Rigidbody rbody;
    public Transform reflectedObject;


    public bool bounce;
    List<Vector3> movePoints = new List<Vector3>();
    public int Raycount = 2;
    int flag = 0;
    public LayerMask colMask;

    private void Start()
    {
        speed = 0.3f;
        rbody = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        isMoving.Enqueue(false);// пока 5 кадра пусть будет
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//
        isMoving.Enqueue(false);//


        //kostil.Enqueue(Vector3.zero);// костыль
        //kostil.Enqueue(Vector3.zero);// костыль
        //kostil.Enqueue(Vector3.zero);// костыль
        //kostil.Enqueue(Vector3.zero);// костыль
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
                    maximaseSwipe = Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    if (bounce)
                        CastRay(transform.position, maximaseSwipe, oldPos);
                    else NoBounceMetod();
                    break;
            }
        }
    }

    private void NoBounceMetod()
    {
        oldPos = transform.position;
        seckondPoint = transform.position + maximaseSwipe;

        Debug.DrawRay(transform.position, maximaseSwipe, Color.red);
        Ray objectRay = new Ray(transform.position, maximaseSwipe);
        RaycastHit hit2;
        if (Physics.Raycast(objectRay, out hit2, maximaseSwipe.magnitude, colMask))
        {
            seckondPoint = hit2.point - maximaseSwipe.normalized / 10;
            Debug.Log("hit");
        }
        else
        {
            seckondPoint = oldPos + maximaseSwipe;
            Debug.Log("no");
        }
    }

    void CastRay(Vector3 pos, Vector3 dir, Vector3 oldpose)
    {
        for (int i = 0; i < Raycount; i++)
        {
            Ray objectRay2 = new Ray(pos, dir);
            RaycastHit hit1;
            if (Physics.Raycast(objectRay2, out hit1, ((oldPos + dir) - pos).magnitude))
            {
                Debug.DrawLine(pos, hit1.point, Color.red);
                pos = hit1.point - dir.normalized / 10;
                dir = Vector3.Reflect(dir, hit1.normal);
                oldPos = hit1.point;
                movePoints.Add(pos);
                Debug.Log(movePoints.Count);
            }
            else
            {
                Debug.DrawRay(pos, (oldPos + dir) - pos, Color.blue);
                Debug.Log(movePoints.Count);
                movePoints.Add(oldPos + dir);
                break;
            }
        }
    }


    public void FixedUpdate()
    {
        if (movePoints.Count == 1)
        {
            seckondPoint = movePoints[0];
            movePoints.Clear();
        }
        if (movePoints.Count != 0)
        {
            seckondPoint = movePoints[flag];
            if (transform.position == seckondPoint) flag++;
            if (transform.position == movePoints[movePoints.Count - 1])
            {
                flag = 0;
                movePoints.Clear();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, seckondPoint, speed);
    }
    public void OnCollisionEnter(Collision col)
    {
        switch (col.collider.tag)
        {
            //case "wall":
            //    var kostil2 = kostil.Dequeue();// костыль
            //    transform.position = kostil2;// костыль
            //    seckondPoint = kostil2;// костыль
            //    kostil.Clear();// костыль
            //    kostil.Enqueue(transform.position);// костыль
            //    kostil.Enqueue(transform.position);// костыль
            //    kostil.Enqueue(transform.position);// костыль
            //    kostil.Enqueue(transform.position);// костыль

            //    break;

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
