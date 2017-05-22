using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour, Harvestable {
    public int value = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool Harvestable.harvestable()
    {
        return true;
    }

    int Harvestable.harvest()
    {
        return value;
    }
}
