using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour 
{
	public float acceleration, distanceThreshold;
	public Vector3 direction, explosionPoint;
	public GameObject explosion;
	private BugAI bugAI;
	public AudioClip explosionSnd;
	private bool destroyed;

	// Use this for initialization
	void Start () 
	{
		destroyed = false;
		bugAI = GameObject.Find("Environment").GetComponent<BugAI>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += acceleration  * direction * Time.deltaTime;

		// Check if the meteor has reached its destination.
		if(Vector3.Distance(explosionPoint, transform.position) <= distanceThreshold)
		{
			// Check if the meteor has been flagged as destroyed. We give the 
			// object additional time to live to play the sound...
			if(!destroyed)
			{
				//Instantiate(explosion, transform.position, Quaternion.identity);
				audio.PlayOneShot(explosionSnd);
				bugAI.RemoveAllBugs();

				// Hide gameobject and children
				renderer.enabled = false;
				foreach(Renderer r in GetComponentsInChildren<Renderer>())
					r.enabled = false;

				destroyed = true;
				Destroy(gameObject, 2f);
			}
		}
	}
}
