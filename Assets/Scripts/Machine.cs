using UnityEngine;

public class Machine : MonoBehaviour, Interactable
{
    public int season;
    public bool activated;
    public GameObject core;

    // Use this for initialization
    void Start()
    {
        this.activated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Interactable.interact()
    {
        if (PlayerController.cores[season] && this.activated == false)
        {
            GameObject[] soils;
            this.activated = true;
            Instantiate(core, new Vector3(this.transform.position.x+4, this.transform.position.y+1, this.transform.position.z), this.transform.rotation);
            this.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            if ((soils = GameObject.FindGameObjectsWithTag("soil")).Length != 0)
            {
                foreach (GameObject soil in soils)
                {
                    if (soil.GetComponent<Soil>().season == this.season)
                    {
                        soil.GetComponent<Soil>().activated = true;
                    }
                }
            }
        }
    }
}
