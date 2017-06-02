using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_Flash : MonoBehaviour {
    public Text pressText;
    private Color textColor = Color.white;
    private int count = 100;
    private bool countDone = false;

    void Start () {
        textColor = pressText.color;
    }
	
	void Update () {
        if (!countDone)
        {
            textColor.a -= 0.01f;
            pressText.color = textColor;
            count--;
            if(count == 0)
            {
                countDone = true;
                count = 100;
            }
        }
        else
        {
            textColor.a += 0.01f;
            pressText.color = textColor;
            count--;
            if (count == 0)
            {
                countDone = false;
                count = 100;
            }
        }
    }
}
