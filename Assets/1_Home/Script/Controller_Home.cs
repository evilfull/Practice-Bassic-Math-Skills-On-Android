using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Config;

public class Controller_Home : MonoBehaviour {
    public Image fadeImage;
    public Text text,vers;
    private float fadeSpeed = 5f;
    private float count = 50;
    private Color fadeColor = Color.black;
    private bool isFade = true, click = false;
    private string android_id;
    private string insertURL = "http://" + config.host + "/insert.php";
    private string displayURL = "http://" + config.host + "/display.php";

    void Start ()
    {
        vers.text = "\u00A9 RMUTP 2016, Version " + Application.version.ToString();
        fadeColor = fadeImage.color;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = secure.CallStatic<string>("getString", contentResolver, "android_id");
        }
        else
        {
            android_id = "abcdefghigk";
        }
        StartCoroutine(CheckRegister());
    }

    IEnumerator CheckRegister()
    {
        WWWForm form = new WWWForm();
        form.AddField("AID", android_id);
        WWW checkRegister = new WWW(displayURL, form);
        yield return checkRegister;
        if (checkRegister.text != android_id)
        {
            StartCoroutine(Register());
        } 
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("AID", android_id);
        WWW www = new WWW(insertURL, form);
        yield return www;
    }

    void Update ()
    {
        if (!isFade)
        {
            fadeColor.a = Mathf.Lerp(fadeColor.a, isFade ? 0 : 1, Time.deltaTime * fadeSpeed);
            fadeImage.color = fadeColor;
            if(count == 0)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                count--;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!click)
            {
                isFade = false;
                click = true;
            }
        }
    }
}
