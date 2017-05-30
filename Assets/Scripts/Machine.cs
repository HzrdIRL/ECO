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
    public GameObject core;
	public GameObject coreJoint;

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
			core = Instantiate(core, coreJoint.transform.position, coreJoint.transform.rotation);
			SpriteRenderer coreSprite = core.GetComponent<SpriteRenderer> ();
			core.GetComponent<BoxCollider2D>().enabled = false;
			coreSprite.color = seasonColor;
			coreSprite.sortingOrder = 2;
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
