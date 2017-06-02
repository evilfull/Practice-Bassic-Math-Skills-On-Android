using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Button_Play_Exit : MonoBehaviour {
    public Button button;
    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        Destroy(GameObject.Find("Panel_Play(Clone)"));
    }
}
