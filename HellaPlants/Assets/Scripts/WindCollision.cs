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
//click to kill
	void Update()
	{
		Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) 
		{
            if( Vector2.Distance(mouse, gameObject.transform.position) < 0.6) 
			{
				bugAI.RemoveBug (gameObject);
				Instantiate(blood, gameObject.transform.position, gameObject.transform.rotation);
			}
		}
	}
}
