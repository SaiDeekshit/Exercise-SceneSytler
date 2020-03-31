using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler, IDropHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag");
        this.transform.position = Input.mousePosition;
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
    }

}
