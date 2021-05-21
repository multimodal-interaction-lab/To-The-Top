using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        Debug.Log("rotateLeftFlag: " + rotateLeftFlag);
        Debug.Log("rotateRightFlag: " + rotateRightFlag);
        Debug.Log("moveUpFlag: " + moveUpFlag);
        Debug.Log("moveDownFlag: " + moveDownFlag);
        Debug.Log("position: " + transform.position);

        if (rotateLeftFlag)
        {
            transform.RotateAround(table.transform.position, Vector3.up, rotationSpeed * Time.deltaTime * -1f);
        }

        if (rotateRightFlag)
        {
            transform.RotateAround(table.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
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
