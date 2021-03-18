using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DashMove : MonoBehaviour
{
    private Rigidbody rb;
    public Camera cam;
    Vector3 touchPos;
    Vector3 releasePos;
    Vector3 swipeVector;
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Dash();
    }

    void Dash()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        touchPos = hit.point;  //touch position
                    }
                    touchPos.y = 0; //y = 0 prevent flying 
                    Debug.Log("touch at" + touchPos);
                    break;

                case TouchPhase.Ended:
                    Ray ray1 = Camera.main.ScreenPointToRay(touch.position);                  
                    RaycastHit hit2;
                    if (Physics.Raycast(ray1, out hit2))
                    {
                        releasePos = hit2.point;
                    }
                    releasePos.y = 0;   
                    swipeVector = releasePos - touchPos;   //movement vector
                    transform.position += swipeVector;
                    Debug.Log("release at" + releasePos);
                    break;
            }
        }
    }
}
