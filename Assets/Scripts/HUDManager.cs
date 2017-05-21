using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Text bioMatter;
    public Text water;
    public Text equippedTool;
    public Text energy;
    public Text time;
    public PlayerController player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        water.text = player.getWaterLevel().ToString();
        bioMatter.text = player.getBioMatterLevel().ToString();
        equippedTool.text = player.getEquippedTool();
        time.text = GameManager.instance.getHour().ToString("00") + ":" + GameManager.instance.getMinute().ToString("00");
        energy.text = player.getEnergyLevel().ToString("000");
    }

}
