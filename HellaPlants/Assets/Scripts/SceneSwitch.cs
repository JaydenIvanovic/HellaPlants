using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour 
{
	public string scene; // The scene to be switched to.

	void OnMouseDown()
	{
		Application.LoadLevel (scene);
	}

	void OnMouseEnter()
	{

	}

	void OnMouseExit()
	{

	}
}
