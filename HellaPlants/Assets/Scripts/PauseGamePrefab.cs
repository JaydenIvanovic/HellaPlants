using UnityEngine;
using System.Collections;

// When this scripts game object receivies a click/press
// instatiate the given prefab and pause the game. Good
// way to load in any menu.
public class PauseGamePrefab : MonoBehaviour 
{
	public GameObject menuPrefab;

	void OnMouseDown()
	{
		Instantiate(menuPrefab);
		// Pause game. The menu should unpause itself upon destruction.
		Time.timeScale = 0; 
	}
}
