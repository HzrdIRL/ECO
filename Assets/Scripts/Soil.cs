using UnityEngine;

public class Soil : MonoBehaviour {
  public bool watered;
  public GameObject plant;

	// Use this for initialization
	void Start () {
      watered = true;
	}

	// Update is called once per frame
	void Update () {

  }

	void water() {
		watered = true;
	}

	public void plantNewPlant(GameObject plantObject) {
      plant = Instantiate(plantObject, this.transform.position, this.transform.rotation);
			plant.AddComponent<SimplePlant>();
  }
}
