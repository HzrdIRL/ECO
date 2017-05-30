using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioMatterHub : MonoBehaviour, Interactable {

    public PlayerController player;
    public int bioMatter;

    void Interactable.interact()
    {
        if(this.bioMatter >= 10 && player.bioMatter <= 90)
        {
            player.bioMatter += 10;
            this.bioMatter -= 10;
        } else
        {
            GameManager.instance.dialogue.insufficient();
        }
    }

    public void deposit(int amount)
    {
        this.bioMatter += amount;
    }

    // Use this for initialization
    void Start () {
        this.bioMatter = 100;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
