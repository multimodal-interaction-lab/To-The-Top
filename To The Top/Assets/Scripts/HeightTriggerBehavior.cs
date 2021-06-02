using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTriggerBehavior : MonoBehaviour
{
    float lastTowerHeight;              // measures the current height of the tower
    float lastTowerHeightNorm;          // equal to the last height, but passed through our normalization formula for display
    float planeSpeed = 1f;              // speed that the height plane moves by
    Vector3 planeBase;                  // coordinates for the plane's home position
    bool active;                        // the plane will only move down while active is true        
    bool foundTop;                      // will be false while the scanner is looking for the top of the tower 
                                        // and becomes true when it collides with a blockInPlay
    float playerStartHeight;            // height of the table where player starts
    float baseHeightError = 0.01309f;   // an error value for the height of the table


    void Start()
    {
        transform.localPosition = new Vector3(0, 200, 0);
        planeBase = transform.position;     // Saves initial position as the 'base'
        active = true;
        foundTop = false;
        playerStartHeight = transform.parent.position.y;
    }

    void FixedUpdate()
    {
        if (active)
        {
            transform.position += planeSpeed * (Vector3.down * Time.deltaTime); // Moves downwards at a constant speed
        }
    }

    // Returns the trigger to its home position
    void toBase()
    {
        transform.position = planeBase;
        foundTop = false;
        // Debug.Log("lastTowerHeight for loop: " + lastTowerHeight); // DEBUG
    }

    // Detects collisions
    void OnTriggerEnter(Collider collision)
    {
        //Check for collision with a block that's been placed by a player and isn't in their hand
        if (collision.gameObject.tag == "BlockInPlay")
        {
            // If this is the first block that we've found, we'll set it as the top
            if (!foundTop)
            {
                foundTop = true;
                lastTowerHeight = getTowerHeight();
                lastTowerHeightNorm = getHeightNorm();
            }
        }
        else if (collision.gameObject.tag == "Table")
        {
            // If we haven't hit the top yet, the table will be the top.
            if (!foundTop)
            {
                foundTop = true;
                lastTowerHeight = getTowerHeight(); // DEBUG
                lastTowerHeightNorm = getHeightNorm();
                //lastTowerHeight = 0f;
            }
            toBase();
        }
        //Debug.Log("Last tag for collision: " + collision.gameObject.tag);
        //toBase(); // DEBUG
    }

    float getTowerHeight()
    {
        float displacement = planeBase.y - transform.position.y;
        //Debug.Log("planeBase.y: " + planeBase.y);
        //Debug.Log("transform.position.y: " + transform.position.y);
        //Debug.Log("Displacement: " + displacement + ", heightFromTable: " + heightFromTable);
        
        return (planeBase.y - displacement - playerStartHeight);
    }

    float getHeightNorm()
    {
        return (float)(System.Math.Round((lastTowerHeight-baseHeightError)*100,2));
    }

    public float readTowerHeight()
    {
        return lastTowerHeight;
    }

    public float readTowerHeightNorm()
    {
        return lastTowerHeightNorm;
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
