using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {
    public BioMatterHub bioMatterHub;
    public GameObject ProgressBar1, ProgressBar2, ProgressBar3, ProgressBar4, ProgressBar5;
    public GameObject PanelSpring, PanelSummer, PanelAutumn, PanelWinter;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Progress Seasons
        if (GameManager.instance.dialogStage >= (int)DialogueStages.ActivatedSpring)
        {
            PanelSpring.SetActive(true);
        }
        if(GameManager.instance.dialogStage >= (int)DialogueStages.ActivatedSummer)
        {
            PanelSummer.SetActive(true);
        }
        if (GameManager.instance.dialogStage >= (int)DialogueStages.ActivatedAutumn)
        {
            PanelAutumn.SetActive(true);
        }
        if (GameManager.instance.dialogStage >= (int)DialogueStages.ActivatedWinter)
        {
            PanelWinter.SetActive(true);
        }

        //ProgressBar
        if (bioMatterHub.bioMatter >= 100)
        {
            ProgressBar1.SetActive(true);
        } else
        {
            ProgressBar1.SetActive(false);
        }
        if (bioMatterHub.bioMatter >= 200)
        {
            ProgressBar2.SetActive(true);
        }
        else
        {
            ProgressBar2.SetActive(false);
        }
        if (bioMatterHub.bioMatter >= 300)
        {
            ProgressBar3.SetActive(true);
        }
        else
        {
            ProgressBar3.SetActive(false);
        }
        if (bioMatterHub.bioMatter >= 400)
        {
            ProgressBar4.SetActive(true);
        }
        else
        {
            ProgressBar4.SetActive(false);
        }
        if (bioMatterHub.bioMatter >= 500)
        {
            ProgressBar5.SetActive(true);
            GameManager.instance.dialogue.endGame();
        }
        else
        {
            ProgressBar5.SetActive(false);
        }
    }
}
