using UnityEngine;
using System.Collections;

// Keeps track of the current difficulty and increases it
// at the interval set by changeDiff. Also keeps track of
// the players score, although this should probably be moved
// to another class.
public class DifficultyController : MonoBehaviour 
{
	private int numBugs;
	public int scr;
	public static float score;
	public float changeDiff;
	public GUIStyle guiStyle;
	private string scoreStr;
	private int difflv;
	private float timeSinceLastDiff;
	private TextMesh scoreDisplay;

	// Use this for initialization
	void Start () 
	{
		scr = 0;
		score = 0;
		difflv = 0;
		timeSinceLastDiff = 0f;
		scoreDisplay = GameObject.Find("Score").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		score = Time.timeSinceLevelLoad + scr;
		scoreStr = "Score: " + ((int)score).ToString ();

		// Time to change the difficulty.
		if (timeSinceLastDiff > changeDiff) 
		{
			difflv++;
			timeSinceLastDiff = 0;
		}			

		timeSinceLastDiff += Time.deltaTime;

		// Using this instead of the GUI.Label as GUI.Label doesn't scale properly with android and ios.
		scoreDisplay.text = scoreStr;
	}

	/*
	void OnGUI() 
	{
		GUI.Label(new Rect(Screen.width * 0.01f, Screen.height * 0.01f, Screen.width * 0.3f, Screen.height * 0.3f), score, guiStyle);
	}
	*/

	// Other classes can use this to query the current difficulty
	// and act accordingly.
	public int GetDifficulty()
	{
		return difflv;
	}

	// Return the players score.
	public static float GetScore()
	{
		return score;
	}
}
