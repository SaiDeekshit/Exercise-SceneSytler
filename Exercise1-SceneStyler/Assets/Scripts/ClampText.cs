using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampText : MonoBehaviour
{
    public Text uitext;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 uiPosition = Camera.main.WorldToScreenPoint(AssetManager.Instance.spherePrefab.transform.position);
       Debug.Log(uiPosition);
       uitext.transform.position = uiPosition;
    }
}
