using UnityEngine;
using System.Collections;

// Representation of the plants current state.
// How much water, sunlight, soil and health it has,
// the sprite which represents its current growth state etc.
public class PlantState : MonoBehaviour 
{
	private float health {get;set;}
    private float soil {get;set;}
    private float water {get;set;}
    private float sun {get;set;}
	private bool vulnerable, regenerate;
    public int growthSecs;
    private RandomWeather rw;
	private DifficultyController diffContr;
    private SpriteRenderer spriteR;
	private GameObject environ, growthParticles_i;
    private uint growthLevel;
    public Sprite f1, f2, f3, f4, f5;
	public GameObject growthParticles;
	private const int STOP_GROWTH = 6;

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
        environ = GameObject.FindGameObjectWithTag("Environment");
        rw = environ.GetComponent<RandomWeather>();
		diffContr = environ.GetComponent<DifficultyController> ();
        spriteR = GetComponent<SpriteRenderer>();
        growthLevel = 0;
		vulnerable = true;
    }

	// Update is called once per frame
	void Update () 
	{
        // Handle plant growth.
		if(diffContr.GetDifficulty() > growthLevel && diffContr.GetDifficulty() < STOP_GROWTH)
			Grow();

		// Check the weather.
        if (rw.GetWeather() == RandomWeather.Weather.Sunny)
        {
			sun = ValueFilter(sun, Time.deltaTime * 5.5f);
			water = ValueFilter(water, Time.deltaTime * -4f);
        }
        else if (rw.GetWeather() == RandomWeather.Weather.Rainy)
        {
            sun = ValueFilter(sun, Time.deltaTime * -4f);
            water = ValueFilter(water, Time.deltaTime * 5.5f);
        }
        else if (rw.GetWeather() == RandomWeather.Weather.Cloudy)
        {
            sun = ValueFilter(sun, Time.deltaTime * -5f);
            water = ValueFilter(water, Time.deltaTime * -5f);
        }
		else if (rw.GetWeather() == RandomWeather.Weather.Snowy)
		{
			TakeDamage(8f);
		}

        // Soil always gradually depletes.
        soil = ValueFilter(soil, Time.deltaTime * -2f);

		// If sun, water, or soil bar are at 0 then damage
		// the plant.
		if (sun <= 0)
			TakeDamage (4f);
		if (water <= 0)
			TakeDamage (4f);
		if (soil <= 0)
			TakeDamage (4f);

        // We don't want it to replenish if it
        // has been emptied by a bug.
        if (health > 0)
            // Health always gradually replenishes.
			health = ValueFilter (health, Time.deltaTime * 1f);
		else
			Application.LoadLevel ("GameOver");

		// The plant is regenerating, be generous and regenerate
		// sun, water and soil(effectively reducing the rate it decreases
		// due to weather) in addition to health.
		if(regenerate)
		{
			health = ValueFilter (health, Time.deltaTime * 8f);
			sun = ValueFilter(sun, Time.deltaTime * 8f);
			water = ValueFilter(water, Time.deltaTime * 8f);
			soil = ValueFilter(soil, Time.deltaTime * 8f);
		}

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

        /*Update bars*/
        plantVarTxt.transform.Find("SunBar").GetComponent<HealthBar>().updateDisplay(sun / 100);
        plantVarTxt.transform.Find("WaterBar").GetComponent<HealthBar>().updateDisplay(water / 100);
        plantVarTxt.transform.Find("SoilBar").GetComponent<HealthBar>().updateDisplay(soil / 100);
        plantVarTxt.transform.Find("HealthBar").GetComponent<HealthBar>().updateDisplay(health / 100);
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
        soil = ValueFilter(soil, 100f); 
    }
	
	// Version of TakeDamage where the damage can be passed as an parameter.
	// Called by this script when sun, water, or soil are at 0.
	// Called by the BugAI script when the bug is attacking the plant.
	public void TakeDamage(float damage)
	{
		if(vulnerable)
			health = ValueFilter (health, Time.deltaTime * -damage);
	}

	// aka, not damage over time so deltaTime isn't taken into consideration.
	public void TakePureDamage(float damage)
	{
		if(vulnerable)
			health = ValueFilter (health, -damage);
	}

    // Change the plant to its next 'growth form'.
    private void Grow()
    {
		//Debug.Log ("Difficulty");
		// Bring particles in front of plant with z change.
		growthParticles_i = Instantiate(growthParticles, 
		                                new Vector3(transform.position.x, transform.position.y, -4), 
		                                transform.rotation) as GameObject;
		Invoke("DestroyGrowthParticles", 2);
		audio.Play();

		growthLevel++;

        if(diffContr.GetDifficulty() == 1)
            spriteR.sprite = f1;
		else if(diffContr.GetDifficulty() == 2)
            spriteR.sprite = f2;
		else if(diffContr.GetDifficulty() == 3)
            spriteR.sprite = f3;
		else if(diffContr.GetDifficulty() == 4)
            spriteR.sprite = f4;
		else if(diffContr.GetDifficulty() == 5)
            spriteR.sprite = f5;
    }

	// Set the plant to be vulnerable or invulnerable.
	public void Vulnerable(bool vulnerable)
	{
		this.vulnerable = vulnerable;
	}

	// Tell the plant that it can regenerate at a rapid rate.
	public void Regenerate(bool regen)
	{
		this.regenerate = regen;
	}

	private void DestroyGrowthParticles()
	{
		if(growthParticles_i)
			Destroy(growthParticles_i);
	}
}
