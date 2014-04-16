using UnityEngine;
using System.Collections;

public class DifficultyController : MonoBehaviour 
{
	private int numBugs;
	public float scr;
	public float s;
	public float changeDiff;
	private string score;
	private int difflv = 0;
	private float current;
	private float timeSinceLastDiff;

	// Use this for initialization
	void Start () 
	{
		scr = 0;
		timeSinceLastDiff = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		s = Time.time + scr;
		score = "score: " + s.ToString ();

		if (timeSinceLastDiff > changeDiff) {
			difflv++;
			timeSinceLastDiff = 0;
		}			

		timeSinceLastDiff += Time.deltaTime;
	}

	void OnGUI() 
	{
		GUI.Label(new Rect(10, 10, 150, 100), score);
	}

	// Call other classes update difficulty method.
	public int GetDifficulty()
	{
		return difflv;
	}
}
