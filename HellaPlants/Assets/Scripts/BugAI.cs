//#define DEBUG

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class handles the spawning of bugs
// and the handling of bug logic. It may make sense
// to split the two into seperate classes in the future assignment.
public class BugAI : MonoBehaviour 
{
	public GameObject redbug, bluebug, plant, blood, points;
	private List<GameObject> bugs;
	public float speed;
	private int numBugsAtt;
	private const float CLOSE_TO_PLANT = 1.5f;
	private int numBugs = 0;
    private Timer attackTimer;
	public float initalSpwnRate;
	private float minSpwnRate, maxSpwnRate;
	public AudioClip squash;
	private DifficultyController diff;
	public int requiredDifficulty;
	private GameObject environment;
	private PlantState plantState;

	// Use this for initialization
	void Start () 
	{
		environment = GameObject.FindGameObjectWithTag ("Environment");
		plantState = GameObject.Find ("Flower").GetComponent<PlantState> ();
		diff = environment.GetComponent<DifficultyController> ();
        bugs = new List<GameObject>();
        attackTimer = new Timer(initalSpwnRate);
	}
	
	// Update is called once per frame
	void Update () 
	{
        // See if anymore bugs should be created.
        CreateBugs();
		foreach(GameObject bug in bugs)
		{
			// Check distance to plant.
			if (CloseToPlant (plant, bug)) 
				// Difficulty level is added to damage given by bug to really make the player scared of bugs!
                plant.GetComponent<PlantState>().TakeDamage(4 + diff.GetDifficulty());
			else 
				MoveToPlant(bug);
		}
	}

	// Creates a new bug and adds it to the list.
	private void InstantiateBug()
	{
		// Randomize the bug prefab to be selected.
		int rNum = Random.Range(1,3);
		float xpos = 0;
		float ypos = 0;

		// If we don't want the sneaky bug we should uncomment this as 
		// it prevents the bug from appearing directly on the flower.
		/*
		do {
			xpos = Random.Range (-14F, 14F); 
			ypos = Random.Range (-6F, 4F); 
		} while((xpos < 6F && xpos > -6F) && (ypos < 3F)); 
		*/

		// Random position the bug should appear at.
		//xpos = Random.Range (-14F, 14F); 
		//ypos = Random.Range (-6F, 4F); 

		//Give the bug an equal chance of spawning on each of the four sides
		switch(Random.Range (1,5))
		{
		case 1:
			xpos = 11.5f;
			ypos = Random.Range (-6.5f,6.5f);
			break;
		case 2:
			xpos = -11.5f;
			ypos = Random.Range (-6.5f,6.5f);
			break;
		case 3:
			ypos = 6.5f;
			xpos = Random.Range (-11.5f,11.5f);
			break;
		case 4:
			ypos = -6.5f;
			xpos = Random.Range (-11.5f,11.5f);
			break;
		}
		
		if (rNum == 1)
			bugs.Add((GameObject)Instantiate (redbug, new Vector3(xpos, ypos, redbug.transform.position.z), Quaternion.identity));
		else if (rNum == 2)
			bugs.Add((GameObject)Instantiate (bluebug, new Vector3(xpos, ypos, bluebug.transform.position.z), Quaternion.identity));

		numBugs++;
	}

    // Makes a check to see if a bug should be created.
    // If so, the bug is created.
	private void CreateBugs()
	{
		if (diff.GetDifficulty() >= requiredDifficulty){
        	attackTimer.updateTimer(Time.deltaTime);

        	if (attackTimer.hitMaxTime())
        	{
				// As difficulty increases the maximum and minimum spawn rate will decrease.
				float max = initalSpwnRate  * (Mathf.Pow(2, -diff.GetDifficulty() * 0.5f)); 
				float min = initalSpwnRate * (Mathf.Pow(10, -diff.GetDifficulty() * 0.5f)); 

				// Clamping the spawn rates to a number which is still reasonable for
				// the player to handle.
				maxSpwnRate = Mathf.Clamp(max, 0.5f, initalSpwnRate);
				minSpwnRate = Mathf.Clamp(min, 0.5f, initalSpwnRate);

       	     	InstantiateBug();
        	    attackTimer.setMaxSeconds(Random.Range(minSpwnRate, maxSpwnRate));
       	     	attackTimer.resetTimer();
        	}
		}
	}

    // Checks whether the bug is close to the plant. 
	private bool CloseToPlant(GameObject plant, GameObject bug)
	{
		if (Vector2.Distance (plant.transform.position, bug.transform.position) <= CLOSE_TO_PLANT)
			return true; 
		return false;
	}

    // Moves the bug towards the plant.
	private void MoveToPlant(GameObject bug)
	{
		bug.transform.LookAt(bug.transform.position + new Vector3(0,0,1), plantState.transform.position  - bug.transform.position);
		// Move the bugs x position closer to the plant.
		if (bug.transform.position.x < 0) 
			bug.transform.position += Time.deltaTime * Vector3.right * (float)(diff.GetDifficulty() * 0.1f + 1);
		else if (bug.transform.position.x > 0)
			bug.transform.position += Time.deltaTime * Vector3.left * (float)(diff.GetDifficulty() * 0.1f + 1);
		// Move the bugs y position closer to the plant.
		if (bug.transform.position.y > -2.65F)
			bug.transform.position += Time.deltaTime * Vector3.down * (float)(diff.GetDifficulty() * 0.1f + 1);
		else if (bug.transform.position.y < -2.65F)
			bug.transform.position += Time.deltaTime * Vector3.up * (float)(diff.GetDifficulty() * 0.1f + 1);
	}

    // Called when a bug has been squashed.
    public void RemoveBug(GameObject bug)
    {
		audio.PlayOneShot (squash);
        bugs.Remove(bug);
        Destroy(bug);
		Instantiate(blood, bug.transform.position, bug.transform.rotation);
		Instantiate(points, bug.transform.position, bug.transform.rotation);
		(diff.scr) += 10;
    }

	// Destroys all bugs in the scene.
	public void RemoveAllBugs()
	{
		for(int i = 0; i < bugs.Count;)
			RemoveBug(bugs[i]);
	}
}
