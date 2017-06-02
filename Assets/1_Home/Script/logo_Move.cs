using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class logo_Move : MonoBehaviour {
    public Image logoImage;
    private int count = 100;
    private bool countDone = false;

    void Start () {
	
	}
	
	void Update () {
        if (!countDone)
        {
            count--;
            logoImage.GetComponent<RectTransform>().position += new Vector3(0,0.01f,0);
            if (count == 0)
            {
                count = 100;
                countDone = true;
            }
        }
        else
        {
            count--;
            logoImage.GetComponent<RectTransform>().position += new Vector3(0, -0.01f, 0);
            if (count == 0)
            {
                count = 100;
                countDone = false;
            }
        }
        
	}
}
