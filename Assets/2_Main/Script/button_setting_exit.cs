using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Config;

public class button_setting_exit : MonoBehaviour {
    public Button button;
    public Slider slider;
    public Text text;
    private string android_id;
    private string updateURL = "http://" + config.host + "/update.php";

    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
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
        StartCoroutine(DataSave());
        Destroy(GameObject.Find("Panel_Setting(Clone)"));
    }

    IEnumerator DataSave()
    {
        WWWForm form = new WWWForm();
        form.AddField("AID", android_id);
        float vol = slider.value * 100;
        int vol2 = (int)vol;
        form.AddField("volume", vol2.ToString());
        WWW dataUpdate = new WWW(updateURL, form);
        yield return dataUpdate;
    }

    void Update()
    {
        if (GameObject.Find("backgroundSound"))
        {
            GameObject game = GameObject.Find("backgroundSound");
            game.GetComponent<AudioSource>().volume = slider.value;
        }
    }
}
