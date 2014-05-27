using UnityEngine;
using System.Collections;

public class EnterName : MonoBehaviour 
{
	private string playerName = "Wizard";
	public GUIStyle style;
	public float left, top, width, height; // in percentages

	// Use this for initialization
	void Start () {
		left = Screen.width * left;
		top = Screen.height * top;
		width = Screen.width * width;
		height = Screen.height * height;

		style.fontSize = (int)(Screen.height * 0.12f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		// Make a text field that modifies stringToEdit.
		playerName = GUI.TextField (new Rect (left, top, width, height), playerName, 10, style);
	}

	public string GetPlayerName()
	{
		return playerName;
	}
}
