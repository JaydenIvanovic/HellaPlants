using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour 
{
	private bool activated = false;
	private const int COMPLETED = 1;
	public string key{get;set;}

	// Use this for initialization
	void Start () 
	{
		if(PlayerPrefs.GetInt(key) == COMPLETED)
			activated  = false;
		else
			activated  = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated)
		{
			// Check for the first occurence of different
			// game scenarios and help the player!
		}
	}

	// Record that the user has completed the tutorial.
	public void SaveProgress()
	{
		PlayerPrefs.SetInt(key, COMPLETED);
	}
}
