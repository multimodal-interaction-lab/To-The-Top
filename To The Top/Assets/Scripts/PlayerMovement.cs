using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach this script to the Leap Rig (Parent of Main Camera, Interaction Manager, etc).
 * Make the corresponding movement buttons also a child of the Leap Rig.
 * Causes left and right buttons to make player rotate around the table and up and down buttons to pan camera vertically.
 */

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Point for player to revolve around which should be center of table")]
    public GameObject pointToCircle;
    public float rotationSpeed = 25f;
    public float moveSpeed = 0.75f;

    bool rotateLeftFlag;
    bool rotateRightFlag;
    bool moveUpFlag;
    bool moveDownFlag;

    Vector3 startingPosition;


    void Start()
    {
        rotateLeftFlag = false;
        rotateRightFlag = false;
        moveUpFlag = false;
        moveDownFlag = false;
        startingPosition = transform.position;
    }

    void FixedUpdate()
    {

        if (rotateLeftFlag)
        {
            transform.RotateAround(pointToCircle.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (rotateRightFlag)
        {
            transform.RotateAround(pointToCircle.transform.position, Vector3.up, rotationSpeed * Time.deltaTime * -1f);
        }

        if (moveUpFlag)
        {
            Vector3 moveVector = new Vector3(0f, moveSpeed * Time.deltaTime, 0f);

            float currentY = transform.position.y;
            currentY += moveVector.y;

            if (currentY <= 5)
            {
                transform.position += moveVector;
            }

        }

        if (moveDownFlag)
        {
            Vector3 moveVector = new Vector3(0f, moveSpeed * Time.deltaTime * -1f, 0f);
            float currentY = transform.position.y;
            currentY += moveVector.y;

            if (currentY >= startingPosition.y)
            {
                transform.position += moveVector;
            }
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
