//#define DEBUG

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugAI : MonoBehaviour 
{
	public GameObject redbug, bluebug, plant;
	private List<GameObject> bugs;
	public float speed;
	private int numBugsAtt;
	private const float CLOSE_TO_PLANT = 1.5f;
	private int numBugs = 0;
    private Timer attackTimer;
    public float initalSpwnRate, minSpwnRate, maxSpwnRate;
	public AudioClip sq;
	private DifficultyController diff;
	private GameObject d;
	// Use this for initialization
	void Start () 
	{
		d = GameObject.FindGameObjectWithTag ("Environment");
		diff = d.GetComponent<DifficultyController> ();
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
			{
                plant.GetComponent<PlantState>().TakeDamage(1);
				// Should also add to numBugsAttacking if first time its close.
				// circlePlant
			} 
			else 
			{
				MoveToPlant(bug);
			}
		}
	}

	// Creates a new bug and adds it to the list.
	private void InstantiateBug()
	{
		int rNum = Random.Range(1,3);
		float xpos;
		float ypos;

		do {
			xpos = Random.Range (-14F, 14F); 
			ypos = Random.Range (-6F, 4F); 
		} while((xpos < 6F && xpos > -6F) && (ypos < 3F));

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
        attackTimer.updateTimer(Time.deltaTime);

        if (attackTimer.hitMaxTime())
        {
			maxSpwnRate = Mathf.Pow(10, -diff.GetDifficulty() * 0.1f);
			minSpwnRate = Mathf.Pow(2, -diff.GetDifficulty() * 0.1f);

            InstantiateBug();
            attackTimer.setMaxSeconds(Random.Range(minSpwnRate, maxSpwnRate));
            attackTimer.resetTimer();
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
		if (bug.transform.position.x < 0) 
			bug.transform.position += Time.deltaTime * Vector3.right * (float)(diff.GetDifficulty() * 0.1f + 1);
		else if (bug.transform.position.x > 0)
			bug.transform.position += Time.deltaTime * Vector3.left * (float)(diff.GetDifficulty() * 0.1f + 1);
		if (bug.transform.position.y > -2.65F)
			bug.transform.position += Time.deltaTime * Vector3.down * (float)(diff.GetDifficulty() * 0.1f + 1);
		else if (bug.transform.position.y < -2.65F)
			bug.transform.position += Time.deltaTime * Vector3.up * (float)(diff.GetDifficulty() * 0.1f + 1);
	}

    // Likely to be called when a bug has been blown
    // away by the wind.
    public void RemoveBug(GameObject bug)
    {
		audio.PlayOneShot (sq);
        bugs.Remove(bug);
        Destroy(bug);
		(diff.scr) += 10;
    }


    // To be called by the difficulty manager.
    public void UpdateSpawnRate()
    {

    }
}
