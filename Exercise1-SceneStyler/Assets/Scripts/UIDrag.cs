using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag : MonoBehaviour, IDragHandler, IDropHandler, IEndDragHandler
{
    public AssetBundle myLoadedAssetBundle;
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag");
        this.transform.position = Input.mousePosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (CameraRayCast.Instance.hit.transform.tag == this.tag)
        {
           
            GameObject cube = Instantiate(GetAssetModel());
             
            cube.AddComponent<BoxCollider>();
            cube.transform.position = GetPosition(cube);
            cube.GetComponent<Renderer>().material.color = this.GetComponent<Image>().color;
            myLoadedAssetBundle.Unload(false);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
    }
    
    Vector3 loadPosition;
    Vector3 GetPosition(GameObject asset)
    {
       
        switch (CameraRayCast.Instance.hit.transform.tag)
        {
           case "Ceiling":
           ListOfAssets.Instance.listOfCeilingAssets.Add(asset);
           ListOfAssets.Instance.displayCeilingCount.text = ListOfAssets.Instance.listOfCeilingAssets.Count.ToString();
            loadPosition = new Vector3(CameraRayCast.Instance.hit.point.x, CameraRayCast.Instance.hit.transform.position.y - (asset.GetComponent<Collider>().bounds.size.y / 2), CameraRayCast.Instance.hit.point.z);
           break;
           case "Wall":
           ListOfAssets.Instance.listOfWallAssets.Add(asset);
           ListOfAssets.Instance.displayWallCount.text = ListOfAssets.Instance.listOfWallAssets.Count.ToString();
            // Debug.Log("wall rotation " + CameraRayCast.Instance.hit.transform.eulerAngles);
            switch (CameraRayCast.Instance.hit.transform.eulerAngles.y)
            {
                case 0:
               
                loadPosition = new Vector3(CameraRayCast.Instance.hit.transform.position.x + (asset.GetComponent<Collider>().bounds.size.x), CameraRayCast.Instance.hit.point.y, CameraRayCast.Instance.hit.point.z);
                break;
                case 90:
               
                loadPosition = new Vector3(CameraRayCast.Instance.hit.point.x, CameraRayCast.Instance.hit.point.y, CameraRayCast.Instance.hit.transform.position.z - (asset.GetComponent<Collider>().bounds.size.z / 2));
                break;
                case 180:
                
                loadPosition = new Vector3(CameraRayCast.Instance.hit.transform.position.x - (asset.GetComponent<Collider>().bounds.size.x), CameraRayCast.Instance.hit.point.y, CameraRayCast.Instance.hit.point.z);
                break;
                
            }
            
           break;
           case "Floor":
           ListOfAssets.Instance.listOfFloorAssets.Add(asset);
            ListOfAssets.Instance.displayFloorCount.text = ListOfAssets.Instance.listOfFloorAssets.Count.ToString();
            loadPosition = new Vector3(CameraRayCast.Instance.hit.point.x, CameraRayCast.Instance.hit.transform.position.y + (asset.GetComponent<Collider>().bounds.size.y / 2), CameraRayCast.Instance.hit.point.z);
           break; 
        
        }
        
        return loadPosition;
    }
    GameObject loadGameobject;
    GameObject GetAssetModel()
    {
       
        switch (CameraRayCast.Instance.hit.transform.tag)
        {
           case "Ceiling":
            loadGameobject = LoadModelFromAsset("ceilingplaceholder");
           break;
           case "Wall":
            loadGameobject = LoadModelFromAsset("wallplaceholder");
           break;
           case "Floor":
            loadGameobject = LoadModelFromAsset("floorplaceholder");
           break; 
        
        }
        
        return loadGameobject;
    }

    private GameObject LoadModelFromAsset(string type)
    {

        myLoadedAssetBundle = AssetBundle.LoadFromFile("/Users/saideekshit/Downloads/PlaceholderBundles/" + type);

        // Debug.Log(myLoadedAssetBundle == null ? "Failed to load AssetBundle" : "AssetBundle succesfully loaded");
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return null;
        }

        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>(type);

        // myLoadedAssetBundle.Unload(false);
        return prefab;
    }

}
