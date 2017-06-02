using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Config;

public class GameObject_Controller_Status : MonoBehaviour {
    public Text Name, matchCount, win, exp, level, plus, minus, multiply, divide;
    public Image profilePic;
    public string Username, Level, volume, UserID, pl, mi, mu, di, EXP;
    private string android_id;
    private string statsURL = "http://" + config.host + "/stats.php";
    private string picURL = "http://" + config.host + "/picture/";
    public string[] stats;
    public string picture;
    public GameObject obj;

    void Start () {
        obj.SetActive(true);
        StartCoroutine(CheckStats());
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    IEnumerator CheckStats()
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
        WWWForm form = new WWWForm();
        form.AddField("AID", android_id);
        WWW checkStats = new WWW(statsURL, form);
        yield return checkStats;
        stats = checkStats.text.Split(',');
        if (stats.Length > 1)
        {
            obj.SetActive(false);
            Username = stats[0];
            Name.GetComponent<Text>().text = stats[0];
            matchCount.GetComponent<Text>().text = stats[1];
            win.GetComponent<Text>().text = stats[2];
            int xp, lv;
            int.TryParse(stats[3], out xp);
            lv = xp / 100;
            xp = xp % 100;
            exp.GetComponent<Text>().text = xp + "/100";
            level.GetComponent<Text>().text = lv.ToString();
            Level = lv.ToString();
            EXP = stats[3];
            float numPlus, numMinus, numMultiply, numDivide;
            float.TryParse(stats[5], out numPlus);
            float.TryParse(stats[6], out numMinus);
            float.TryParse(stats[7], out numMultiply);
            float.TryParse(stats[8], out numDivide);
            plus.GetComponent<Text>().text = (numPlus) + "%";
            pl = stats[5];
            minus.GetComponent<Text>().text = (numMinus) + "%";
            mi = stats[6];
            multiply.GetComponent<Text>().text = (numMultiply) + "%";
            mu = stats[7];
            divide.GetComponent<Text>().text = (numDivide) + "%";
            di = stats[8];
            volume = stats[10];
            UserID = stats[11];
            if (stats[9] != "")
            {
                picture = stats[9];
                StartCoroutine(GetProfilePicture());
            }
        }
    }

    IEnumerator GetProfilePicture()
    {
        string url = picURL + picture;
        WWW image = new WWW(url);
        yield return image;
        Sprite sprite = new Sprite();
        sprite = Sprite.Create(image.texture, new Rect(0, 0, image.texture.width, image.texture.height),new Vector2(0,0),1);
        profilePic.GetComponent<Image>().sprite = sprite;
    }
}
