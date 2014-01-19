﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    bool mouseClickjudge;
    public float RotationSpeed = 100;

    //will store the original position 
    private Quaternion orgionalPosition;



    void Start()
    {
        orgionalPosition = transform.rotation;
    }

	// Update is called once per frame
	void Update () {

         if (Input.GetMouseButton(0))
        {
            
                //float x = -Input.GetAxis("Mouse X");
                //float y = -Input.GetAxis("Mouse Y");
            //float z = -Input.GetAxis("Mouse Z");
              //  float speed = 10;
               // Vector3 zk = new Vector3(0f, 0f, 1f);
                //transform.rotation = Quaternion.Euler(0f, 0f, 90f);
             // transform.rotation *= Quaternion.AngleAxis(y*speed, Vector3.left);
       }



        /*if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector2.up * speed * Time.deltaTime);
         
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector2.right * speed * Time.deltaTime);
        } 
        
         * */

        // Assign an absolute rotation using eulerAngles
	   
	
	}

    void OnMouseDrag()
    {
            //Debug.Log(Input.GetAxis("Mouse X"));
           // Debug.Log("above is the X axis");
           // Debug.Log(Input.GetAxis("Mouse Y"));
           // Debug.Log("Above is the Y axis");
            //transform.Rotate((Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime),
            //0, (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime)
            // , Space.World);
            //transform.Rotate(0, 0, -Input.GetAxis("Mouse X") * 70f * Time.deltaTime, Space.World);

            //float z = -Input.GetAxis("Mouse Z");
            //float speed = 10;
            // Vector3 zk = new Vector3(0f, 0f, 1f);
            // transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            float xDrag = Input.GetAxis("Mouse X");
            float yDrag = Input.GetAxis("Mouse Y");

            Vector3 pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.Rotate(0, 0, -pointer.z * Time.fixedDeltaTime * 10);
            float rawX = pointer.x;
            float rawY = pointer.y;

            //Debug.Log("xDrag: " + xDrag + " yDrag" + yDrag);

            float xLocal = (transform.position.x - rawX);
            float yLocal = (transform.position.y - rawY);

            Debug.Log("xDrag: " + xDrag + " Y: " + yDrag);
            //Vector3 Zvect = new Vector3(0f, 0f, zCoord);
            // transform.Rotate(0f, 0f, xLocal, Space.Self);
            //transform.Rotate(xDrag , 0 , yDrag );

            // Debug.Log("X: " + transform.position.x + " Y: " + transform.position.y);
            // Debug.Log("Transform: " + transform.localEulerAngles);
            if (transform.localEulerAngles.z > 355)
            {
                transform.localEulerAngles = new Vector3(0, 0, 10);
            }
        }



    

    //Will execute when the user stops rotating the object
    void OnMouseUp()
    {
        //The Z coordinate the user left the object at
        float UserZ = transform.localEulerAngles.z;

        //Snaps the piece to the appropiate position depending on where the user dropped it 
        if((UserZ < 45) || (UserZ <= 360 && UserZ >= 315))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if(UserZ >= 45 && UserZ < 135)
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        else if(UserZ >= 135 && UserZ < 225)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else if(UserZ >= 225 && UserZ < 315)
        {
            transform.localEulerAngles = new Vector3(0, 0, 270);
        }

        orgionalPosition = transform.rotation;
    }

    //The User stopped dragging the piece right above the piece, thus cancelling rotation
    void OnMouseUpAsButton()
    {
        transform.rotation = orgionalPosition;
    }

}
