using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class AnswerCheck : MonoBehaviour, IDropHandler
{
    Transform answerParent = null;
    public bool done = false,finishQuest = false;
    public Text answerText;
    public AudioClip effect;
    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        answerParent = this.transform;
    }
    
    void Update()
    {
        if (done)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (Transform slot in answerParent)
            {
                if(slot.name != "New Game Object")
                {
                    builder.Append(slot.name);
                }
            }
            answerText.text = builder.ToString();
            gameplay g = GameObject.Find("Controller").GetComponent<gameplay>();
            int answer1, answer2;
            answer1 = g.answer;
            int.TryParse(builder.ToString(), out answer2);
            if (answer1 == answer2)
            {
                sound.PlayOneShot(effect, 1f);
                int answer;
                int.TryParse(builder.ToString(), out answer);
                answerText.text = answer.ToString("N0") + "\n ถูกต้อง";
                finishQuest = true;
            }
            done = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        done = true;
    }

    
}
