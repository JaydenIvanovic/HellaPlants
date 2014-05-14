using UnityEngine;
using System.Collections;

// Handles the spawning of the UFO which
// randomly appears and shoots projectiles at the flower.
public class UfoSpawner : MonoBehaviour 
{
	public GameObject leftSpawnPoint, rightSpawnPoint;
	public GameObject ufo;
	private Timer t;
	private bool canUpdate;

	void Start ()
	{
		t = new Timer(Random.Range(10,30));
		canUpdate = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if(canUpdate)
			t.updateTimer(Time.deltaTime);

		if(t.hitMaxTime())
		{
			canUpdate = false;
			Instantiate(ufo, getRandomSpawnPoint(), Quaternion.identity);
			t.resetTimer();
		}
	}

	// Randomly select the left or right spawn point.
	public Vector3 getRandomSpawnPoint()
	{
		int choice = Random.Range(1, 3);
		
		if(choice == 1)
			return leftSpawnPoint.transform.position;
		else
			return rightSpawnPoint.transform.position;
	}

	// Reset the timer now the UFO has moved offscreen.
	// Will be called by the UfoAI script.
	public void continueTimer()
	{
		canUpdate = true;
	}
}
