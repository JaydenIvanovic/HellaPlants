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
	private int requiredDifficulty;
	private DifficultyController diff;
	private GameObject environment;

	void Start ()
	{
		environment = GameObject.FindGameObjectWithTag ("Environment");
		diff = environment.GetComponent<DifficultyController> ();

		t = new Timer(0);
		randomizeTimerMax ();
		canUpdate = true;
		requiredDifficulty = 1;
	}

	// Update is called once per frame
	void Update () 
	{
		if (diff.GetDifficulty() >= requiredDifficulty){
			if(canUpdate)
				t.updateTimer(Time.deltaTime);

			if(t.hitMaxTime())
			{
				canUpdate = false;
				Instantiate(ufo, getRandomSpawnPoint(), Quaternion.identity);
				randomizeTimerMax();
				t.resetTimer();
			}
		}
	}

	//Chooses the time before the next UFO based on difficulty
	private void randomizeTimerMax()
	{
		if (diff.GetDifficulty () == 0)
			t.setMaxSeconds (Random.Range (20f, 30f));
		if (diff.GetDifficulty () == 1)
			t.setMaxSeconds (Random.Range (15f, 25f));
		if (diff.GetDifficulty () == 2)
			t.setMaxSeconds (Random.Range (12f, 20f));
		if (diff.GetDifficulty () == 3)
			t.setMaxSeconds (Random.Range (9f, 15f));
		if (diff.GetDifficulty () > 3)
			t.setMaxSeconds (Random.Range (7f, 12f));
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
