using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DragNumber : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform startParrent = null;
    public Transform placeholderParrent = null;
    GameObject placeholder = null;
    public GameObject item;

    public void OnBeginDrag(PointerEventData eventData)
    {
        item = this.gameObject;
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        le.transform.localScale = new Vector3(1, 1, 1);

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        startParrent = this.transform.parent;
        placeholderParrent = startParrent;
        this.transform.SetParent(this.transform.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentMouse = Input.mousePosition;
        currentMouse.z = 100;
        this.transform.position = Camera.main.ScreenToWorldPoint(currentMouse);

        if(placeholder.transform.parent != placeholderParrent)
        {
            placeholder.transform.SetParent(placeholderParrent);
        }
        int newSiblingIndex = placeholderParrent.childCount;
        for(int i=0; i < placeholderParrent.childCount; i++)
        {
            if(this.transform.position.x < placeholderParrent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        item = null;
        this.transform.SetParent(startParrent);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);
    }
}
