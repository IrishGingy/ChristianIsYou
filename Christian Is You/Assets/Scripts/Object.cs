using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public List<string> attributes;
    public bool moving;

    [SerializeField] GameObject triggerPrefab;

    // trigger gameobjects.
    private GameObject above;
    private GameObject below;
    private GameObject rightSide;
    private GameObject leftSide;

    // triggers.
    protected Trigger aboveTrigger;
    protected Trigger belowTrigger;
    protected Trigger rightSideTrigger;
    protected Trigger leftSideTrigger;
    protected List<Trigger> triggers = new List<Trigger>();

    private void Awake()
    {
        AddAttribute("a1");

        // instantiate trigger game objects and change their name.
        above = Instantiate(triggerPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        above.name = "Above";

        below = Instantiate(triggerPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        below.name = "Below";

        rightSide = Instantiate(triggerPrefab, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity, transform);
        rightSide.name = "RightSide";

        leftSide = Instantiate(triggerPrefab, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity, transform);
        leftSide.name = "LeftSide";

        // get trigger components from objects and add them to the list of triggers.
        aboveTrigger = transform.GetChild(0).GetComponent<Trigger>();
        triggers.Add(aboveTrigger);

        belowTrigger = transform.GetChild(1).GetComponent<Trigger>();
        triggers.Add(belowTrigger);

        rightSideTrigger = transform.GetChild(2).GetComponent<Trigger>();
        triggers.Add(rightSideTrigger);

        leftSideTrigger = transform.GetChild(3).GetComponent<Trigger>();
        triggers.Add(leftSideTrigger);

        CheckAttribute();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            moving = true;
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            CheckAttribute();
        }
    }

    public void AddAttribute(string attribute)
    {
        attributes.Add(attribute);
    }

    public void RemoveAttribute(string attribute)
    {
        attributes.Remove(attribute);
    }

    public virtual void CheckAttribute()
    {
        foreach (Trigger t in triggers)
        {
            if (t.triggered)
            {
                Debug.Log("I am " + t.name + " and I feel personally offended by " + t.triggerer);
            }
        }
        moving = false;
    }
}
