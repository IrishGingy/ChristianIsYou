using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize { get; private set; }

    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] GameObject startingPosition;

    [SerializeField] Transform cam;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cellPrefab;

    public bool[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        /*grid = new bool[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

            }
        }*/

        cellSize = cellPrefab.transform.localScale.x;
        //Debug.Log("Cell size: " + cellSize);
        PlayerMovement movement = player.GetComponent<PlayerMovement>(); 
        CreateGrid();

        // position player on start (having it in this class makes sure the grid is created before positioning player).
        Vector3 start = GetCellClosestToPosition(startingPosition.transform.position);
        movement.PositionPlayerOnStart(start);
    }

    void CreateGrid()
    {
        int xCount = 0;
        int yCount = 0;

        for (float x = 0; x < width; x += cellSize)
        {
            for (float y = 0; y < height; y += cellSize)
            {
                GameObject cellObj = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity, this.transform);
                int[] loc = new int[2];
                loc[0] = xCount;
                loc[1] = yCount;

                Cell cell = cellObj.GetComponent<Cell>();

                // testing placing objects on grid by tag.
                if (loc[0] == 2 && loc[2] == 2)
                {
                    PlaceObject(cell, "Christian");
                    cell.occupied = true;
                }
                else
                {
                    cell.occupied = false;
                }

                cell.location = loc;
                cell.name = $"Cell {x} {y}";
                yCount++;
            }

            xCount++;
        }

        // Center camera.
        cam.transform.position = new Vector3((float)width/2 - 0.5f, (float)height/2 - 0.5f, -10);
        //Debug.Log("New camera position: " + cam.transform.position);
    }

    Vector3 GetCellClosestToPosition(Vector3 position)
    {
        float closestX = 0;
        float closestY = 0;

        //Debug.Log("Starting position before: " + position);
        if (!(position.x % cellSize == 0) || !(position.y % cellSize == 0))
        {
            float offset;
            if (!(position.x % cellSize == 0))
            {
                offset = position.x % cellSize;
                closestX = position.x - offset;
            }

            if (!(position.y % cellSize == 0))
            {
                offset = position.y % cellSize;
                closestY = position.y - offset;
            }

            position = new Vector3(closestX, closestY, position.z);
        }

        //Debug.Log("Starting position after: " + position);
        return position;
    }

    public void UpdateGrid()
    {
        Debug.Log("Updating grid...");
    }

    public void PlaceObject(Cell cell, string tag)
    {
        //Instantiate(christianPrefab);
        cell.tag = tag;
    }
}
