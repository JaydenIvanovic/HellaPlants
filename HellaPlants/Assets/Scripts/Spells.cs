//#define SPELLSDEBUG

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


/* 
 * Script which handles the user casting spells.
 * Key presses for now, gesture recognition will come after 
 * implementation of basic logic.
 */
public class Spells : MonoBehaviour 
{
    public GameObject fertilizer, wind, shield;
    public float MAX_SECONDS;
    private GameObject fertilizer_i, wind_i;
    private float secondsPassed;
	private GameObject environment, flower;
	private RandomWeather rw;
	private GestureMap gestureMap;

    // Use this for initialization
	void Start () 
    {
		environment = GameObject.FindGameObjectWithTag ("Environment");
        flower = GameObject.Find("Flower");
		rw = environment.GetComponent<RandomWeather> ();
        secondsPassed = 0f;
		gestureMap = new GestureMap ();
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
	public void setGesture(List<Gestures.direction> dirList)
	{
		GestureMap.Spell spell = gestureMap.GetSpell (dirList);
#if SPELLSDEBUG
		Debug.Log (spell);
#endif
		switch (spell) 
		{
			case GestureMap.Spell.Sun:
				rw.SetWeather(RandomWeather.Weather.Sunny);
				break;
			case GestureMap.Spell.Rain:
				rw.SetWeather(RandomWeather.Weather.Rainy);
				break;
			case GestureMap.Spell.Fert:
				if (!fertilizer_i)
				{
					fertilizer_i = (GameObject)Instantiate(fertilizer);
					flower.GetComponent<PlantState>().IncreaseSoil();
				}
				break;
			case GestureMap.Spell.Wind:
				if (!wind_i)
				{
					if (TimedSpellInProgress())
						Reset();
					wind_i = (GameObject)Instantiate(wind);
				}
				break;
			case GestureMap.Spell.Misc:
				if (!fertilizer_i)
				{
					fertilizer_i = (GameObject)Instantiate(fertilizer);
					flower.GetComponent<PlantState>().IncreaseSoil();
				}
				break;
			default:
				break;
		}
	
        /* //Cast fireball.
        if (dirList.Count == 1 && (dirList[0] == Gestures.direction.N))
        {
            GetComponent<Wand>().castFireball();
        }*/
#if GESTURESDEBUG 
        // Debug.Log(hmm.classifySequence(dirList));
#endif
	}


    // Reset game state.
    private void Reset()
    {
        if (fertilizer_i)
            Destroy(fertilizer_i);

        if (wind_i)
            Destroy(wind_i);

        secondsPassed = 0f;
        fertilizer_i = null;
        wind_i = null;
    }
 

    // Check how long the current weather condition has been
    // going on for and terminate it if it exceeds the limit.
    private void CheckWeatherTimer()
    {
        if (secondsPassed >= MAX_SECONDS)
        {
#if SPELLSDEBUG
            Debug.Log(secondsPassed + " -> hit 3 seconds.");
#endif
            secondsPassed = 0;
            Reset();
        }
        else
        {
#if SPELLSDEBUG
            Debug.Log(secondsPassed);
#endif
            secondsPassed += Time.deltaTime;
        }
    }


    // Check if wind or shield has been cast.
    private bool TimedSpellInProgress()
    {
        if (wind_i)
            return true;
        return false;
    }	
}
