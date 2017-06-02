using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class panel_list_controller : MonoBehaviour {
    public Button player1, player2, plus, minus, multiply, divide, start;
    public Text player, type;
    public int[] gameMode;
    public GameObject userStats, plusDone, minusDone, multiplyDone, divideDone;
    public string sPlus, sMinus, sMultiply, sDivide;

	void Start () {
        userStats = GameObject.Find("UserID");
        sPlus = userStats.GetComponent<getID>().sPlus;
        sMinus = userStats.GetComponent<getID>().sMinus;
        sMultiply = userStats.GetComponent<getID>().sMultiply;
        sDivide = userStats.GetComponent<getID>().sDivide;
        if (sPlus == "100")
        {
            plusDone.SetActive(true);
        }
        if (sMinus == "100")
        {
            minusDone.SetActive(true);
        }
        if (sMultiply == "100")
        {
            multiplyDone.SetActive(true);
        }
        if (sDivide == "100")
        {
            divideDone.SetActive(true);
        }
        gameMode = new int[2];
        gameMode[0] = 0;
        gameMode[1] = 0;
        player.GetComponent<Text>().text = "1 ผู้เล่น";
        type.GetComponent<Text>().text = "บวก";
        player1.onClick.AddListener(player1Click);
        player2.onClick.AddListener(player2Click);
        plus.onClick.AddListener(plusClick);
        minus.onClick.AddListener(minusClick);
        multiply.onClick.AddListener(multiplyClick);
        divide.onClick.AddListener(divideClick);
        start.onClick.AddListener(startClick);
	}

    private void startClick()
    {
        if (gameMode[0] == 1)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
        
    }

    private void divideClick()
    {
        gameMode[1] = 3;
        type.GetComponent<Text>().text = "หาร";
    }

    private void multiplyClick()
    {
        gameMode[1] = 2;
        type.GetComponent<Text>().text = "คูณ";
    }

    private void minusClick()
    {
        gameMode[1] = 1;
        type.GetComponent<Text>().text = "ลบ";
    }

    private void plusClick()
    {
        gameMode[1] = 0;
        type.GetComponent<Text>().text = "บวก";
    }

    private void player2Click()
    {
        gameMode[0] = 1;
        player.GetComponent<Text>().text = "2 ผู้เล่น";
    }

    private void player1Click()
    {
        gameMode[0] = 0;
        player.GetComponent<Text>().text = "1 ผู้เล่น";
    }
}
