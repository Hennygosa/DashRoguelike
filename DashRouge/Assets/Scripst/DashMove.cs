using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DashMove : MonoBehaviour
{
    private float deltaX, deltaY;
    private Rigidbody2D rb;
    public Camera cam;
    private Vector2 landPoint;
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = cam.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    landPoint = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
                    break;

                case TouchPhase.Ended:
                    rb.MovePosition(landPoint);
                    break;
            }
        }
    }
}
