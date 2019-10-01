using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI sentence;

    [SerializeField]
    private string[] sentences;
    [SerializeField]
    private string[] titles;

    public int index;

    private bool isEnded;

    private float timeToNext;
    public void StartDialog()
    {
        sentence.text = "";
        isEnded = false;
        timeToNext = 0.02f;
        title.text = titles[index];
        StartCoroutine(Typing());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && sentence.text != sentences[index])
        {
            timeToNext = 0.005f;
        }
        else
        {
            timeToNext = 0.02f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && sentence.text == sentences[index])
        {
            NextSentence();
        }
    }
    IEnumerator Typing()
    {
            foreach (char letter in sentences[index].ToCharArray())
            {
                sentence.text += letter;
                yield return new WaitForSeconds(timeToNext);
                if(isEnded)
                {
                    break;
                }
            }  
    }
    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            sentence.text = "";
            title.text = titles[index];
            StartCoroutine(Typing());
        }
        else
        {
            SetTextsNull();
        }
    }
    public void SetTextsNull()
    {
        isEnded = true;
        sentence.text = "";
        title.text = "";
    }
}
