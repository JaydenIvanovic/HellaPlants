using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
	void OnMouseDown () 
	{
		if (Input.GetMouseButton (0))
			Application.LoadLevel ("Scene1");
	}
}
