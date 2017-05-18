using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Text bioMatter;
    public Text water;
    public Text equippedTool;
    public PlayerController player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        water.text = player.getWaterLevel().ToString();
        bioMatter.text = player.getBioMatterLevel().ToString();
        equippedTool.text = player.getEquippedTool();
    }

}
