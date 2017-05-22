using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour, Interactable
{
    public int season;
    public bool activated;
    public GameObject machineBGObject;
    private SpriteRenderer machineBGSprite;
    public Color seasonColor;
    private bool powering, powered;

    // Use this for initialization
    void Start()
    {
        this.activated = false;
        this.machineBGSprite = machineBGObject.GetComponent<SpriteRenderer>();
        machineBGSprite.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Interactable.interact()
    {
        if (PlayerController.cores[season] && !this.activated)
        {
            GameObject[] soils;
            this.activated = true;
            machineBGSprite.color = seasonColor;

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
