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
    [SerializeField] GameObject[] cellPrefabs;

    public Cell[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        width = 20;
        height = 10;
        SetGridDimensions(width, height);
        /*grid = new bool[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

            }
        }*/

        cellSize = cellPrefabs[0].transform.localScale.x; // cellPrefabs[0] is the base cell.
        //Debug.Log("Cell size: " + cellSize);
        PlayerMovement movement = player.GetComponent<PlayerMovement>(); 
        CreateGrid();

        // position player on start (having it in this class makes sure the grid is created before positioning player).
        Vector3 start = GetCellClosestToPosition(startingPosition.transform.position);
        movement.PositionPlayerOnStart(start);
    }

    void CreateGrid()
    {
        for (float x = 0; x < width; x += cellSize)
        {
            for (float y = 0; y < height; y += cellSize)
            {
                GameObject backgroundCell = Instantiate(cellPrefabs[0], new Vector2(x, y), Quaternion.identity, this.transform);
                int[] loc = new int[2];
                loc[0] = (int)x;
                loc[1] = (int)y;

                Cell cell = backgroundCell.GetComponent<Cell>();

                grid[(int)x, (int)y] = cell;

                /*// testing placing objects on grid by tag.
                if (loc[0] == 3 && loc[1] == 4)
                {
                    GameObject cellObj = PlaceObject(cell, "Christian", 3, 4);
                    cell.occupied = true;
                }
                else
                {
                    cell.occupied = false;
                }*/

                cell.location = loc;
                cell.name = $"Cell {x} {y}";
            }
        }

        PlaceObject(cellPrefabs[1], grid[3, 3]);
        PlaceObject(cellPrefabs[1], grid[4, 3]);

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

    public void UpdateGrid(Vector3 position, string direction)
    {
        List<GameObject> objectsToMove = new List<GameObject>();
        Vector3 directionVector = Vector3.zero;
        bool movingObjects = false;

        switch (direction)
        {
            case "right":
                // right
                directionVector = new Vector3(1, 0, 0);

                Cell adjacentCell = grid[(int)position.x + 1, (int)position.y];
                if (adjacentCell.occupied)
                {
                    objectsToMove.Add(adjacentCell.occupiedBy);
                    movingObjects = true;

                    int i = 2;
                    Cell nextAdjacentCell = grid[(int)position.x + i, (int)position.y];
                    // while the next cell over is occupied (this is for pushing multiple objects simultaneously).
                    while (nextAdjacentCell.occupied)
                    {
                        objectsToMove.Add(nextAdjacentCell.occupiedBy);
                        i++;
                        nextAdjacentCell = grid[(int)position.x + i, (int)position.y];
                    }
                }
                break;
            case "left":
                // left
                directionVector = new Vector3(-1, 0, 0);
                break;
            case "up":
                // up
                directionVector = new Vector3(0, 1, 0);
                break;
            case "down":
                // down
                directionVector = new Vector3(0, -1, 0);
                break;
            default:
                Debug.Log("Unknown direction to update grid...");
                break;
        }

        if (movingObjects)
        {
            foreach (GameObject g in objectsToMove)
            {
                g.transform.position += directionVector;
            }
            player.transform.position += directionVector;
        }

        Debug.Log("Updating grid...");
    }

    public void PlaceObject(GameObject prefab, Cell cell)
    {
        int x = cell.location[0];
        int y = cell.location[1];

        //Instantiate(christianPrefab);
        GameObject cellObj = Instantiate(prefab, new Vector2(x, y), Quaternion.identity, this.transform);
        grid[x, y].occupied = true;
        grid[x, y].occupiedBy = cellObj;
    }

    private void SetGridDimensions(int w, int h)
    {
        grid = new Cell[w, h];
    }
}
