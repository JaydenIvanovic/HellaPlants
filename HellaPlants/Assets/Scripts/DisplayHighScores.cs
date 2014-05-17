using UnityEngine;
using System.Collections;

public class DisplayHighScores : MonoBehaviour 
{
	private ScoreManager scoreManager;

	// Use this for initialization
	void Start () 
	{
		scoreManager = GetComponent<ScoreManager>();
		scoreManager.Load ();

		int count = 0;
		// Populate the high scores.
		foreach ( TextMesh tm in GetComponentsInChildren<TextMesh>() )
			tm.text = "Wizard: " + scoreManager.GetScores()[count++];
	}
}
