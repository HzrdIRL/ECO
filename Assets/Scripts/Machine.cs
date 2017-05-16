using UnityEngine;

public class Machine : MonoBehaviour, Interactable {
	public int season;
	public bool activated;

	// Use this for initialization
	void Start () {
		this.activated = false;
	}

	// Update is called once per frame
	void Update () {

	}

	void Interactable.interact() {
		if(PlayerController.cores[season])  {
      GameObject[] soils;
			this.activated = true;
			this.GetComponent<SpriteRenderer>().color = Color.green;
			if((soils = GameObject.FindGameObjectsWithTag("soil")).Length != 0) {
				foreach (GameObject soil in soils) {
					if (soil.GetComponent<Soil>().season == this.season) {
						soil.GetComponent<Soil>().activated = true;
					}
				}
			}
		}
	}
}
