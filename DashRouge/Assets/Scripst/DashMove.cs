using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DashMove : MonoBehaviour
{
    private Rigidbody rb;
    public Camera cam;
    public float maxSwipeLenght;
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
                    Debug.Log("touch at" + touchPos);
                    break;

                case TouchPhase.Ended:
                    
                    if (Physics.Raycast(ray, out hit))
                    {
                        releasePos = hit.point;
                    }
                    releasePos.y = 0;   
                    swipeVector = releasePos - touchPos;   //movement vector
                    transform.position += Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    Debug.Log("release at" + releasePos);
                    break;
            }
        }
    }
    //private void FixedUpdate()
    //{
    //        Vector3 postMove = Vector3.Lerp(transform.position, transform.position += swipeVector, 0.125f);
    //        transform.position = postMove;
    //}
}
