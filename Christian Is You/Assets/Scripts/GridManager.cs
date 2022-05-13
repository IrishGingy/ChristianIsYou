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
        Stack<GameObject> objectsToMove = new Stack<GameObject>();
        Vector3 directionVector = Vector3.zero;
        bool movingObjects = false;
        Cell playerCell = grid[(int)position.x, (int)position.y];
        Cell adjacentCell = null;
        Cell nextCell = null;

        switch (direction)
        {
            case "right":
                // right
                directionVector = new Vector3(1, 0, 0);
                adjacentCell = grid[(int)position.x + 1, (int)position.y];
                nextCell = adjacentCell;

                if (nextCell.occupied)
                {
                    int i = 2;
                    // while the next cell over is occupied (this is for pushing multiple objects simultaneously).
                    do
                    {
                        objectsToMove.Push(nextCell.occupiedBy);
                        nextCell = grid[(int)position.x + i, (int)position.y];
                        i++;
                    } while (nextCell.occupied);
                    movingObjects = true;
                }

                /*if (adjacentCell.occupied)
                {
                    //Cell curCell = grid[(int)position.x, (int)position.y];
                    objectsToMove.Push(currentCell.occupiedBy);

                    int i = 2;
                    Cell nextAdjacentCell = grid[(int)position.x + i, (int)position.y];
                    // while the next cell over is occupied (this is for pushing multiple objects simultaneously).
                    while (nextAdjacentCell.occupied)
                    {
                        objectsToMove.Push(nextAdjacentCell.occupiedBy);
                        i++;
                        nextAdjacentCell = grid[(int)position.x + i, (int)position.y];
                    }
                }*/

                break;
            case "left":
                // left
                directionVector = new Vector3(-1, 0, 0);
                adjacentCell = grid[(int)position.x - 1, (int)position.y];
                nextCell = adjacentCell;

                if (nextCell.occupied)
                {
                    int i = 2;
                    // while the next cell over is occupied (this is for pushing multiple objects simultaneously).
                    do
                    {
                        objectsToMove.Push(nextCell.occupiedBy);
                        nextCell = grid[(int)position.x - i, (int)position.y];
                        i++;
                    } while (nextCell.occupied);
                    movingObjects = true;
                }

                break;
            case "up":
                // up
                directionVector = new Vector3(0, 1, 0);
                adjacentCell = grid[(int)position.x, (int)position.y + 1];
                nextCell = adjacentCell;

                if (nextCell.occupied)
                {
                    int i = 2;
                    // while the next cell over is occupied (this is for pushing multiple objects simultaneously).
                    do
                    {
                        objectsToMove.Push(nextCell.occupiedBy);
                        nextCell = grid[(int)position.x, (int)position.y + i];
                        i++;
                    } while (nextCell.occupied);
                    movingObjects = true;
                }

                break;
            case "down":
                // down
                directionVector = new Vector3(0, -1, 0);
                adjacentCell = grid[(int)position.x, (int)position.y - 1];
                nextCell = adjacentCell;

                if (nextCell.occupied)
                {
                    int i = 2;
                    // while the next cell over is occupied (this is for pushing multiple objects simultaneously).
                    do
                    {
                        objectsToMove.Push(nextCell.occupiedBy);
                        nextCell = grid[(int)position.x, (int)position.y - i];
                        i++;
                    } while (nextCell.occupied);
                    movingObjects = true;
                }

                break;
            default:
                Debug.Log("Unknown direction to update grid...");
                break;
        }

        if (movingObjects)
        {
            // moving objects backwards (from last object put on stack to player).
            while (objectsToMove.Count > 0)
            {
                GameObject currentObject = objectsToMove.Pop();
                Cell currentCell = grid[(int)currentObject.transform.position.x, (int)currentObject.transform.position.y];
                currentObject.transform.position += directionVector;
                Vector3 movedObjectPosition = currentObject.transform.position;
                Cell destinationCell = grid[(int)movedObjectPosition.x, (int)movedObjectPosition.y];
                UpdateObjectOnGrid(currentCell, destinationCell, currentObject);
            }

            player.transform.position += directionVector;

            /* // while next object to move isn't the player...
             while (objectsToMove.Count > 0)
             {
                 GameObject curObject = objectsToMove.Pop();
                 Vector3 curPos = curObject.transform.position;
                 Cell curCell = grid[(int)curPos.x, (int)curPos.y];
                 curObject.transform.position += directionVector;
                 nextCell = grid[(int)curObject.transform.position.x, (int)curObject.transform.position.y];
                 if (objectsToMove.Count != 1)
                 {
                     UpdateObjectOnGrid(nextCell, curObject);
                 }
                 else
                 {
                     UpdateObjectOnGrid(nextCell);
                 }
                 //prevCell = curCell;
             }
            */
            Debug.Log("Updating grid...");



            /*
            UpdateCell(prevCell);
            while (objectsToMove.Count > 1)
            {
                GameObject obj = objectsToMove.Pop();
                Vector3 objectPos = obj.transform.position;
                obj.transform.position += directionVector;
                Cell objectsCell = grid[(int)objectPos.x, (int)objectPos.y];
                UpdateCell(objectsCell, prevCell);
                prevCell = grid[(int)objectPos.x, (int)objectPos.y];
            }
            */
        }

        UpdatePlayerOnGrid(playerCell, adjacentCell);
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

    /*private void UpdateCell(Cell curCell, GameObject curObject = null)
    {
        if (curObject)
        {
            curCell.occupied = true;
            curCell.occupiedBy = curObject;
        }
        else
        {
            curCell.occupied = false;
            curCell.occupiedBy = curObject;
        }
    }*/

    private void UpdatePlayerOnGrid(Cell previous, Cell current)
    {
        previous.occupied = false;
        previous.occupiedBy = null;

        current.occupied = true;
        current.occupiedBy = player;
    }

    private void UpdateObjectOnGrid(Cell start, Cell destination, GameObject obj)
    {
        start.occupied = false;
        start.occupiedBy = null;

        destination.occupied = true;
        destination.occupiedBy = obj;
    }
}
