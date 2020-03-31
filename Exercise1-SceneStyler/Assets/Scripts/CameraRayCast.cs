using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{

    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                // Debug.Log("hit " + hit.transform.name);
            }
        }
    }
}
