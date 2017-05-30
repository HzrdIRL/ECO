using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Text bioMatter;
    public RectTransform bioBar;
    public RectTransform waterBar;
    public Image harvesterImage;
    public Image hydraterImage;
    public Image cultivatorImage;
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
        bioBar.localScale = new Vector3(player.getBioMatterLevel()/100f, bioBar.localScale.y, bioBar.localScale.z);
        waterBar.localScale = new Vector3(player.getWaterLevel()/100f, waterBar.localScale.y, waterBar.localScale.z);
        if(player.getEquippedTool().Equals("Harvester"))
        {
            harvesterImage.color = Color.green;
            hydraterImage.color = Color.grey;
            cultivatorImage.color = Color.grey;
        } else if(player.getEquippedTool().Equals("Hydrater"))
        {
            harvesterImage.color = Color.grey;
            hydraterImage.color = Color.green;
            cultivatorImage.color = Color.grey;
        } else if (player.getEquippedTool().Equals("Cultivator"))
        {
            harvesterImage.color = Color.grey;
            hydraterImage.color = Color.grey;
            cultivatorImage.color = Color.green;
        }
        equippedTool.text = player.getEquippedTool();
        time.text = GameManager.instance.getHour().ToString("00") + ":" + GameManager.instance.getMinute().ToString("00");
        energy.text = player.getEnergyLevel().ToString("000");
    }

}
