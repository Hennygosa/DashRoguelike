using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DashMove : MonoBehaviour
{
    public float maxSwipeLenght, speed;
    Vector3 touchPos, releasePos, swipeVector, seckondPoint;
    private Rigidbody rbody;
    bool nigger;
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
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
                    swipeVector = releasePos - touchPos;//movement vector
                    seckondPoint = transform.position + Vector3.ClampMagnitude(swipeVector, maxSwipeLenght);
                    Debug.Log("release at" + releasePos);
                    break;
            }        
        }    
    }
    public void FixedUpdate()
    {
        Vector3 postMove = Vector3.Lerp(transform.position, seckondPoint, speed);
        transform.position = postMove;
    }
    public void OnCollisionEnter (Collision col)
    {
        if (col.collider.name == "wall")
        { 

        }
    }
}
