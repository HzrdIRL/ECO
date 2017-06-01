using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(GameManager.instance.dialogStage == (int)DialogueStages.ActivatedWinter && GameObject.FindObjectOfType<BioMatterHub>().bioMatter >=500)
        {
            SceneManager.LoadScene("EndGame");
            GameManager.instance.GetComponent<AudioSource>().Stop();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
