using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOperator : Operator
{
    public Object left;
    public Object right;
    public Object top;
    public Object bottom;

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
                        left.wordObject = t.triggerer;
                        break;
                    case "RightSide":
                        right.wordObject = t.triggerer;
                        break;
                    case "Above":
                        top.wordObject = t.triggerer;
                        break;
                    case "Below":
                        bottom.wordObject = t.triggerer;
                        break;
                }
            }

            /*if (t.triggered)
            {
                Debug.Log("I am " + t.name + " and I feel personally offended by " + t.triggerer);
            }*/
        }
        moving = false;

        // if either the left and right triggers are triggered, or the top and bottom triggers.
        if (left.wordObject && right.wordObject)
        {
            // left.wordObject.tag;
        }
        if (top.wordObject && bottom.wordObject)
        {

        }
    }
}
