using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour 
{
	public string scene; // The scene to be switched to.

	void OnMouseDown()
	{
		Application.LoadLevel (scene);

		if(Time.timeScale == 0)
			Time.timeScale = 1;
	}

	void OnMouseEnter()
	{

	}

	void OnMouseExit()
	{

	}
}
