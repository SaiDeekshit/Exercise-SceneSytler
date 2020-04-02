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
       
        this.transform.position = Input.mousePosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(CameraRayCast.Instance.hit.transform != null){
        if (CameraRayCast.Instance.hit.transform.tag == this.tag)
        {

            GameObject asset = Instantiate(GetAssetModel());
            asset.AddComponent<RefrenceAssetDrag>();
            asset.AddComponent<BoxCollider>();
            asset.transform.position = GetPosition(asset);
            asset.GetComponent<Renderer>().material.color = this.GetComponent<Image>().color;

            myLoadedAssetBundle.Unload(false);
        }
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
                Vector3 eulerAngles = CameraRayCast.Instance.hit.transform.eulerAngles;
                asset.transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z);
                switch (CameraRayCast.Instance.hit.transform.eulerAngles.y)
                {

                    case 0:
                        loadPosition = new Vector3(CameraRayCast.Instance.hit.transform.position.x + (asset.GetComponent<Collider>().bounds.size.x), CameraRayCast.Instance.hit.point.y, CameraRayCast.Instance.hit.point.z);

                        break;
                    case 90:
                        loadPosition = new Vector3(CameraRayCast.Instance.hit.point.x, CameraRayCast.Instance.hit.point.y, CameraRayCast.Instance.hit.transform.position.z - (asset.GetComponent<Collider>().bounds.size.z));
                        
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
        asset.tag = CameraRayCast.Instance.hit.transform.tag + "Asset";
       asset.GetComponent<RefrenceAssetDrag>().plane2D = GetPlaneMomentValues(CameraRayCast.Instance.hit.transform.gameObject,asset);
        
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
    Plane GetPlaneMomentValues(GameObject plane,GameObject asset)
    {
        Vector3 values = plane.transform.localScale;
        Plane newPlane = new Plane();
        if (values.x > values.y && values.z > values.y)
        {
            // Debug.Log("y is smallest");
            newPlane.minimumX = - plane.transform.localScale.x / 2 + (asset.GetComponent<Collider>().bounds.size.x/2);
            newPlane.maximumX =  plane.transform.localScale.x / 2 - (asset.GetComponent<Collider>().bounds.size.x/2);
            newPlane.minimumZ = - plane.transform.localScale.z / 2 + (asset.GetComponent<Collider>().bounds.size.z/2);
            newPlane.maximumZ =  plane.transform.localScale.z / 2 - (asset.GetComponent<Collider>().bounds.size.z/2);
            
        }
        if (values.x > values.z && values.y > values.z)
        {
            // Debug.Log("z is smallest");
            newPlane.minimumX = - plane.transform.localScale.x / 2 + (asset.GetComponent<Collider>().bounds.size.x/2);
            newPlane.maximumX =  plane.transform.localScale.x / 2 - (asset.GetComponent<Collider>().bounds.size.x/2);
            newPlane.minimumY = - plane.transform.localScale.y / 2 + (asset.GetComponent<Collider>().bounds.size.y/2);
            newPlane.maximumY =  plane.transform.localScale.y / 2 - (asset.GetComponent<Collider>().bounds.size.y/2);
        }
        if (values.y > values.x && values.z > values.x)
        {
            // Debug.Log("x is smallest " + plane.transform.localScale.z);
            newPlane.minimumY = - plane.transform.localScale.y / 2 + (asset.GetComponent<Collider>().bounds.size.y/2);
            newPlane.maximumY =  plane.transform.localScale.y / 2 - (asset.GetComponent<Collider>().bounds.size.y/2);
            newPlane.minimumZ = - plane.transform.localScale.z / 2 + (asset.GetComponent<Collider>().bounds.size.z);
            newPlane.maximumZ =  plane.transform.localScale.z / 2;
        }
        return newPlane;
    }


}
