using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReclaimer : MonoBehaviour, Interactable {
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Interactable.interact()
    {
        int waterLevel;
        if((waterLevel = player.GetComponent<PlayerController>().getWaterLevel()) <= 900)
        {
            player.GetComponent<PlayerController>().setWaterLevel(100);
        } else
        {
            player.GetComponent<PlayerController>().setWaterLevel(1000 - waterLevel);
        }
    }
}
