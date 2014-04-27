using UnityEngine;
using System.Collections;

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
