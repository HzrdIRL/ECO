using UnityEngine;

public abstract class Plant : MonoBehaviour, Interactable, Harvestable {
    public int age;
    public bool fruiting;
    public bool wilted;
    public int daysToFruit;
    public int value;
    public int cost;
    public int daysSinceHarvest;
    public SpriteRenderer sprite;
    public int season;
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
    bool Harvestable.harvest()
    {
        if (fruiting)
        {
            daysSinceHarvest = 0;
            fruiting = false;
            sprite.color = Color.blue;
            return true;
        }
        else
        {
            Debug.Log("Not fruiting yet");
            return false;
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
            sprite.color = Color.black;
        }

        if (!wilted)
        {
            if (age >= stageLengths[(int)Stages.Matured])
            {
                daysSinceHarvest++;

                if (daysSinceHarvest >= daysToFruit)
                {
                    fruiting = true;
                }

                if (fruiting)
                {
                    sprite.color = Color.red;
                } else {
                    sprite.color = Color.blue;
                }
            }
            else if (age >= stageLengths[(int)Stages.Sprouted])
            {
                sprite.color = Color.green;
            }
            else if (age >= stageLengths[(int)Stages.Planted])
            {
                sprite.color = Color.grey;
            }
        }
    }
}
