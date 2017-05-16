using System;
using UnityEngine;

public class Soil : MonoBehaviour, Interactable {
    public bool watered;
    public GameObject plantedObject;

    // Use this for initialization
    void Start () {
        watered = true;
        plantedObject = null;
    }

    public void ageUp()
    {
        if(plantedObject != null)
            plantedObject.GetComponent<Plant>().ageUp(watered);
        watered = false;
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
        if (PlayerController.hasSpringCore && plantedObject == null)
        {
            plantNewPlant(plantBlueprint);
            plantedObject.transform.parent = this.gameObject.transform;
        }
    }
}
