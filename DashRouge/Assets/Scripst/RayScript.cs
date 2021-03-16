using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public Transform player, pointer;
    Vector3 postEnd, postMove;
    bool touchflag = false;
    void Update()
    {      
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {               
                    pointer.position = hit.point;
                }
                touchflag = false
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchflag = true;            
            }         
        }
    }
    private void FixedUpdate()
    {
        if (touchflag)
        {
            postEnd = new Vector3(pointer.position.x, pointer.position.y, pointer.position.z);
            postMove = Vector3.Lerp(player.position, postEnd, 0.125f);
            player.position = postMove;
        }
    }
}
