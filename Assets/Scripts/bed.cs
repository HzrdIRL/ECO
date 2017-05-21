using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bed : MonoBehaviour, Interactable {
    void Interactable.interact()
    {
        Debug.Log("sleep");
        GameManager.instance.cycleDay();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
