using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool occupied;
    public GameObject occupiedBy;
    public int[] location = new int[2];
    public ScriptableObject attribute;
}
