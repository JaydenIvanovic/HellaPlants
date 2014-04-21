using UnityEngine;
using System.Collections;

public class DifficultyController : MonoBehaviour 
{
	private int numBugs;
	public float scr;
	public float s;
	public float changeDiff;
	public GUIStyle guiStyle;
	private string score;
	private int difflv;
	private float timeSinceLastDiff;
	private TextMesh scoreDisplay;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (gameObject);
		scr = 0;
		s = 0;
		difflv = 0;
		timeSinceLastDiff = 0f;
		scoreDisplay = GameObject.Find("Score").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		s = Time.timeSinceLevelLoad + scr;
		score = "Score: " + s.ToString ();

		if (timeSinceLastDiff > changeDiff) {
			difflv++;
			timeSinceLastDiff = 0;
		}			

		timeSinceLastDiff += Time.deltaTime;

		// Using this instead of the GUI.Label as GUI.Label doesn't scale properly with android and ios.
		scoreDisplay.text = score;
	}

	/*
	void OnGUI() 
	{
		GUI.Label(new Rect(Screen.width * 0.01f, Screen.height * 0.01f, Screen.width * 0.3f, Screen.height * 0.3f), score, guiStyle);
	}
	*/

	// Call other classes update difficulty method.
	public int GetDifficulty()
	{
		return difflv;
	}

	public float GetScore()
	{
		return s;
	}
}
