using UnityEngine;
using System.Collections;

public class PlantState : MonoBehaviour 
{
	private float health {get;set;}
    private float soil {get;set;}
    private float water {get;set;}
    private float sun {get;set;}

	// Use this for initialization
	void Awake () 
	{
		health = 100;
		soil = 100;
		water = 100;
		sun = 100;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check the difficulty.

		// Check for pests.

		// Check the weather.

        // Show visual update.
        Update3DText();
	}

    // Show the current levels for the sunlight, water, soil and
    // health of the plant until bars are implemented.
    private void Update3DText ()
    {
        GameObject plantVarTxt = GameObject.Find("PlantVariablesText");

        plantVarTxt.transform.Find("HealthLevelsText").GetComponent<TextMesh>().text = "Health: " + health + "%";
        plantVarTxt.transform.Find("WaterLevelsText").GetComponent<TextMesh>().text = "Water: " + water + "%";
        plantVarTxt.transform.Find("SoilLevelsText").GetComponent<TextMesh>().text = "Soil: " + soil + "%";
        plantVarTxt.transform.Find("SunLevelsText").GetComponent<TextMesh>().text = "Sun: " + sun + "%";
    }
}
