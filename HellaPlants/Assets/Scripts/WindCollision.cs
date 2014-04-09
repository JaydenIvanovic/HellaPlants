//#define WINDDEBUG

using UnityEngine;
using System.Collections;

public class WindCollision : MonoBehaviour
{
    private BugAI bugAI;
	public GameObject blood;

    void Start()
    {
        bugAI = GameObject.Find("Environment").GetComponent<BugAI>();
    }

    void OnParticleCollision(GameObject particleSys)
    {
#if WINDDEBUG
        Debug.Log("Bug got hit by wind");
#endif
        bugAI.RemoveBug(gameObject);
    }

	void Update()
	{
		Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) 
		{
			Debug.Log(Vector2.Distance (mouse, gameObject.transform.position));
			if (Vector2.Distance (mouse, gameObject.transform.position) < 0.4) 
			{
				Debug.Log ("Inner if");
				bugAI.RemoveBug (gameObject);
				Instantiate(blood, gameObject.transform.position, gameObject.transform.rotation);
			}
		}
	}
}
