using UnityEngine;

public class Terminal : MonoBehaviour, Interactable {
    
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

  void Interactable.interact()
  {
        if (GameManager.instance.dialogStage == (int)DialogueStages.Start) {
            GameManager.instance.dialogue.welcome();
        } else if(GameManager.instance.dialogStage == (int)DialogueStages.ActivatedSpring) {
            GameManager.instance.dialogue.firstCoreGot();
        } else if (GameManager.instance.dialogStage == (int)DialogueStages.Planted) {
            GameManager.instance.dialogue.firstPlant();
        } else if (GameManager.instance.dialogStage == (int)DialogueStages.Harvested)
        {
            GameManager.instance.dialogue.firstHarvest();
        }
    }
}
