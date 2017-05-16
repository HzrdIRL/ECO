using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private int time;
	private int day;
    private bool gameIsOver;

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
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		if(!gameIsOver) {
			if (time >= 10) {
				cycleDay();
			}
			if(day >= 10) {
				GameOver();
			}
		} else {
			InitGame();
		}
	}

	void InitGame() {
		time = 0;
		day = 0;
		gameIsOver = false;
	}

	void passTime() {
		time+=10;
	}

	void cycleDay() {
		Debug.Log("Times Up! Day is: " + day);
		day++;
        time = 0;

		GameObject[] soils = GameObject.FindGameObjectsWithTag("soil");
        foreach (GameObject soil in soils)
        {
            soil.GetComponent<Soil>().ageUp();
		}
	}
 
	void GameOver() {
		Debug.Log("Game Over!");
		gameIsOver = true;
	}

}
