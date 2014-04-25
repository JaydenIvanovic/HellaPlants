﻿using UnityEngine;
using System.Collections;

public class UfoAI : MonoBehaviour 
{
    private Timer timer;
    public float minX, maxX;
    private float xLoc;
    private bool isMoving;
	private GameObject environment, plant;
	private UfoSpawner ufoSpawner;
	private DifficultyController diffContr;
	public GameObject thunder;
	private float speed;
	private Vector3 offscreen;

	// Use this for initialization
	void Start () 
    {
		speed = 2.2f;
		xLoc = Random.Range(-9, 9);
        isMoving = true;
		environment = GameObject.FindGameObjectWithTag("Environment");
		plant = GameObject.Find("Flower");
		ufoSpawner = environment.GetComponent<UfoSpawner>();
		diffContr = environment.GetComponent<DifficultyController> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isMoving)
            moveToRandomLocation();
        else
			moveOffScreen();
	}
	
    // Transform the UFO's position along the x axis to
    // some random position.
    private void moveToRandomLocation()
    {
        float x;
        Vector3 newLocation;

        if (xLoc < transform.position.x)
            x = -speed;
        else
            x = speed;

        newLocation = new Vector3(xLoc, transform.position.y, transform.position.z);

        transform.position += new Vector3(Time.deltaTime * x * (float)(diffContr.GetDifficulty() * 0.1 + 1), 0, 0);
#if UFODEBUG
        Debug.Log(transform.position.ToString() + " " + newLocation.ToString());
        Debug.Log(Vector3.Distance(newLocation, transform.position));
#endif
        if (Vector3.Distance(newLocation, transform.position) <= 0.1)
		{
            isMoving = false;
			shootBolt();
			offscreen = ufoSpawner.getRandomSpawnPoint();
		}
    }

	// Move the ufo offscreen as its fired the bullet.
	private void moveOffScreen()
	{
		transform.position = Vector3.MoveTowards(transform.position, offscreen, Time.deltaTime * speed);
		if(Vector3.Distance(offscreen, transform.position) <= 0.1)
		{
			Destroy(gameObject);
			ufoSpawner.continueTimer();
		}
	}
	
	// Shoot a bullet in the direction of the plant.
	private void shootBolt()
	{
		Vector3 dirToPlant = Vector3.Normalize(plant.transform.position - transform.position);
		GameObject newBolt = (GameObject)Instantiate (thunder, transform.position, transform.rotation);
		newBolt.GetComponent<UFOProjectile>().SetDirection(dirToPlant);
	}	
}
