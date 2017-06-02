using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Config;

public class get_value_setting : MonoBehaviour {
    public Slider slider;
    public Text text;
    private string android_id;
    private string loadURL = "http://" + config.host + "/load.php";
    void Start () {
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
        StartCoroutine(LoadSetting());
	}

    IEnumerator LoadSetting()
    {
        WWWForm form = new WWWForm();
        form.AddField("AID", android_id);
        WWW ls = new WWW(loadURL, form);
        yield return ls;
        float floatVol;
        float.TryParse(ls.text, out floatVol);
        floatVol = floatVol / 100;
        slider.value = floatVol;
        text.GetComponent<Text>().text = ls.text + "%";
    }

    void Update()
    {
    }
}
