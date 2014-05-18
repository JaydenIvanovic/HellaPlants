using UnityEngine;
using System.Collections;

// Handles the UFO's behaviour. The UFO simply
// enters the scene, moves to random position, fires
// a projectile and then exits the scene.
public class UfoAI : MonoBehaviour 
{
	private GameObject environment, plant;
	private UfoSpawner ufoSpawner;
	private DifficultyController diffContr;
	public GameObject thunder;
	private float speed;
	private Vector3 destination;
	private bool hasDestination;
	private int numBullets;

	// Use this for initialization
	void Start () 
    {
		speed = 1.8f;
		hasDestination = false;
		destination = transform.position;
		environment = GameObject.FindGameObjectWithTag("Environment");
		plant = GameObject.Find("Flower");
		ufoSpawner = environment.GetComponent<UfoSpawner>();
		diffContr = environment.GetComponent<DifficultyController> ();

		numBullets = 1;
		if (diffContr.GetDifficulty () > 4)
			numBullets = Random.Range (1,diffContr.GetDifficulty() - 2);
	}
	
	// Update is called once per frame
	void Update () 
    {
		//Get a new destination, if it reached the old one
		if (hasDestination == false){
        	if (numBullets > 0)
	            moveToRandomLocation();
        	else
				moveOffScreen();
			hasDestination = true;
		}

		//Get current distance to destination
		float dist = Vector3.Distance (transform.position, destination);

		//Move towards current destination
		transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed * (float)(diffContr.GetDifficulty() * 0.11 + 1));

		//Get the new distance
		float newDist = Vector3.Distance (transform.position, destination);

		//Check if it is past the destination
		if (newDist <= 0.1 || newDist > dist)
		{
			if (numBullets > 0)
			{
				numBullets --;
				shootBolt();
			}
			else
			{
				Destroy(gameObject);
				ufoSpawner.continueTimer();
			}
			hasDestination = false;
		}
	}
	
    // Set the destination of the UFO to somewhere on-screen so it can fire a bullet
    private void moveToRandomLocation()
    {
		float xLoc;
		do{
			xLoc = Random.Range(-11, 11);
		} while (Mathf.Abs(xLoc - destination.x) < 3); //Make sure the next bullet fires from at least a certain distance away
        destination = new Vector3(xLoc, transform.position.y, transform.position.z);
    }

	// Set the destination off-screen so the UFO can be destroyed
	private void moveOffScreen()
	{
		destination = ufoSpawner.getRandomSpawnPoint();
	}
	
	// Shoot a bullet in the direction of the plant.
	private void shootBolt()
	{
		Vector3 dirToPlant = Vector3.Normalize(plant.transform.position - transform.position);
		GameObject newBolt = (GameObject)Instantiate (thunder, transform.position, transform.rotation);
		newBolt.GetComponent<UFOProjectile>().SetDirection(dirToPlant);
	}	
}
