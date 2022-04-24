using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public List<string> attributes;

    [SerializeField] GameObject triggerPrefab;

    // trigger gameobjects.
    private GameObject above;
    private GameObject below;
    private GameObject rightSide;
    private GameObject leftSide;

    // triggers.
    private Trigger aboveTrigger;
    private Trigger belowTrigger;
    private Trigger rightSideTrigger;
    private Trigger leftSideTrigger;
    List<Trigger> triggers = new List<Trigger>();

    private void Awake()
    {
        AddAttribute("a1");

        // instantiate trigger game objects and change their name.
        above = Instantiate(triggerPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        above.name = $"Above {above.transform.localPosition}";

        below = Instantiate(triggerPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        below.name = $"Below {below.transform.localPosition}";

        rightSide = Instantiate(triggerPrefab, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity, transform);
        rightSide.name = $"RightSide {rightSide.transform.localPosition}";

        leftSide = Instantiate(triggerPrefab, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity, transform);
        leftSide.name = $"LeftSide {leftSide.transform.localPosition}";

        // get trigger components from objects and add them to the list of triggers.
        aboveTrigger = transform.GetChild(0).GetComponent<Trigger>();
        triggers.Add(aboveTrigger);

        belowTrigger = transform.GetChild(1).GetComponent<Trigger>();
        triggers.Add(belowTrigger);

        rightSideTrigger = transform.GetChild(2).GetComponent<Trigger>();
        triggers.Add(rightSideTrigger);

        leftSideTrigger = transform.GetChild(3).GetComponent<Trigger>();
        triggers.Add(leftSideTrigger);
    }

    public void AddAttribute(string attribute)
    {
        attributes.Add(attribute);
    }

    public void RemoveAttribute(string attribute)
    {
        attributes.Remove(attribute);
    }

    public void CheckAttribute()
    {
        foreach (Trigger t in triggers)
        {
            if (t.triggered)
            {
                Debug.Log("I am " + t.name + " and I feel personally offended.");
            }
        }
    }
}
