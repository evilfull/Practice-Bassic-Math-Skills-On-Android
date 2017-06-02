using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Music_Setting : MonoBehaviour {
    public Slider music_slider;
    public Text text;
    public float value;
    public int valueInt;

	void Start () {
        music_slider.onValueChanged.AddListener(OnMusicChange);
	}

    private void OnMusicChange(float arg0)
    {
        value = music_slider.value * 100;
        valueInt = (int)value;
        text.GetComponent<Text>().text = valueInt.ToString() + "%";
    }
}
