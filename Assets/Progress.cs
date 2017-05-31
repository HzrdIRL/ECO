using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.dialogStage == 4)
        {
            GameObject panelObject = GameObject.FindGameObjectWithTag("PanelSpring");
        }
	}
}
