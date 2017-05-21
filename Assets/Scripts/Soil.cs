using UnityEngine;

public class Soil : MonoBehaviour, Interactable {
  public bool watered;
  public GameObject plantedObject;
  public int season;
  public bool activated;

    // Use this for initialization
    void Start () {
    watered = false;
        plantedObject = null;
        activated = false;
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void ageUp()
    {
        if(plantedObject != null)
            plantedObject.GetComponent<Plant>().ageUp(watered);
        watered = false;
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }


  void FixedUpdate()
  {

  }

  //Hyrdtae the plant, otherwise it will wilt
  public void water() {
    watered = true;
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
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
        }
    }

    //Plant new flora in soil
    public void cultivate(GameObject plantBlueprint)
    {
        if (this.activated && plantedObject == null)
        {
            plantNewPlant(plantBlueprint);
            plantedObject.transform.parent = this.gameObject.transform;
        }
    }
}
