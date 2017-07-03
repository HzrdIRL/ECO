using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private int time;
	private int day;
    public bool gameIsOver;
    public int dialogStage;
    public GameObject player;
    public GameObject bed;
    public DialogueManager dialogue;
    public GameObject SummerCore, AutumnCore, WinterCore;

    void Awake() {
		if (instance == null) {
			instance = this;
		}

		else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		InitGame();
	}

	void Start() {
		InvokeRepeating("passTime", 0.0f, 2.0f);
        dialogStage = 0;
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	void FixedUpdate () {
		if(!gameIsOver) {
			if (time >= 1440) {
				cycleDay();
			}
		} else {
			//InitGame();
		}
	}

	void InitGame() {
		time = 540;
		day = 0;
		gameIsOver = false;
	}

	void passTime() {
		time+=5;
	}

    public int getHour()
    {
        return this.time/60;
    }

    public int getMinute()
    {
        return this.time%60;
    }

    public void EndGame()
    {
        Debug.Log("dialogue over");
        if (this.dialogStage >= (int)DialogueStages.ActivatedWinter && GameObject.FindObjectOfType<BioMatterHub>().bioMatter >= 500)
        {
            SceneManager.LoadScene("EndGame");
            this.GetComponent<AudioSource>().Stop();
        }
    }

    public void cycleDay() {
        Debug.Log("Times Up! Day is: " + day);
        day++;
        time = 540;
        player.transform.position = new Vector3(bed.transform.position.x - 1, bed.transform.position.y, player.transform.position.z);
        player.GetComponent<PlayerController>().energyLevel = 100;

        GameObject[] soils = GameObject.FindGameObjectsWithTag("soil");
        foreach (GameObject soil in soils)
        {
            soil.GetComponent<Soil>().ageUp();
        }
        if (dialogStage == (int)DialogueStages.Harvested)
        {
            if (SummerCore != null)
            {
                SummerCore.SetActive(true);
            }
        }
        if (dialogStage == (int)DialogueStages.ActivatedSummer)
        {
            if (AutumnCore != null)
            {
                AutumnCore.SetActive(true);
            }
        }
        if (dialogStage == (int)DialogueStages.ActivatedAutumn)
        {
            if (WinterCore != null)
            {
                WinterCore.SetActive(true);
            }
        }
    }
 
	void GameOver() {
		Debug.Log("Game Over!");
		//gameIsOver = true;
	}

}
