using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfAssets : MonoBehaviour
{
    private static ListOfAssets _instance;
    public static ListOfAssets Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("ListOfAsset is empty");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

   public List<GameObject> listOfCeilingAssets;
   public List<GameObject> listOfWallAssets;
   public List<GameObject> listOfFloorAssets;

   public Text displayCeilingCount;
   public Text displayWallCount;
   public Text displayFloorCount;

}
