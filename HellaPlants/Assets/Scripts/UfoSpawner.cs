using UnityEngine;
using System.Collections;

// Handles the spawning of the UFO which
// randomly appears and shoots projectiles at the flower.
public class UfoSpawner : MonoBehaviour 
{
	public GameObject leftSpawnPoint, rightSpawnPoint;
	public GameObject ufo;
	private Timer t;
	private DifficultyController diff;
	private GameObject environment;
	private int UFOCount;
	private int allowedCount;
	public int requiredDifficulty;

	void Start ()
	{
		environment = GameObject.FindGameObjectWithTag ("Environment");
		diff = environment.GetComponent<DifficultyController> ();

		t = new Timer(0);
		UFOCount = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		if (diff.GetDifficulty() < requiredDifficulty)
			allowedCount = 0;
		else if (diff.GetDifficulty() < 5)
			allowedCount = 1;
		else if (diff.GetDifficulty() < 10)
			allowedCount = 2;
		else
			allowedCount = 3;

		if (allowedCount != 0){

			if(UFOCount < allowedCount)
				t.updateTimer(Time.deltaTime);

			if(t.hitMaxTime())
			{
				Instantiate(ufo, getRandomSpawnPoint(), Quaternion.identity);
				randomizeTimerMax();
				t.resetTimer();
				UFOCount++;
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
			t.setMaxSeconds (Random.Range (11f, 18f));
		if (diff.GetDifficulty () > 3 && diff.GetDifficulty() < 7)
			t.setMaxSeconds (Random.Range (9f, 16f));
		if (diff.GetDifficulty () >= 7)
			t.setMaxSeconds (Random.Range (7f, 13f));
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
		UFOCount--;
	}
}
