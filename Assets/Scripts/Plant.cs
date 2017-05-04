using UnityEngine;

public abstract class Plant : MonoBehaviour {
		public int age;
		public bool fruiting;
		public bool wilted;
		public int daysToFruit = 1;
    public int daysSinceHarvest = 0;
    public SpriteRenderer sprite;
    //public Sprite[] images = new Sprite[4];

    public int[] stageLengths = {0,1,2};

    public void ageUp(bool watered)
    {
        if (watered)
        {
            age++;
        }
        else if (wilted)
        {
            kill();
        }
        else
        {
            wilted = true;
        }

        if (age >= stageLengths[(int)Stages.Matured])
        {
            sprite = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            sprite.color = Color.blue;
						if (fruiting)
            {
                sprite.color = Color.red;
                daysSinceHarvest = 0;
            }
        }
        else if (age >= stageLengths[(int)Stages.Sprouted])
        {
            if (!wilted)
            {
                if (daysSinceHarvest == daysToFruit)
                {
                    fruiting = true;
                    sprite.color = Color.green;
                }
            } else {
							sprite.color = Color.black;
						}
        }
    }


		// Use this for initialization
		void Start () {
			wilted = false;
			fruiting = false;
      age = 0;
		}

		void kill() {
			Destroy(gameObject);
		}

		void Update() {

		}
}
