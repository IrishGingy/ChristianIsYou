using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GridManager grid;

    bool right;
    bool left;
    bool up;
    bool down;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Move right
            right = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // Move left
            left = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // Move up
            up = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Move down
            down = true;
        }
    }

    private void FixedUpdate()
    {
        if (right)
        {
            Debug.Log("Moving right...");
            transform.position = new Vector3(transform.position.x + grid.cellSize, transform.position.y, transform.position.z);
            right = false;
        }
        else if (left)
        {
            Debug.Log("Moving left...");
            transform.position = new Vector3(transform.position.x - grid.cellSize, transform.position.y, transform.position.z);
            left = false;
        }
        else if (up)
        {
            Debug.Log("Moving up...");
            transform.position = new Vector3(transform.position.x, transform.position.y + grid.cellSize, transform.position.z);
            up = false;
        }
        else if (down)
        {
            Debug.Log("Moving down...");
            transform.position = new Vector3(transform.position.x, transform.position.y - grid.cellSize, transform.position.z);
            down = false;
        }
    }

    public void PositionPlayerOnStart(Vector3 position)
    {
        transform.position = position;
    }
}
