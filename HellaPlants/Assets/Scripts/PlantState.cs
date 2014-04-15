using UnityEngine;
using System.Collections;

public class PlantState : MonoBehaviour 
{
	private float health {get;set;}
    private float soil {get;set;}
    private float water {get;set;}
    private float sun {get;set;}
    public int growthSecs;
    private RandomWeather rw;
    private SpriteRenderer spriteR;
    private Timer growthTimer;
    private uint growthLevel;
    public Sprite f1, f2, f3, f4, f5;

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
        spriteR = GetComponent<SpriteRenderer>();
        growthTimer = new Timer(growthSecs);
        growthLevel = 0;
    }

	// Update is called once per frame
	void Update () 
	{
        // Handle plant growth.
        growthTimer.updateTimer(Time.deltaTime);
        if (growthTimer.hitMaxTime())
        {
            Grow();
            growthTimer.resetTimer();
        }

		// Check the weather.
        if (rw.GetWeather() == RandomWeather.Weather.Sunny)
        {
            sun = ValueFilter(sun, 0.2f);
            water = ValueFilter(water, -0.1f);
        }
        else if (rw.GetWeather() == RandomWeather.Weather.Rainy)
        {
            sun = ValueFilter(sun, -0.1f);
            water = ValueFilter(water, 0.2f);
        }
        else if (rw.GetWeather() == RandomWeather.Weather.Cloudy)
        {
            sun = ValueFilter(sun, -0.3f);
            water = ValueFilter(water, -0.3f);
        }

        // Soil always gradually depletes.
        soil = ValueFilter(soil, -0.09f);

        // We don't want it to replenish if it
        // has been emptied by a bug.
        if(health > 0)
            // Health always gradually replenishes.
            health = ValueFilter(health, 0.1f);

        // Show visual update.
        Update3DText();
	}

    // Show the current levels for the sunlight, water, soil and
    // health of the plant until bars are implemented.
    private void Update3DText ()
    {
        GameObject plantVarTxt = GameObject.Find("PlantVariablesText");

        plantVarTxt.transform.Find("HealthLevelsText").GetComponent<TextMesh>().text = "Health: " +  Mathf.Round(health) + "%";
        plantVarTxt.transform.Find("WaterLevelsText").GetComponent<TextMesh>().text = "Water: " + Mathf.Round(water) + "%";
        plantVarTxt.transform.Find("SoilLevelsText").GetComponent<TextMesh>().text = "Soil: " + Mathf.Round(soil) + "%";
        plantVarTxt.transform.Find("SunLevelsText").GetComponent<TextMesh>().text = "Sun: " + Mathf.Round(sun) + "%";
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

    // Spells script will send a message to the flower
    // that it should run this method as the fertilizer spell
    // has been cast.
    public void IncreaseSoil()
    {
        // Has to be a larger increase as this gets called once
        // per spell cast and soil always decreases in the update method
        // of this script.
        soil = ValueFilter(soil, 25f); 
    }

    // Called by the BugAI script when the bug is attacking the plant.
    public void TakeDamage()
    {
        health = ValueFilter(health, -0.5f);
    }

    // Change the plant to its next 'growth form'.
    private void Grow()
    {
        if(growthLevel == 0)
            spriteR.sprite = f1;
        else if(growthLevel == 1)
            spriteR.sprite = f2;
        else if(growthLevel == 2)
            spriteR.sprite = f3;
        else if(growthLevel == 3)
            spriteR.sprite = f4;
        else if(growthLevel == 4)
            spriteR.sprite = f5;

        growthLevel++;
    }
}
