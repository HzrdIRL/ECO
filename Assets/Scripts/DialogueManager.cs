using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    public RPGTalk dialogue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void welcome()
    {
        dialogue.lineToStart = 4;
        dialogue.lineToBreak = 9;
        dialogue.NewTalk();
    }

    public void insufficient()
    {
        dialogue.lineToStart = 60;
        dialogue.lineToBreak = 60;
        dialogue.NewTalk();
    }

    public void firstCoreGot()
    {
        dialogue.lineToStart = 13;
        dialogue.lineToBreak = 17;
        dialogue.NewTalk();
    }

    public void firstPlant()
    {
        dialogue.lineToStart = 22;
        dialogue.lineToBreak = 24;
        dialogue.NewTalk();
    }

    public void firstHarvest()
    {
        dialogue.lineToStart = 29;
        dialogue.lineToBreak = 32;
        dialogue.NewTalk();
    }

    public void summerCore()
    {
        dialogue.lineToStart = 45;
        dialogue.lineToBreak = 45;
        dialogue.NewTalk();
    }

    public void autumnCore()
    {
        dialogue.lineToStart = 50;
        dialogue.lineToBreak = 50;
        dialogue.NewTalk();
    }

    public void winterCore()
    {
        dialogue.lineToStart = 55;
        dialogue.lineToBreak = 55;
        dialogue.NewTalk();
    }

    public void endGame()
    {
        dialogue.lineToStart = 65;
        dialogue.lineToBreak = 65;
        dialogue.NewTalk();
    }
}
