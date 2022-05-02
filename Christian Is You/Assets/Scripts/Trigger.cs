using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool triggered;
    public GameObject triggerer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(transform.name + " was triggered by " + collision);
        if (!collision.CompareTag("Trigger"))
        {
            triggered = true;
            triggerer = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(transform.name + " is not triggered by " + collision + " now");
        if (!collision.CompareTag("Trigger"))
        {
            triggered = false;
            triggerer = null;
        }
    }
}
