using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class button_about_exit : MonoBehaviour {
    public Button button;
	void Start () {
        button.onClick.AddListener(OnClickButton);
	}

    private void OnClickButton()
    {
        Destroy(GameObject.Find("Panel_About(Clone)"));
    }
}
