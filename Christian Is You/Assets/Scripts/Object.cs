using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // game object that represents a series of game objects.
    public GameObject wordObject;

    // game object that attributes are added and removed from.
    public GameObject[] linkedObjects;

    bool noun;
    bool adjective;

    private void Start()
    {
        if (wordObject.CompareTag("Noun"))
        {
            noun = true;
        }
        else if (wordObject.CompareTag("Adjective"))
        {
            adjective = true;
        }
    }

    public void AddAttribute()
    {
        // if noun, we are adding the attribute (could be either noun or adjective) to this object.

        // if adjective, we check whether it is on the output side.
        // if it is on the output side, we add it's associated attribute to the noun.
        // if it is not on the output side, we do nothing.
    }

    public void RemoveAttribute()
    {

    }
}
