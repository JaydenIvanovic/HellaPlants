using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour 
{
	public float acceleration, distanceThreshold;
	public Vector3 direction, explosionPoint;
	public GameObject explosion;
	private BugAI bugAI;

	// Use this for initialization
	void Start () 
	{
		bugAI = GameObject.Find("Environment").GetComponent<BugAI>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += acceleration  * direction * Time.deltaTime;
	
		if(Vector3.Distance(explosionPoint, transform.position) <= distanceThreshold)
		{
			// The meteor has reached its destination.
			Instantiate(explosion, transform.position, Quaternion.identity);
			bugAI.RemoveAllBugs();
			Destroy(gameObject);
		}
	}
}
