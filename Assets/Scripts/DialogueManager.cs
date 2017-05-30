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
        dialogue.lineToBreak = 8;
        dialogue.NewTalk();
    }

    public void insufficient()
    {
        dialogue.lineToStart = 58;
        dialogue.lineToBreak = 58;
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

    public void firstWater()
    {
        dialogue.lineToStart = 29;
        dialogue.lineToBreak = 30;
        dialogue.NewTalk();
    }

    public void firstHarvest()
    {
        dialogue.lineToStart = 35;
        dialogue.lineToBreak = 38;
        dialogue.NewTalk();
    }
}
