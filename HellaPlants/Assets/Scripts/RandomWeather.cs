//#define WEATHERDEBUG

using UnityEngine;
using System.Collections;

public class RandomWeather : MonoBehaviour 
{
    public float MAX_SECONDS;
    public GameObject sunny, rainy;
	private GameObject weather;
    private float secondsPassed;
	public enum Weather {Sunny, Rainy, Cloudy};
	private Weather currentWeather;
    private GameObject cloud1, cloud2, cloud3, cloud4;
    private bool cloudy;
    private static Color32 cloudyCol;

	// Use this for initialization
	void Start () 
    {
        secondsPassed = 0f;

        cloud1 = GameObject.Find("Cloud1");
        cloud2 = GameObject.Find("Cloud2");
        cloud3 = GameObject.Find("Cloud3");
        cloud4 = GameObject.Find("Cloud4");

        cloudyCol = new Color32(77, 77, 77, 255);
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
#if WEATHERDEBUG
        Debug.Log("Environment: Sunny");
#endif
        weather = (GameObject) Instantiate(sunny);
    }

    private void Rainy()
    {
#if WEATHERDEBUG
        Debug.Log("Environment: Rainy");
#endif
        weather = (GameObject) Instantiate(rainy);
    }

    private void Cloudy()
    {
#if WEATHERDEBUG
        Debug.Log("Environment: Cloudy");
#endif
        cloud1.GetComponent<SpriteRenderer>().color = cloudyCol;
        cloud2.GetComponent<SpriteRenderer>().color = cloudyCol;
        cloud3.GetComponent<SpriteRenderer>().color = cloudyCol;
        cloud4.GetComponent<SpriteRenderer>().color = cloudyCol;
        cloudy = true;
    }

    private void UpdateWeatherTimer()
    {
        if (secondsPassed >= MAX_SECONDS)
        {
#if WEATHERDEBUG
            Debug.Log(secondsPassed + " -> hit 3 seconds.");
#endif
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

        if(cloudy)
        {
            cloud1.GetComponent<SpriteRenderer>().color = Color.white;
            cloud2.GetComponent<SpriteRenderer>().color = Color.white;
            cloud3.GetComponent<SpriteRenderer>().color = Color.white;
            cloud4.GetComponent<SpriteRenderer>().color = Color.white;
            cloudy = false;
        }

		if (w == Weather.Sunny)
			Sunny ();
		else if (w == Weather.Rainy)
			Rainy ();
		else if (w == Weather.Cloudy)
			Cloudy ();
	}
}
