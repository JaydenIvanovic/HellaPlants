using UnityEngine;
using System.Collections;

// When this scripts game object has its collider pressed/clicked,
// load the scene given.
public class SceneSwitch : MonoBehaviour 
{
	public string scene; // The scene to be switched to.

	void OnStart()
	{

	}

	void OnMouseDown()
	{
		Application.LoadLevel (scene);

		// Unpause the game if it was paused.
		if(Time.timeScale == 0)
			Time.timeScale = 1;
	}

	// Would be nice to give the user some visual feedback
	// when they click the button.
	void OnMouseEnter()
	{

	}

	void OnMouseExit()
	{

	}
}
