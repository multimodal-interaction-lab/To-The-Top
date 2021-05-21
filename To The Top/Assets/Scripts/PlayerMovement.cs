﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach this script to the Leap Rig (Parent of Main Camera, Interaction Manager, etc).
 * Make the corresponding movement buttons also a child of the Leap Rig.
 * Causes left and right buttons to make player rotate around the table and up and down buttons to pan camera vertically.
 */

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Table for player to revolve around")]
    public GameObject table;
    public float rotationSpeed = 25f;
    public float moveSpeed = 0.75f;

    bool rotateLeftFlag;
    bool rotateRightFlag;
    bool moveUpFlag;
    bool moveDownFlag;


    void Start()
    {
        rotateLeftFlag = false;
        rotateRightFlag = false;
        moveUpFlag = false;
        moveDownFlag = false;
    }

    void FixedUpdate()
    {

        if (rotateLeftFlag)
        {
            transform.RotateAround(table.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (rotateRightFlag)
        {
            transform.RotateAround(table.transform.position, Vector3.up, rotationSpeed * Time.deltaTime * -1f);
        }

        if (moveUpFlag)
        {
            Vector3 moveVector = new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
            transform.position += moveVector;
        }

        if (moveDownFlag)
        {
            Vector3 moveVector = new Vector3(0f, moveSpeed * Time.deltaTime * -1f, 0f);
            transform.position += moveVector;
        }

    }


    // Functions called when button is pressed or unpressed
    // Flags start out false, turn true when button is pressed
    // and turn back to false when button is unpressed
    public void FlipRotateLeftFlag()
    {
        rotateLeftFlag = !rotateLeftFlag;
    }
    public void FlipRotateRightFlag()
    {
        rotateRightFlag = !rotateRightFlag;
    }
    public void FlipMoveUpFlag()
    {
        moveUpFlag = !moveUpFlag;
    }
    public void FlipMoveDownFlag()
    {
        moveDownFlag = !moveDownFlag;
    }

}
