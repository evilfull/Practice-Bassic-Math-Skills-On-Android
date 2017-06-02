﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class button_play : MonoBehaviour
{
    public Button button;
    public GameObject window;
    public GameObject clone;
    public RectTransform canvasParent;

    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        clone = Instantiate(window);
        clone.transform.SetParent(canvasParent.transform, false);
    }

}