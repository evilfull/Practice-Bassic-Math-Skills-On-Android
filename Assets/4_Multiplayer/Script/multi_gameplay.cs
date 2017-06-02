using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Config;

public class multi_gameplay : MonoBehaviour {

    public SocketIOComponent socketIO;
    public GameObject roomList, panelFishin;
    private GameObject objUID;
    public Image imgA,imgB;
    public Text uNameA, uNameB, lvlA, lvlB, scoreA, scoreB, room1, room2,
        time, number1, number2, answer1, answer2,
        fName, fScore, fExp, fLevel, onlineUser;
    public Button Room1, Room2, giveUp, bAnswerA, bAnswerB, bFinish;
    public int modeCheck, NumberA, NumberB, AnswerA, AnswerB;
    public float CountDown;
    public bool startGame;
    private string webUpdate = "http://" + config.host + "/updateLv.php";
    private string picURL = "http://" + config.host + "/picture/";

    void Start() {
        roomList.SetActive(true);
        startGame = false;
        CountDown = 60;
        Room1.onClick.AddListener(OnClickRoom1);
        giveUp.onClick.AddListener(OnClickGiveUp);
        bAnswerA.onClick.AddListener(OnClickAnswerA);
        bAnswerB.onClick.AddListener(OnClickAnswerB);
        bFinish.onClick.AddListener(OnClickGiveUp);
        socketIO.On("User_Connected", OnUserConnected);
        socketIO.On("PLAY", OnUserPlay);
        socketIO.On("Check_User", OnCheckRoom);
        socketIO.On("User_Disconnected", OnUserDisconnected);
        socketIO.On("START", OnGameStart);
        socketIO.On("UPDATE", OnScoreUpdate);
        socketIO.On("FINISH", OnFinishGame);
        socketIO.On("FULL", OnRoomFull);
        StartCoroutine(CallToServer());
        if (GameObject.Find("UserID"))
        {
            objUID = GameObject.Find("UserID");
            modeCheck = objUID.GetComponent<getID>().gameMode[1];
        }
        else
        {
            Debug.Log("error : No UserID found");
        }
    }

    private void OnClickAnswerA()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = objUID.GetComponent<getID>().UserID;
        data["name"] = objUID.GetComponent<getID>().UserName;
        data["answer"] = AnswerA.ToString();
        socketIO.Emit("ANSWER", new JSONObject(data));
    }

    private void OnClickAnswerB()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = objUID.GetComponent<getID>().UserID;
        data["name"] = objUID.GetComponent<getID>().UserName;
        data["answer"] = AnswerB.ToString();
        socketIO.Emit("ANSWER", new JSONObject(data));
    }

    private void OnClickRoom1()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = objUID.GetComponent<getID>().UserID;
        data["name"] = objUID.GetComponent<getID>().UserName;
        data["level"] = objUID.GetComponent<getID>().Level;
        data["mode"] = objUID.GetComponent<getID>().gameMode[1].ToString();
        data["exp"] = objUID.GetComponent<getID>().EXP;
        data["room"] = "room1";
        data["img"] = objUID.GetComponent<getID>().img;
        socketIO.Emit("JOIN", new JSONObject(data));
    }

    private void OnClickGiveUp()
    {
        socketIO.Emit("disconnect");
        Destroy(objUID);
        SceneManager.LoadScene(2);
    }

    private void OnUserConnected(SocketIOEvent obj)
    {
        string position = obj.data.GetField("slot").ToString();
        string pic = JsonToString(obj.data.GetField("img").ToString(), "\"");
        if (position == "1")
        {
            StartCoroutine(GetProfilePictureA(pic));
            uNameA.text = JsonToString(obj.data.GetField("name").ToString(), "\"");
            lvlA.text = "lv. " + JsonToString(obj.data.GetField("level").ToString(), "\"");
            int score;
            int.TryParse(JsonToString(obj.data.GetField("score").ToString(), "\""), out score);
            scoreA.text = score.ToString("D5");
        }
        else
        {
            StartCoroutine(GetProfilePictureB(pic));
            uNameB.text = JsonToString(obj.data.GetField("name").ToString(), "\"");
            lvlB.text = "lv. " + JsonToString(obj.data.GetField("level").ToString(), "\"");
            int score;
            int.TryParse(JsonToString(obj.data.GetField("score").ToString(), "\""), out score);
            scoreB.text = score.ToString("D5");
        }
    }

    IEnumerator GetProfilePictureA(string s)
    {
        string url = picURL + s;
        WWW image = new WWW(url);
        yield return image;
        Sprite sprite = new Sprite();
        sprite = Sprite.Create(image.texture, new Rect(0, 0, image.texture.width, image.texture.height), new Vector2(0, 0), 1);
        imgA.GetComponent<Image>().sprite = sprite;
    }

    IEnumerator GetProfilePictureB(string s)
    {
        string url = picURL + s;
        WWW image = new WWW(url);
        yield return image;
        Sprite sprite = new Sprite();
        sprite = Sprite.Create(image.texture, new Rect(0, 0, image.texture.width, image.texture.height), new Vector2(0, 0), 1);
        imgB.GetComponent<Image>().sprite = sprite;
    }

    private void OnUserPlay(SocketIOEvent obj)
    {
        roomList.SetActive(false);
        socketIO.Emit("User_Join");
        socketIO.Emit("READY");
    }

    public void OnCheckRoom(SocketIOEvent obj)
    {
        onlineUser.text = "ผู้เล่นที่ออนไลน์ขณะนี้ : " + obj.data.GetField("userLobby").ToString();
        room1.text = obj.data.GetField("roomCount1").ToString() + "/2 ";
        //room2.text = JsonToString(obj.data.GetField("room2").ToString(), "\"") + "/2 ";
    }

    private void OnUserDisconnected(SocketIOEvent obj)
    {
        string position = obj.data.GetField("slot").ToString();
        if (position == "1")
        {
            uNameA.text = "User1";
            lvlA.text = "lv. 0";
            scoreA.text = 0.ToString("D4");
            imgA.GetComponent<Image>().sprite = null;
        }
        else
        {
            uNameB.text = "User2";
            lvlB.text = "lv. 0";
            scoreB.text = 0.ToString("D4");
            imgA.GetComponent<Image>().sprite = null;
        }
    }

    private void OnGameStart(SocketIOEvent obj)
    {
        time.text = CountDown.ToString("N0");
        number1.text = obj.data.GetField("num1").ToString();
        number2.text = obj.data.GetField("num2").ToString();
        answer1.text = obj.data.GetField("ans1").ToString();
        answer2.text = obj.data.GetField("ans2").ToString();
        int.TryParse(obj.data.GetField("num1").ToString(), out NumberA);
        int.TryParse(obj.data.GetField("num2").ToString(), out NumberB);
        int.TryParse(obj.data.GetField("ans1").ToString(), out AnswerA);
        int.TryParse(obj.data.GetField("ans2").ToString(), out AnswerB);
        startGame = true;
    }

    private void OnScoreUpdate(SocketIOEvent obj)
    {
        string position = obj.data.GetField("slot").ToString();
        int score;
        int.TryParse(JsonToString(obj.data.GetField("score").ToString(), "\""), out score);
        string status = JsonToString(obj.data.GetField("status").ToString(), "\"");
        if(status == "true")
        {
            number1.color = Color.green;
            number2.color = Color.green;
        }
        else
        {
            number1.color = Color.red;
            number2.color = Color.red;
        }
        StartCoroutine(DelayTime(position, score, status));
    }

    private void OnFinishGame(SocketIOEvent obj)
    {
        panelFishin.SetActive(true);
        int s, lv, exp;
        int.TryParse(JsonToString(obj.data.GetField("score").ToString(), "\""), out s);
        int.TryParse(JsonToString(obj.data.GetField("exp").ToString(), "\""), out exp);
        s = s / 10;
        lv = s / 100;
        exp = exp + s;
        fName.text = JsonToString(obj.data.GetField("name").ToString(), "\"");
        //fName.text = JsonToString(obj.data.GetField("win").ToString(), "\"");//test win/lose
        fScore.text = "คะแนน : " + JsonToString(obj.data.GetField("score").ToString(), "\"");
        fExp.text = "ค่าประสบการณ์ : " + JsonToString(obj.data.GetField("exp").ToString(), "\"") + " +" + s.ToString();
        fLevel.text = "ระดับ : " + JsonToString(obj.data.GetField("level").ToString(), "\"") + " +" + lv.ToString();
        string s1 = JsonToString(obj.data.GetField("win").ToString(), "\"");
        StartCoroutine(updateLevelWeb(s1,exp.ToString()));
    }

    IEnumerator updateLevelWeb(string s1,string s2)
    {
        WWWForm from = new WWWForm();
        from.AddField("id", objUID.GetComponent<getID>().UserID);
        from.AddField("wl", s1);
        from.AddField("exp", s2);
        WWW updateLv = new WWW(webUpdate, from);
        yield return updateLv;
    }

    private void OnRoomFull(SocketIOEvent obj)
    {
        Destroy(objUID);
        SceneManager.LoadScene(2);
    }

    IEnumerator DelayTime(string position, int score, string status)
    {
        yield return new WaitForSeconds(1f);
        number1.color = Color.black;
        number2.color = Color.black;
        if (position == "1")
        {
            scoreA.text = score.ToString("D4");
        }
        else
        {
            scoreB.text = score.ToString("D4");
        }
        if (status == "true")
        {
            socketIO.Emit("READY");
        }
    }

    IEnumerator CallToServer()
    {
        yield return new WaitForSeconds(0.3f);
        socketIO.Emit("User_Connect");  
    }

    string JsonToString(string target, string s)
    {
        string[] newString = Regex.Split(target, s);
        return newString[1];
    }

    void Update()
    {
        if (startGame)
        {
            if (CountDown > 0)
            {
                CountDown -= Time.deltaTime;
                time.text = CountDown.ToString("N0");
            }
            else
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data["id"] = objUID.GetComponent<getID>().UserID;
                data["name"] = objUID.GetComponent<getID>().UserName;
                socketIO.Emit("END", new JSONObject(data));
                time.text = "0";
                startGame = false;
            }
        }
    }
}
