using UnityEngine;
using System.Collections;

// Gets the players score and displays it in the textmesh
// of the 3DText game object.
public class DisplayScore : MonoBehaviour 
{
	private float score;
	private TextMesh textMesh;

	void Start()
	{
		// Display score in this game objects text mesh.
		textMesh = GetComponent<TextMesh> ();
		textMesh.text = "Score: " + DifficultyController.GetScore();
	}
}
