using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool occupied;
    public GameObject occupiedBy;
    public int[] location = new int[2];
    public ScriptableObject attribute;

    /*public void GetDestinationCell(Vector3 direction)
    {
        location += direction;
    }*/

    /*public int[] MoveUp(int[] l)
    {
        l[1] += 1;
        return l;
    }

    public int[] MoveDown(int[] l)
    {
        l[1] -= 1;
        return l;
    }

    public void MoveRight(int[] l)
    {
        l[0] += 1;
    }

    public void MoveLeft(int[] l)
    {
        l[0] -= 1;
    }*/
}
