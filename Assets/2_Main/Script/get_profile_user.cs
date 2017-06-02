using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class get_profile_user : MonoBehaviour {
    public Text text;
	void Start () {
        GameObject controller = GameObject.Find("GameObject_Controller");
        GameObject_Controller_Status getText = controller.GetComponent<GameObject_Controller_Status>();
        text.GetComponent<Text>().text = getText.Username;
    }
}
