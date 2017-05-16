using UnityEngine;

public class Core : MonoBehaviour, Interactable  {

  public int season;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    void Interactable.interact()
    {
        PlayerController.cores[season] = true;
        Destroy(this.gameObject);
    }
}
