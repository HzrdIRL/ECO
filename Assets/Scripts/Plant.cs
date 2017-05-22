using UnityEngine;

public abstract class Plant : MonoBehaviour, Interactable, Harvestable
{
    public int age;
    public bool fruiting;
    public bool wilted;
    public int daysToFruit;
    public int value;
    public int cost;
    public int daysSinceHarvest;
    public SpriteRenderer sprite;
    public Sprite spriteSeed;
    public Sprite spriteSapling;
    public Sprite spriteMatured;
    public int season;
    //public Sprite[] images = new Sprite[4];

    public int[] stageLengths = { 0, 1, 2 };

    // Use this for initialization
    void Start()
    {
        wilted = false;
        fruiting = false;
        age = 0;
        daysSinceHarvest = 0;

    }

    public void kill()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {

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
            sprite.sprite = spriteSapling;
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
            sprite.color = Color.white;
        }
        else if (wilted)
        {
            kill();
        }
        else
        {
            wilted = true;
            sprite.color = Color.grey;
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
                    sprite.sprite = spriteMatured;
                }
                else
                {
                    sprite.sprite = spriteSapling;
                }
            }
            else if (age >= stageLengths[(int)Stages.Sprouted])
            {
                sprite.sprite = spriteSapling;
            }
        }
    }
}
