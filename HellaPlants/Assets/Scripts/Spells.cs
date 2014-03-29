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
    private bool rain_i, fertilizer_i, wind_i, sun_i; // keep track of which prefabs have been instantiated.

	// Use this for initialization
	void Start () 
    {
        rain_i = false;
        fertilizer_i = false;
        wind_i = false;
        sun_i = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Cast sun.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!sun_i)
            {
                sun_i = true;
                Instantiate(sun);
            }
        }
        // Cast rain.
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!rain_i)
            {
                rain_i = true;
                Instantiate(rain);
            }
        }
        // Cast wind.
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ;
        }
        // Cast fertilizer.
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(fertilizer);
        }
    }
}
