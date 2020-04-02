using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager _instance;
    public static AssetManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("AssetManager is empty");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    private RefrenceAssetDrag currentAsset;
    public RefrenceAssetDrag CurrentAsset
    {
        get
        {
            return currentAsset;
        }
        set
        {
           
            if (currentAsset != null)
            {
                // Debug.Log("currentAsset " + currentAsset);
                // currentAsset.OnDeSelection();
            }
            currentAsset = value;
            // currentCategory.OnSelection();
        }
    }
    void OnEnable()
    {
        AssetSelectionEvent.onAssetSelection += SetCurrentAsset;
    }
    void OnDisable()
    {
        AssetSelectionEvent.onAssetSelection -= SetCurrentAsset;
    }
    public GameObject canvasPrefab;
    public GameObject spherePrefab;
    //Method is used for load into delegate
    void SetCurrentAsset(RefrenceAssetDrag asset)
    {
        CurrentAsset = asset;
    }
}
