using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            currentAsset = value;
            isSpin.interactable = true;
            isSpin.isOn = currentAsset.isSpin;
            deleteButton.interactable = true;
            duplicateButton.interactable = true;

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
    public Toggle isSpin;
    public float mouseSensitivity;
    public GameObject temporaryParent;
    public Button deleteButton;
    public Button duplicateButton;
    void Start()
    {
        temporaryParent = new GameObject("TemporaryParent");
    }
    //Method is used for load into delegate
    void SetCurrentAsset(RefrenceAssetDrag asset)
    {
        CurrentAsset = asset;
    }
    public void UpdateIsSpin()
    {
        if (CurrentAsset != null)
        {
            CurrentAsset.isSpin = isSpin.isOn;
        }
    }
    public void DeleteAsset()
    { 
        DeleteAssetFromAsset(CurrentAsset.gameObject);
        Destroy(CurrentAsset.gameObject);
        isSpin.interactable = false;
        deleteButton.interactable = false;
        duplicateButton.interactable = false;
    }
    public void DuplicateAsset()
    {
        GameObject duplicateAsset = GameObject.Instantiate(CurrentAsset.gameObject);
        duplicateAsset.GetComponent<RefrenceAssetDrag>().plane2D = CurrentAsset.gameObject.GetComponent<RefrenceAssetDrag>().plane2D;
        AddToListOfAsset(duplicateAsset);
    }
    void AddToListOfAsset(GameObject duplicatedAsset)
    {
        switch (duplicatedAsset.tag)
        {
            case "CeilingAsset":
                ListOfAssets.Instance.listOfCeilingAssets.Add(duplicatedAsset);
                ListOfAssets.Instance.displayCeilingCount.text = ListOfAssets.Instance.listOfCeilingAssets.Count.ToString();

                break;
            case "WallAsset":
                ListOfAssets.Instance.listOfWallAssets.Add(duplicatedAsset);
                ListOfAssets.Instance.displayWallCount.text = ListOfAssets.Instance.listOfWallAssets.Count.ToString();
                
                break;
            case "FloorAsset":
                ListOfAssets.Instance.listOfFloorAssets.Add(duplicatedAsset);
                ListOfAssets.Instance.displayFloorCount.text = ListOfAssets.Instance.listOfFloorAssets.Count.ToString();
                break;

        }
    }
    void DeleteAssetFromAsset(GameObject asset)
    {
        switch (asset.tag)
        {
            case "CeilingAsset":
                ListOfAssets.Instance.listOfCeilingAssets.Remove(asset);
                ListOfAssets.Instance.displayCeilingCount.text = ListOfAssets.Instance.listOfCeilingAssets.Count.ToString();

                break;
            case "WallAsset":
                ListOfAssets.Instance.listOfWallAssets.Remove(asset);
                ListOfAssets.Instance.displayWallCount.text = ListOfAssets.Instance.listOfWallAssets.Count.ToString();
                
                break;
            case "FloorAsset":
                ListOfAssets.Instance.listOfFloorAssets.Remove(asset);
                ListOfAssets.Instance.displayFloorCount.text = ListOfAssets.Instance.listOfFloorAssets.Count.ToString();
                break;

        }
    }
    
}
