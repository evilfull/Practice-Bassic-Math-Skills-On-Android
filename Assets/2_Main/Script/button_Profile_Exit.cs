using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class button_Profile_Exit : MonoBehaviour {
    public Button button;
    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        Destroy(GameObject.Find("Panel_Profile(Clone)"));
    }
}
