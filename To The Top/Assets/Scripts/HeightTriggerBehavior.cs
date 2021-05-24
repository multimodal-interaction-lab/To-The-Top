using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTriggerBehavior : MonoBehaviour
{
    float lastTowerHeight;      // measures the current height of the tower
    float planeSpeed = 1f;      // speed that the height plane moves by
    Vector3 planeBase;          // coordinates for the plane's home position
    bool active;         // the plane will only move down while active is true        
    bool foundTop;              // will be false while the scanner is looking for the top of the tower 
                                // and becomes true when it collides with a blockInPlay
    float heightFromTable = 4f; // the height difference between the surface of the table and the home base

    void Start()
    {
        planeBase = transform.position;     // Saves initial position as the 'base'
        active = true;
        foundTop = false;
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
        Debug.Log("lastTowerHeight for loop: " + lastTowerHeight); // DEBUG
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
            }
        }
        else if (collision.gameObject.tag == "Table")
        {
            // If we haven't hit the top yet, the table will be the top.
            if (!foundTop)
            {
                foundTop = true;
                //lastTowerHeight = getTowerHeight(); // DEBUG
                lastTowerHeight = 0f;
            }
            toBase();
        }
        Debug.Log("Last tag for collision: " + collision.gameObject.tag);
        //toBase(); // DEBUG
    }

    float getTowerHeight()
    {
        float displacement = planeBase.y - transform.position.y;
        Debug.Log("Displacement: " + displacement + ", heightFromTable: " + heightFromTable);
        return heightFromTable - displacement;
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
