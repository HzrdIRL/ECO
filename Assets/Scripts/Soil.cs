using System;
using UnityEngine;

public class Soil : MonoBehaviour, Interactable {
    public bool watered;
    public GameObject plantedObject;
    public GameObject plantBlueprint;

    // Use this for initialization
    void Start () {
        watered = true;
        plantedObject = null;
    }

    
    void FixedUpdate()
    {
        
    }

    //Hyrdtae the plant, otherwise it will wilt
    void water() {
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
        else if(PlayerController.hasSpringCore)
        {
            plantNewPlant(plantBlueprint);
        }
    }
}
