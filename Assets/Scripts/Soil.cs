using UnityEngine;

public class Soil : MonoBehaviour, Interactable {
  public bool watered;
  public GameObject plantedObject;
  public GameObject plantBlueprint;
  public int season;
  public bool activated;

  // Use this for initialization
  void Start () {
    watered = true;
    plantedObject = null;
    activated = false;
  }


  void FixedUpdate()
  {

  }

  //Hyrdtae the plant, otherwise it will wilt
  public void water() {
    watered = true;
  }

  public void plantNewPlant(GameObject plantObject) {
    plantedObject = Instantiate(plantObject, this.transform.position, this.transform.rotation);
    plantedObject.AddComponent<SimplePlant>();
    plantedObject.GetComponent<SimplePlant>().season = this.season;
  }


  void Interactable.interact()
  {
    Plant plant = null;

    if (plantedObject != null && (plant = plantedObject.GetComponent<Plant>()) != null)
    {
      plant.GetComponentInChildren<Interactable>().interact();
    } else if(this.activated) {
      plantNewPlant(plantBlueprint);
    }

  }
}
