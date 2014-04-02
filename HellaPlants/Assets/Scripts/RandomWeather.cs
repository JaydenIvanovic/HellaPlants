using UnityEngine;
using System.Collections;

public class RandomWeather : MonoBehaviour 
{
    public float MAX_SECONDS;
    public GameObject sunny, rainy, cloudy;
	private GameObject weather;
    private float secondsPassed;
	public enum Weather {Sunny, Rainy, Cloudy};
	private Weather currentWeather;

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
			SetWeather(Weather.Sunny);
        }
        else if (rNum % 3 == 1)
        {
			SetWeather(Weather.Rainy);
        }
        else if (rNum % 3 == 2)
        {
			SetWeather(Weather.Cloudy);
        }
    }

    private void Sunny()
    {
        Debug.Log("Environment: Sunny");
        weather = (GameObject) Instantiate(sunny);
    }

    private void Rainy()
    {
        Debug.Log("Environment: Rainy");
        weather = (GameObject) Instantiate(rainy);
    }

    private void Cloudy()
    {
        Debug.Log("Environment: Cloudy");
        weather = (GameObject) Instantiate(cloudy);
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

	public Weather GetWeather()
	{
		return currentWeather;
	}

	public void SetWeather(Weather w)
	{
		currentWeather = w;
		secondsPassed = 0;

		if (weather)
			Destroy (weather);

		if (w == Weather.Sunny)
			Sunny ();
		else if (w == Weather.Rainy)
			Rainy ();
		else if (w == Weather.Cloudy)
			Cloudy ();
	}
}
