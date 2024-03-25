using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        Vector3[] displayedObjectCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(displayedObjectCorners);
        for(int i = 0; i < displayedObjectCorners.Length; i++)
            Debug.Log(string.Format("canvas : ({0}, {1}, {2})", displayedObjectCorners[i].x, displayedObjectCorners[i].y, displayedObjectCorners[i].z));
    }
}
