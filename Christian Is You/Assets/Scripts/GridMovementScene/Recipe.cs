using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attribute", menuName = "Attribute")]
public class Recipe : ScriptableObject
{
    public GameObject input1;
    public GameObject input2;

    public Attribute output;
}
