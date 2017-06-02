using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Config;
using UnityEngine.SceneManagement;

public class gameplay : MonoBehaviour {
    public string UserID, level, l;
    private GameObject objUserID, cloneA, cloneB, symbol;
    public Button button, giveUp;
    public int mode;
    public Text textLevel, textQuestA, textQuestB;
    private string getQuest = "http://" + config.host + "/quest.php";
    private string levelUp = "http://" + config.host + "/levelup.php";
    public string[] q1;
    public string[][] q2;
    public GameObject itemQuestA, itemQuestA_Ten, itemQuestA_Hund, itemQuestA_Thou, itemQuestB, itemQuestB_Ten, itemQuestB_Hund, itemQuestB_Thou, 
        window, sPlus, sMinus, sMultiply, sDivide, ready, finish;
    public RectTransform parentQuestA, parentQuestB, canvasParent, symbolParent, answerParent;
    public int answer = 0, list, tenA, tenB, hundA, hundB, thouA, thouB;
    public GameObject[] obj, objAnswer;
    public AudioSource gameplaySound, bgMusic;
    public GameObject backgroundSound;

    void Start () {
        backgroundSound = GameObject.Find("backgroundSound");
        bgMusic = backgroundSound.GetComponent<AudioSource>();
        gameplaySound = GetComponent<AudioSource>();
        gameplaySound.volume = bgMusic.volume;
        ready.SetActive(true);
        StartCoroutine(genQeust());
        button.onClick.AddListener(startClick);
        giveUp.onClick.AddListener(giveUpClick);
        button = null;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void giveUpClick()
    {
        //StartCoroutine(LevelPass(l));
        DestroyObject(GameObject.Find("UserID"));
        gameplaySound.Stop();
        bgMusic.Play();
        SceneManager.LoadScene(2);
    }

    private IEnumerator LevelPass(string lvl)
    {
        int exp;
        switch (mode)
        {
            case 0:
                exp = 10;
                break;
            case 1:
                exp = 10;
                break;
            case 2:
                exp = 30;
                break;
            case 3:
                exp = 50;
                break;
            default:
                exp = 10;
                break;
        }
        WWWForm fm = new WWWForm();
        fm.AddField("ID", UserID);
        fm.AddField("level", lvl);
        fm.AddField("mode", mode);
        fm.AddField("exp", exp);
        WWW getUp = new WWW(levelUp, fm);
        yield return getUp;
    }

    private IEnumerator genQeust()
    {
        if (GameObject.Find("UserID"))
        {
            objUserID = GameObject.Find("UserID");
            UserID = objUserID.GetComponent<getID>().UserID;
            mode = objUserID.GetComponent<getID>().gameMode[1];
            switch (mode)
            {
                case 0:
                    symbol = Instantiate(sPlus);
                    symbol.transform.SetParent(symbolParent.transform, false);
                    level = objUserID.GetComponent<getID>().sPlus;
                    break;
                case 1:
                    symbol = Instantiate(sMinus);
                    symbol.transform.SetParent(symbolParent.transform, false);
                    level = objUserID.GetComponent<getID>().sMinus;
                    break;
                case 2:
                    symbol = Instantiate(sMultiply);
                    symbol.transform.SetParent(symbolParent.transform, false);
                    level = objUserID.GetComponent<getID>().sMultiply;
                    break;
                case 3:
                    symbol = Instantiate(sDivide);
                    symbol.transform.SetParent(symbolParent.transform, false);
                    level = objUserID.GetComponent<getID>().sDivide;
                    break;
                default:
                    symbol = Instantiate(sPlus);
                    symbol.transform.SetParent(symbolParent.transform, false);
                    level = objUserID.GetComponent<getID>().sPlus;
                    break;
            }
        }
        WWWForm from = new WWWForm();
        from.AddField("ID", UserID);
        from.AddField("mode", mode);
        from.AddField("level", level);
        WWW quest = new WWW(getQuest, from);
        yield return quest;
        q1 = quest.text.Split('/');
        q2 = new string[q1.Length][];
        for(int i = 0; i < (q1.Length - 1); i++) 
        {
            q2[i] = q1[i].Split(',');
        }
        list = 0;
        startQuest(list);
    }

    void startQuest(int list)
    {
        StartCoroutine(setAnswerObj());
    }

    private IEnumerator setAnswerObj()
    {
        yield return new WaitForSeconds(1f);
        objAnswer = GameObject.FindGameObjectsWithTag("Answer");
        for (int i = 0; i < objAnswer.LongLength; i++)
        {
            objAnswer[i].transform.parent = answerParent;
        }
        if (list >= (q2.Length - 1))
        {
            finish.SetActive(true);
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("quest_image").Length > 0)
            {
                ClearObject();
            }
            int setA, setB;
            int.TryParse(q2[list][1], out setA);
            int.TryParse(q2[list][2], out setB);
            int.TryParse(q2[list][3], out answer);
            textLevel.GetComponent<Text>().text = q2[list][0];
            textQuestA.GetComponent<Text>().text = setA.ToString("N0");
            textQuestB.GetComponent<Text>().text = setB.ToString("N0");
            SetQuest(setA, setB);
        }
    }

    private void SetQuest(int setA, int setB)
    {
        if (setA >= 1000)
        {
            thouA = setA / 1000;
            hundA = (setA / 100) - (thouA * 10);
            tenA = (setA / 10) - ((thouA * 100) + (hundA * 10));
            setA = setA % 10;
            GenImageA(thouA, itemQuestA_Thou);
            GenImageA(hundA, itemQuestA_Hund);
            GenImageA(tenA, itemQuestA_Ten);
            GenImageA(setA, itemQuestA);
        }
        else if(setA >= 100)
        {
            hundA = setA / 100;
            tenA = (setA / 10) - (hundA * 10);
            setA = setA % 10;
            GenImageA(hundA, itemQuestA_Hund);
            GenImageA(tenA, itemQuestA_Ten);
            GenImageA(setA, itemQuestA);
        }
        else if(setA >= 10)
        {
            tenA = setA / 10;
            setA = setA % 10;
            GenImageA(tenA, itemQuestA_Ten);
            GenImageA(setA, itemQuestA);
        }
        else
        {
            GenImageA(setA, itemQuestA);
        }

        if (setB >= 1000)
        {
            thouB = setB / 1000;
            hundB = (setB / 100) - (thouB * 10);
            tenB = (setB / 10) - ((thouB * 100) + (hundB * 10));
            setB = setB % 10;
            GenImageB(thouB, itemQuestB_Thou);
            GenImageB(hundB, itemQuestB_Hund);
            GenImageB(tenB, itemQuestB_Ten);
            GenImageB(setB, itemQuestB);
        }
        else if (setB >= 100)
        {
            hundB = setB / 100;
            tenB = (setB / 10) - (hundB * 10);
            setB = setB % 10;
            GenImageB(hundB, itemQuestB_Hund);
            GenImageB(tenB, itemQuestB_Ten);
            GenImageB(setB, itemQuestB);
        }
        else if (setB >= 10)
        {
            tenB = setB / 10;
            setB = setB % 10;
            GenImageB(tenB, itemQuestB_Ten);
            GenImageB(setB, itemQuestB);
        }
        else
        {
            GenImageB(setB, itemQuestB);
        }
    }

    private void GenImageA(int num,GameObject g)
    {
        for (int i = 0; i < num; i++)
        {
            cloneA = Instantiate(g);
            cloneA.transform.SetParent(parentQuestA.transform, false);
            cloneA.name = "q1";
        }
    }

    private void GenImageB(int num, GameObject g)
    {
        for (int i = 0; i < num; i++)
        {
            cloneB = Instantiate(g);
            cloneB.transform.SetParent(parentQuestB.transform, false);
            cloneB.name = "q2";
        }
    }

    private void startClick()
    {
        DestroyObject(GameObject.Find("Panel_Ready"));
        gameplaySound.Play();
        bgMusic.Stop();
    }

    void Update ()
    {
        AnswerCheck a = GameObject.Find("Panel_Answer_Slot").GetComponent<AnswerCheck>();
        if (a.finishQuest)
        {
            l = q2[list][0];
            StartCoroutine(LevelPass(l));
            a.finishQuest = false;
            list++;
            startQuest(list);
        }
    }

    void ClearObject()
    {
        obj = GameObject.FindGameObjectsWithTag("quest_image");
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        }
    }
}
