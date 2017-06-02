using UnityEngine;
using System.Collections;

public class getID : MonoBehaviour {
    public string UserID, Level, UserName, EXP;
    public int[] gameMode = new int[2];
    public string sPlus, sMinus, sMultiply, sDivide, img;
    
    void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (GameObject.Find("GameObject_Controller"))
        {
            GameObject controller = GameObject.Find("GameObject_Controller");
            GameObject_Controller_Status get = controller.GetComponent<GameObject_Controller_Status>();
            UserID = get.UserID;
            UserName = get.Username;
            Level = get.Level;
            EXP = get.EXP;
            sPlus = get.pl;
            sMinus = get.mi;
            sMultiply = get.mu;
            sDivide = get.di;
            img = get.picture;
        }
        if (GameObject.Find("Panel_Play(Clone)"))
        {
            gameMode = GameObject.Find("Panel_List_Controller").GetComponent<panel_list_controller>().gameMode;
        }
        
    }
}
