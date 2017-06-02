using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            return;
        }
        DragNumber d = eventData.pointerDrag.GetComponent<DragNumber>();
        if(d != null)
        {
            d.placeholderParrent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        DragNumber d = eventData.pointerDrag.GetComponent<DragNumber>();
        if (d != null && d.placeholderParrent == this.transform);
        {
            d.placeholderParrent = d.startParrent;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragNumber d = eventData.pointerDrag.GetComponent<DragNumber>();
        if(d != null)
        {
            d.startParrent = this.transform;
        }
    }
}
