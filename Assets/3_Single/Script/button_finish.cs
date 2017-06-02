using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class button_finish : MonoBehaviour {
    public Button btn;

	void Start () {
        btn.onClick.AddListener(OnClickFinish);
	}

    private void OnClickFinish()
    {
        SceneManager.LoadScene(2);
    }
}
