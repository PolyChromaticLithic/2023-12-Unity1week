using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Interaction : MonoBehaviour
{
    public int id;

    static List<Action> actions = new List<Action>
    {
        //0
        () =>
        {
            Speak.Instance.Show(speakDatas[0]);
            Debug.Log("Speak");
        },
    };

    static List<SpeakData[]> speakDatas = new List<SpeakData[]>
    {
        new SpeakData[]
        {
            new SpeakData("Test", "Hello World!\nThis is a sample text!"),
            new SpeakData("Test", "Have a good day!"),
        },
    };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        actions[id]();
    }
}
