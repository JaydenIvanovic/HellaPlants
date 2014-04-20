using UnityEngine;
using System.Collections;

public class LoadLevelonClick : MonoBehaviour 
{
	public string lv;

	// Update is called once per frame
	void OnMouseDown () 
	{
		Application.LoadLevel (lv);
	}
}
