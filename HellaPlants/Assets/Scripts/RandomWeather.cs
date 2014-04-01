using UnityEngine;
using System.Collections;

public class RandomWeather : MonoBehaviour 
{
    public float MAX_SECONDS;
    public GameObject sunny, rainy, cloudy;
    private float secondsPassed;

	// Use this for initialization
	void Start () 
    {
        secondsPassed = 0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateWeatherTimer();
	}

    // Simple random method to call
    // the function which creates an instance 
    // of the weather condition.
    private void GetRandomWeather()
    {
        int rNum = Random.Range(1,4);

        if (rNum % 3 == 0)
        {
            Sunny();
        }
        else if (rNum % 3 == 1)
        {
            Rainy();
        }
        else if (rNum % 3 == 2)
        {
            Cloudy();
        }
    }

    private void Sunny()
    {
        Debug.Log("Environment: Sunny");
        GameObject sun = (GameObject) Instantiate(sunny);
        Destroy(sun, MAX_SECONDS);
    }

    private void Rainy()
    {
        Debug.Log("Environment: Rainy");
        GameObject rain = (GameObject) Instantiate(rainy);
        Destroy(rain, MAX_SECONDS);
    }

    private void Cloudy()
    {
        Debug.Log("Environment: Cloudy");
        GameObject cloud = (GameObject) Instantiate(cloudy);
        Destroy(cloud, MAX_SECONDS);
    }

    private void UpdateWeatherTimer()
    {
        if (secondsPassed >= MAX_SECONDS)
        {
            Debug.Log(secondsPassed + " -> hit 3 seconds.");
            secondsPassed = 0;
            GetRandomWeather();
        }
        else
        {
            //Debug.Log(secondsPassed);
            secondsPassed += Time.deltaTime;
        }
    }
}
