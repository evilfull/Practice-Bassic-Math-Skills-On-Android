using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class button_logout : MonoBehaviour {
    public Button button;
	void Start () {
        button.onClick.AddListener(OnClickButton);
	}

    private void OnClickButton()
    {
        Destroy(GameObject.Find("backgroundSound"));
        DestroyObject(GameObject.Find("UserID"));
        SceneManager.LoadScene(1);
    }
}
