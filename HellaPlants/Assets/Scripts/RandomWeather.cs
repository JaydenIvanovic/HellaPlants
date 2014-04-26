//#define WEATHERDEBUG

using UnityEngine;
using System.Collections;

public class RandomWeather : MonoBehaviour 
{
    public float MAX_SECONDS;
    public GameObject sunny, rainy, snow;
	private GameObject weather;
	private GameObject grassPlane;
    private float secondsPassed;
	public enum Weather {Sunny, Rainy, Cloudy, Snowy};
	private Weather currentWeather;
    private GameObject cloud1, cloud2, cloud3, cloud4;
    private bool cloudy, snowy;
    private static Color32 cloudyCol;
	public AudioClip rainSnd, sunSnd, cloudSnd, snowSnd;
	private AudioClip snd;
	private float r,s,c;
	private Camera mainCamera;
	public Color stormSkyCol, normalSkyCol, normalGrndCol, snowSkyCol, snowGrndCol;

	// Use this for initialization
	void Start () 
    {
		cloudy = false;
		snowy = false;

        secondsPassed = 0f;

        cloud1 = GameObject.Find("Cloud1");
        cloud2 = GameObject.Find("Cloud2");
        cloud3 = GameObject.Find("Cloud3");
        cloud4 = GameObject.Find("Cloud4");

        cloudyCol = new Color32(77, 77, 77, 255);

		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		grassPlane = GameObject.Find("GrassPlane");

		GetRandomWeather();
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
        int rNum = Random.Range(1,5);

        if (rNum == 1)
        {
			SetWeather(Weather.Sunny);
        }
        else if (rNum == 2)
        {
			SetWeather(Weather.Rainy);
        }
        else if (rNum == 3)
        {
			SetWeather(Weather.Cloudy);
        }
		else if (rNum == 4)
		{
			SetWeather(Weather.Snowy);
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
		mainCamera.backgroundColor = stormSkyCol;
        cloudy = true;
    }

	private void Snowy()
	{
		weather = (GameObject)Instantiate(snow);
		mainCamera.backgroundColor = snowSkyCol;
		grassPlane.GetComponent<MeshRenderer>().material.color = snowGrndCol;
		snowy = true;
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

		// Destroy old particle system if it exists.
		if (weather) 
		{
			//Destroy(AudioSource);
			//Debug.Log (weather);
			Destroy (weather);
		}

		// Reset to pre cloudy conditions.
        if(cloudy)
        {
            cloud1.GetComponent<SpriteRenderer>().color = Color.white;
            cloud2.GetComponent<SpriteRenderer>().color = Color.white;
            cloud3.GetComponent<SpriteRenderer>().color = Color.white;
            cloud4.GetComponent<SpriteRenderer>().color = Color.white;
			mainCamera.backgroundColor = normalSkyCol;
            cloudy = false;
        }

		// Reset to pre snowy conditions.
		if(snowy)
		{
			mainCamera.backgroundColor = normalSkyCol;
			grassPlane.GetComponent<MeshRenderer>().material.color = normalGrndCol;
			snowy = false;
		}

		// Set the new weather.
		if (w == Weather.Sunny)
		{
			Sunny ();
			snd = sunSnd;
		}
		else if (w == Weather.Rainy) 
		{
			Rainy ();
			snd = rainSnd;
		}
		else if (w == Weather.Cloudy)
		{
			Cloudy ();
			snd = cloudSnd;
		}
		else if (w == Weather.Snowy)
		{
			Snowy();
			snd = snowSnd;
		}

		if (snd) 
		{
			Destroy(GameObject.Find("One shot audio"));
			AudioSource.PlayClipAtPoint(snd, transform.position,100F);
		}
		//Snd.SetScheduledEndTime (3);
	}
}
