using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class button_exit_multiplayer : MonoBehaviour {
    public Button button;
	// Use this for initialization
	void Start () {
        button.onClick.AddListener(onClick);
	}

    private void onClick()
    {
        SceneManager.LoadScene(2);
    }
}
