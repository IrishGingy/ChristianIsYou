using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Object
{
    [SerializeField] GridManager grid;

    bool right; // 1
    bool left; // -1
    bool up; // 2
    bool down; // -2

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Move right
            right = true;
            grid.UpdateGrid(transform.position, "right");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // Move left
            left = true;
            grid.UpdateGrid(transform.position, "left");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // Move up
            up = true;
            grid.UpdateGrid(transform.position, "up");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Move down
            down = true;
            grid.UpdateGrid(transform.position, "down");
        }
    }

    private void FixedUpdate()
    {
        if (right)
        {
            right = false;
            //Debug.Log("Moving right...");
            if (!rightSideTrigger.triggered)
            {
                transform.position = new Vector3(transform.position.x + grid.cellSize, transform.position.y, transform.position.z);
            }
            else
            {
                Push(rightSideTrigger);
            }
        }
        else if (left)
        {
            left = false;
            //Debug.Log("Moving left...");
            if (!leftSideTrigger.triggered)
            {
                transform.position = new Vector3(transform.position.x - grid.cellSize, transform.position.y, transform.position.z);
            }
            else
            {
                Push(leftSideTrigger);
                // check it doesn't have stop attribute before moving.
                /*Vector3 pos = leftSideTrigger.triggerer.transform.position;
                transform.position = new Vector3(transform.position.x - grid.cellSize, transform.position.y, transform.position.z);
                leftSideTrigger.triggerer.transform.position = new Vector3(pos.x - grid.cellSize, pos.y, pos.z);*/
            }
        }
        else if (up)
        {
            up = false;
            //Debug.Log("Moving up...");
            if (!aboveTrigger.triggered)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + grid.cellSize, transform.position.z);
            }
            else
            {
                Push(aboveTrigger);
                // check it doesn't have stop attribute before moving.
                /*Vector3 pos = aboveTrigger.triggerer.transform.position;
                transform.position = new Vector3(transform.position.x, transform.position.y + grid.cellSize, transform.position.z);
                aboveTrigger.triggerer.transform.position = new Vector3(pos.x, pos.y + grid.cellSize, pos.z);*/
            }
        }
        else if (down)
        {
            down = false;
            //Debug.Log("Moving down...");
            if (!belowTrigger.triggered)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - grid.cellSize, transform.position.z);
            }
            else
            {
                Push(belowTrigger);
                // check it doesn't have stop attribute before moving.
                /*Vector3 pos = belowTrigger.triggerer.transform.position;
                transform.position = new Vector3(transform.position.x, transform.position.y - grid.cellSize, transform.position.z);
                belowTrigger.triggerer.transform.position = new Vector3(pos.x, pos.y - grid.cellSize, pos.z);*/
            }
        }
    }

    public void PositionPlayerOnStart(Vector3 position)
    {
        transform.position = position;
    }

    public void Moving()
    {
        CheckAttribute();
    }

    /// <summary>
    /// Pushes adjacent blockes.
    /// </summary>
    /// <param name="horizontal">Negative 1 if moving left, positive 1 if moving right, 0 if not moving</param>
    /// <param name="vertical">Negative 1 if moving down, positive 1 if moving up, 0 if not moving</param>
    public void Push(Trigger t)
    {
        /*GameObject objectToPush = t.triggerer;
        List<GameObject> list = new List<GameObject>();

        list.Add(objectToPush.transform.Find(t.name).GetComponent<Trigger>().triggerer);

        while (objectToPush.transform.Find(t.name).GetComponent<Trigger>().triggerer != null)
        {
            //list.Add('t');
        }

        for (int i = 0; i < list.Count - 1; i++)
        {

        }

        Vector3 playerEndPosition = Vector3.zero;
        Vector3 triggererEndPosition = Vector3.zero;
        do
        {
            // check it doesn't have stop attribute before moving.
            Vector3 pos = objectToPush.transform.position;
            switch (t.name)
            {
                case "RightSide":
                    playerEndPosition = new Vector3(transform.position.x + grid.cellSize, transform.position.y, transform.position.z);
                    triggererEndPosition = new Vector3(pos.x + grid.cellSize, pos.y, pos.z);
                    break;
                case "LeftSide":
                    transform.position = new Vector3(transform.position.x - grid.cellSize, transform.position.y, transform.position.z);
                    objectToPush.transform.position = new Vector3(pos.x - grid.cellSize, pos.y, pos.z);
                    break;
                case "Above":
                    transform.position = new Vector3(transform.position.x, transform.position.y + grid.cellSize, transform.position.z);
                    objectToPush.transform.position = new Vector3(pos.x, pos.y + grid.cellSize, pos.z);
                    break;
                case "Below":
                    transform.position = new Vector3(transform.position.x, transform.position.y - grid.cellSize, transform.position.z);
                    objectToPush.transform.position = new Vector3(pos.x, pos.y - grid.cellSize, pos.z);
                    break;
                default:
                    Debug.Log("No trigger found in switch statement!");
                    break;
            }
            objectToPush = objectToPush.transform.Find(t.name).GetComponent<Trigger>().triggerer;

            transform.position = playerEndPosition;
        } while (objectToPush != null);*/


        //Debug.Log("hello");
    }
}
