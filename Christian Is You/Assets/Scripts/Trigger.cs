using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool triggered;
    public string triggerer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(transform.name + " was triggered by " + collision);
        triggered = true;
        triggerer = collision.tag;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(transform.name + " is not triggered by " + collision + " now");
        triggered = false;
        triggerer = null;
    }
}
