using UnityEngine;

public class Core : MonoBehaviour, Interactable  {

  	public int season;
	public GameObject machine;
    public float intensityEffect;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().color = machine.GetComponent<Machine>().seasonColor;
        this.GetComponentInChildren<Light>().color = this.GetComponent<SpriteRenderer>().color;
        InvokeRepeating("glow", 0.0f, 0.03f);
    }

	// Update is called once per frame
	void Update () {
        
	}

    void glow()
    {
        this.GetComponentInChildren<Light>().intensity += intensityEffect;
        if ((this.GetComponentInChildren<Light>().intensity >= 3 && intensityEffect > 0) || (this.GetComponentInChildren<Light>().intensity <= 1 && intensityEffect < 0)) {
            intensityEffect *= -1;
        }
    }

    void Interactable.interact()
    {
        PlayerController.cores[season] = true;
        GameManager.instance.dialogStage++;
		Destroy(this.gameObject);
    }
}
