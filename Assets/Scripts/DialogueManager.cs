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

    public void firstCore()
    {
        dialogue.lineToStart = 13;
        dialogue.lineToBreak = 17;
        dialogue.NewTalk();
    }

    public void firstPlant()
    {
        dialogue.lineToStart = 2;
        dialogue.lineToBreak = 2;
        dialogue.NewTalk();
    }

    public void firstWater()
    {
        dialogue.lineToStart = 2;
        dialogue.lineToBreak = 2;
        dialogue.NewTalk();
    }

    public void firstHarvest()
    {
        dialogue.lineToStart = 2;
        dialogue.lineToBreak = 2;
        dialogue.NewTalk();
    }
}
