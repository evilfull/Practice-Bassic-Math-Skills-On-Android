using UnityEngine;
using System.Collections;
using Config;

public class background_Music : MonoBehaviour {
    public AudioSource sound;
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
        sound.volume = 1;
        StartCoroutine(LoadSetting());
	}

    IEnumerator LoadSetting()
    {
        WWWForm form = new WWWForm();
        form.AddField("AID", android_id);
        WWW ls = new WWW(loadURL, form);
        yield return ls;
        if(ls.text != "")
        {
            string vol = ls.text;
            float vol2;
            float.TryParse(vol, out vol2);
            vol2 = vol2 / 100;
            sound.volume = vol2;
        }
        DontDestroyOnLoad(this.gameObject);

    }
}
