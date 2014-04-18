using UnityEngine;
using System.Collections;

public class DisplayScore : MonoBehaviour {

	private float score;
	private TextMesh textMesh;
	
	void Start()
	{
		// Get the score from the previous scenes environment object.
		GameObject environment = GameObject.Find ("Environment");
		score = environment.GetComponent<DifficultyController>().GetScore();
		Destroy (environment);

		// Display score in this game objects text mesh.
		textMesh = GetComponent<TextMesh> ();
		textMesh.text = "Score: " + score;
	}
}
