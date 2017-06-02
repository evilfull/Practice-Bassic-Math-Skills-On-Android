using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade_Logo : MonoBehaviour {
    public Image fadeScene;
    private int count;
    private Color fadeColor = Color.black;
    private bool isFade = true;

    void Start () {
        fadeColor = fadeScene.color;
    }
	
	void Update () {
        if (isFade)
        {
            fadeColor.a = Mathf.Lerp(fadeColor.a, isFade ? 0 : 1, Time.deltaTime * 1f);
            fadeScene.color = fadeColor;
            count++;
            if (count == 200)
            {
                isFade = false;
            }
        }
        else
        {
            fadeColor.a = Mathf.Lerp(fadeColor.a, isFade ? 0 : 1, Time.deltaTime * 3f);
            fadeScene.color = fadeColor;
            count++;
            if(count == 280)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
