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
        int waterLevel = player.GetComponent<PlayerController>().waterLevel;
        player.GetComponent<PlayerController>().setWaterLevel(100 - waterLevel);
    }
}
