using UnityEngine;

public abstract class Plant : MonoBehaviour, Interactable, Harvestable {
    public int age;
    public bool fruiting;
    public bool wilted;
    public int daysToFruit;
    public float value;
    public int daysSinceHarvest;
    public SpriteRenderer sprite;
    //public Sprite[] images = new Sprite[4];

    public int[] stageLengths = {0,1,2};

	// Use this for initialization
	void Start () {
		wilted = false;
		fruiting = false;
        age = 0;
        daysSinceHarvest = 0;
        
    }

	public void kill() {
		Destroy(this.gameObject);
	}

	void Update() {

	}

    /* 
    * Harvest plant, absorbing its value as biomatter 
    * and resetting its fruiting values
    */
    void Harvestable.harvest()
    {
        if (fruiting)
        {
            PlayerController.bioMatter += value;
            daysSinceHarvest = 0;
            fruiting = false;
        }
        else
        {
            Debug.Log("Not fruiting yet");
        }
    }

    //ToDo: Feedback on plant status when interacting
    void Interactable.interact()
    {

    }

    /*
     * Handle the aging of the flower at the end of a day cycle
     */
    public void ageUp(bool watered)
    {
        sprite = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        if (watered)
        {
            age++;
            wilted = false;
        }
        else if (wilted)
        {
            kill();
        }
        else
        {
            wilted = true;
        }

        if (!wilted)
        {
            if (age >= stageLengths[(int)Stages.Matured])
            {
                if (fruiting)
                {
                    sprite.color = Color.green;
                }
                else if (daysSinceHarvest >= daysToFruit)
                {
                    sprite.color = Color.blue;
                    fruiting = true;
                }
            }
            else if (age >= stageLengths[(int)Stages.Sprouted])
            {
                if (daysSinceHarvest == daysToFruit)
                {
                    sprite.color = Color.green;
                }
            }
            else
            {
                sprite.color = Color.blue;
            }
        }
        else
        {
            sprite.color = Color.black;
        }

        daysSinceHarvest++;
    }
}
