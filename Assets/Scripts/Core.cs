using UnityEngine;

public class Core : MonoBehaviour, Interactable  {

  	public int season;
	public GameObject machine;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().color = machine.GetComponent<Machine>().seasonColor;
	}

	// Update is called once per frame
	void Update () {

	}

    void Interactable.interact()
    {
        PlayerController.cores[season] = true;
        GameManager.instance.dialogStage++;
		Destroy(this.gameObject);
    }
}
