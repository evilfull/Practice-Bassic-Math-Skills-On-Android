using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Config;

public class button_Profile : MonoBehaviour {
    public Button button;
    public GameObject window;
    public GameObject clone;
    public RectTransform canvasParent;
    public string android_id;

    void Start()
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
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        Application.OpenURL("http://" + config.host + "/edit.php?aid=" + android_id);
        //clone = Instantiate(window);
        //clone.transform.SetParent(canvasParent.transform, false);
    }

}
