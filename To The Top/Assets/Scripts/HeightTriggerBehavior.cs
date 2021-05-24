using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTriggerBehavior : MonoBehaviour
{
    public GameObject planeZero;
    public float towerHeight;
    public float planeSpeed = 1.0f;
    public Vector3 planeBase;
    public bool active;
    public bool foundTop;

    void Start()
    {
        planeBase = transform.position;     // Saves initial position as the 'base'
        active = true;
        foundTop = false;
        planeZero = GameObject.Find("HeightZeroPlane");
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.position += Vector3.down * planeSpeed * Time.deltaTime; // Moves downwards at a constant speed
        }
    }

    // Returns the trigger to its base position
    void toBase()
    {
        transform.position = planeBase;
        foundTop = false;
    }

    // Detects collisions
    void OnCollisionEnter(Collision collision)
    {
        /*
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
        */

        // Check if collided with HeightZeroPlane at surface of table
        if (collision.gameObject.name == "HeightZeroPlane")
        {
            // If top hasn't been found, then we'll call the table's height (0) the top of the stack 
            if(!foundTop)
            {
                foundTop = true;
                towerHeight = 0f;
            }
            toBase();
        }
        toBase(); // DEBUG
    }

    public float getStackHeight()
    {
        // float tempHeight = 0f;

        return 0f;
    }
    
    /*
    public void pauseScan()
    {
        active = false;
    }

    public void playScan()
    {
        active = true;
    }
    */
}
