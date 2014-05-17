using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour 
{
	public string backScene;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Escape))
			Application.LoadLevel(backScene);
	}
}
