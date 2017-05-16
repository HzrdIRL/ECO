using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, Interactable  {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Interactable.interact()
    {
        PlayerController.hasSpringCore = true;
        Destroy(this.gameObject);
    }
}
