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
    public int initalSpwnRate, minSpwnRate, maxSpwnRate;

	// Use this for initialization
	void Start () 
	{
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
                plant.GetComponent<PlantState>().TakeDamage();
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

		if (rNum == 1)
			bugs.Add((GameObject)Instantiate (redbug));
		else if (rNum == 2)
			bugs.Add((GameObject)Instantiate (bluebug));

		numBugs++;
	}

    // Makes a check to see if a bug should be created.
    // If so, the bug is created.
	private void CreateBugs()
	{
        attackTimer.updateTimer(Time.deltaTime);

        if (attackTimer.hitMaxTime())
        {
            InstantiateBug();
            attackTimer.setMaxSeconds(Random.Range(minSpwnRate, maxSpwnRate));
            attackTimer.resetTimer();
        }
	}

    // Checks whether the bug is close to the plant. 
	private bool CloseToPlant(GameObject plant, GameObject bug)
	{
		if (Vector3.Distance (plant.transform.position, bug.transform.position) <= CLOSE_TO_PLANT)
			return true; 
		return false;
	}

    // Moves the bug towards the plant.
	private void MoveToPlant(GameObject bug)
	{
		if (bug.transform.position.x < 0) 
			bug.transform.position += Time.deltaTime * Vector3.right;
		else if (bug.transform.position.x > 0)
			bug.transform.position += Time.deltaTime * Vector3.left;
	}

    // Likely to be called when a bug has been blown
    // away by the wind.
    public void RemoveBug(GameObject bug)
    {
        bugs.Remove(bug);
        Destroy(bug);
    }


    // To be called by the difficulty manager.
    public void UpdateDifficulty()
    {

    }
}
