using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOperator : Object
{
    public GameObject left;
    public GameObject right;
    public GameObject top;
    public GameObject bottom;

    private void Start()
    {
        AddAttribute("a2");
        CheckAttribute();
    }

    public override void CheckAttribute()
    {
        foreach (Trigger t in triggers)
        {
            if (t.triggered)
            {
                switch (t.name)
                {
                    case "LeftSide":
                        left = t.triggerer;
                        break;
                    case "RightSide":
                        right = t.triggerer;
                        break;
                    case "Above":
                        top = t.triggerer;
                        break;
                    case "Below":
                        bottom = t.triggerer;
                        break;
                }
            }
            /*if (t.triggered)
            {
                Debug.Log("I am " + t.name + " and I feel personally offended by " + t.triggerer);
            }*/
        }
        moving = false;
    }
}
