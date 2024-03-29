﻿using UnityEngine;
using System.Collections;

public class PlantState : MonoBehaviour 
{
	private float health {get;set;}
    private float soil {get;set;}
    private float water {get;set;}
    private float sun {get;set;}
    private RandomWeather rw;

	// Use this for initialization
	void Awake () 
	{
		health = 100;
		soil = 100;
		water = 100;
		sun = 100;	
	}
	
    void Start()
    {
        GameObject environ = GameObject.FindGameObjectWithTag("Environment");
        rw = environ.GetComponent<RandomWeather>();
    }

	// Update is called once per frame
	void Update () 
	{
		// Check the difficulty.

		// Check for pests.

		// Check the weather.
        if (rw.GetWeather() == RandomWeather.Weather.Sunny)
        {
            sun = ValueFilter(sun, 0.08f);
            water = ValueFilter(water, -0.06f);
        }
        else if (rw.GetWeather() == RandomWeather.Weather.Rainy)
        {
            sun = ValueFilter(sun, -0.06f);
            water = ValueFilter(water, 0.08f);
        }
        else if (rw.GetWeather() == RandomWeather.Weather.Cloudy)
        {
            sun = ValueFilter(sun, -0.09f);
            water = ValueFilter(water, -0.09f);
        }

        // Soil always gradually depletes.
        soil = ValueFilter(soil, -0.03f);

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

    // Ensure that the number is always between 0 and 100.
    private float ValueFilter(float val, float inc)
    {
        float deltaVal = val + inc;

        if (deltaVal < 0)
            return 0;
        else if (deltaVal > 100)
            return 100;
        return deltaVal;
    }
}
