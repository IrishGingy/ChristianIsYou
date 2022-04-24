using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] GameObject emptyPrefab;

    private GameObject above;
    private GameObject below;
    private GameObject rightSide;
    private GameObject leftSide;

    private void Awake()
    {
        above = Instantiate(emptyPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        above.name = $"Above {above.transform.localPosition}";

        below = Instantiate(emptyPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        below.name = $"Below {below.transform.localPosition}";

        rightSide = Instantiate(emptyPrefab, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity, transform);
        rightSide.name = $"RightSide {rightSide.transform.localPosition}";

        leftSide = Instantiate(emptyPrefab, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity, transform);
        leftSide.name = $"LeftSide {leftSide.transform.localPosition}";

        /*Debug.Log("Above: " + above.transform.localPosition);
        Debug.Log("Below: " + below.transform.localPosition);
        Debug.Log("Left: " + leftSide.transform.localPosition);
        Debug.Log("Right: " + rightSide.transform.localPosition);*/
    }
}
