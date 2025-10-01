using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeGiReactions : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        Vector2 start, end;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            start = Input.GetTouch(0).position;
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            end = Input.GetTouch(0).position;
            Vector2 dir = end - start;
            if (dir.magnitude > 50f) Debug.Log("Swipe en dirección " + dir.normalized);
        }

    }
}
