using UnityEngine;
using System.Collections;

// When this scripts game object receivies a click/press
// instatiate the given prefab and pause the game. Good
// way to load in any menu.
public class PauseGamePrefab : MonoBehaviour 
{
	public GameObject menuPrefab;
	public string tagName;
	private bool paused;

	void OnStart()
	{
		paused = false;
	}

	void OnMouseDown()
	{
		if(!paused)
		{
			paused = true;
			Instantiate(menuPrefab);
			// Pause game. The menu should unpause itself upon destruction.
			Time.timeScale = 0;
		}
		else
		{
			paused = false;
			Destroy(GameObject.FindGameObjectWithTag(tagName));
			Time.timeScale = 1;
		}
	}

	public void UnPause()
	{
		paused = false;
	}
}
