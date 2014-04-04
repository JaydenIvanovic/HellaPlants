using UnityEngine;
using System.Collections;

/* 
 * Script which handles the user casting spells.
 * Key presses for now, gesture recognition will come after 
 * implementation of basic logic.
 */
public class Spells : MonoBehaviour 
{
    public GameObject rain, fertilizer, wind, sun;
    public float MAX_SECONDS;
    private GameObject rain_i, fertilizer_i, wind_i, sun_i;
    private float secondsPassed;
	private GameObject environment, flower;
	private RandomWeather rw;

    // Use this for initialization
	void Start () 
    {
		environment = GameObject.FindGameObjectWithTag ("Environment");
        flower = GameObject.Find("Flower");
		rw = environment.GetComponent<RandomWeather> ();
        secondsPassed = 0f;
        Reset();
	}

	
	// Update is called once per frame
	void Update () 
    {
        bool timedSpellInProgress = TimedSpellInProgress();
        
        if(timedSpellInProgress)
            CheckWeatherTimer();
    }


	//This method is called from Gestures.cs
	//TODO: This should accept a list of dirs instead of just one
	public void setGesture(Gestures.direction dir){
		// Cast sun.
		//if (Input.GetKeyDown(KeyCode.Alpha1))
		if (dir == Gestures.direction.N)
		{
			rw.SetWeather(RandomWeather.Weather.Sunny);
		}
		// Cast rain.
		//else if (Input.GetKeyDown(KeyCode.Alpha2))
		else if (dir == Gestures.direction.S)
		{
			rw.SetWeather(RandomWeather.Weather.Rainy);
		}
		// Cast wind.
		//else if (Input.GetKeyDown(KeyCode.Alpha3))
		else if (dir == Gestures.direction.E)
		{
			if (!wind_i)
			{
				if (TimedSpellInProgress())
					Reset();
				wind_i = (GameObject)Instantiate(wind);
			}
		}
		// Cast fertilizer.
		//else if (Input.GetKeyDown(KeyCode.Alpha4))
		else if (dir == Gestures.direction.W)
		{
            if (!fertilizer_i)
            {
                fertilizer_i = (GameObject)Instantiate(fertilizer);
                flower.GetComponent<PlantState>().IncreaseSoil();
            }
		}
	}
	

    // Reset game state.
    private void Reset()
    {
        if (fertilizer_i)
            Destroy(fertilizer_i);

        if (rain_i)
            Destroy(rain_i);

        if (wind_i)
            Destroy(wind_i);

        if (sun_i)
            Destroy(sun_i);

        secondsPassed = 0f;
        fertilizer_i = null;
        rain_i = null;
        wind_i = null;
        sun_i = null;
    }
 

    // Check how long the current weather condition has been
    // going on for and terminate it if it exceeds the limit.
    private void CheckWeatherTimer()
    {
        if (secondsPassed >= MAX_SECONDS)
        {
            Debug.Log(secondsPassed + " -> hit 3 seconds.");
            secondsPassed = 0;
            Reset();
        }
        else
        {
            Debug.Log(secondsPassed);
            secondsPassed += Time.deltaTime;
        }
    }


    // Check if a weather condition has been cast and is playing out.
    // Fertilizer is not included. It doesn't need to be timed it handles 
    // its own destruction.
    private bool TimedSpellInProgress()
    {
        if (wind_i || rain_i || sun_i)
            return true;
        return false;
    }


    // Check if any spell is currently in progress.
    private bool SpellInProgress()
    {
        if (TimedSpellInProgress() || fertilizer_i)
            return true;
        return false;
    }
}
