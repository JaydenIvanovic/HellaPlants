using UnityEngine;
using System.Collections;

// Destory the top most GameObject this script is attached to
// when the player clicks some collider. Currently used to
// remove in game menu.
public class DestoryGameObject : MonoBehaviour 
{
	void OnMouseDown()
	{
		Destroy(transform.root.gameObject);

		// Unpause the game if it was paused.
		if(Time.timeScale == 0)
			Time.timeScale = 1;
	}
}
