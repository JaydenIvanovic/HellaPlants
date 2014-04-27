using UnityEngine;
using System.Collections;

// Destory the top most GameObject this script is attached to
// when the player clicks some collider.
public class DestoryGameObject : MonoBehaviour 
{
	void OnMouseDown()
	{
		Destroy(transform.root.gameObject);

		if(Time.timeScale == 0)
			Time.timeScale = 1;
	}
}
