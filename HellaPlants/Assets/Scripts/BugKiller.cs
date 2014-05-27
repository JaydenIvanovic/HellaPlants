//#define WINDDEBUG

using UnityEngine;
using System.Collections;

// Script which handles the killing of bugs by the 
// player touching them.
public class BugKiller : MonoBehaviour
{
    private BugAI bugAI;

    void Start()
    {
        bugAI = GameObject.Find("Environment").GetComponent<BugAI>();
    }

	// For wind collisions.
    void OnParticleCollision(GameObject particleSys)
    {
#if WINDDEBUG
        Debug.Log("Bug got hit by wind");
#endif
        bugAI.RemoveBug(gameObject);
    }

	// Click to kill
	void Update()
	{
		// Get the current position in screen coordinates and convert to world space.
		Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) 
		{
			// Will need to tweak this distance for touch devices...
            if( Vector2.Distance(mouse, gameObject.transform.position) < 0.8) 
			{
				bugAI.RemoveBug (gameObject);
			}
		}
	}
}
