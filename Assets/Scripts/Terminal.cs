using UnityEngine;

public class Terminal : MonoBehaviour, Interactable {
    public DialogueManager dialogue;

	// Use this for initialization
	void Start () {
        dialogue = GameManager.instance.GetComponent<DialogueManager>();
	}

	// Update is called once per frame
	void Update () {

	}

  void Interactable.interact()
  {
        if (GameManager.instance.dialogStage == (int)DialogueStages.Start) {
            dialogue.welcome();
        } else if(GameManager.instance.dialogStage == (int)DialogueStages.ActivatedSpring) {
            dialogue.firstCoreGot();
        } else if (GameManager.instance.dialogStage == (int)DialogueStages.Planted) {
            dialogue.firstPlant();
        } else if (GameManager.instance.dialogStage == (int)DialogueStages.Harvested)
        {
            dialogue.firstHarvest();
        }
    }
}
