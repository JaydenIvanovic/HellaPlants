﻿using UnityEngine;
using System.Collections;

public class UfoAI : MonoBehaviour 
{
    private Timer timer;
    public float minX, maxX;
    private float xLoc;
    private bool isMoving;

	// Use this for initialization
	void Start () 
    {
        xLoc = 0.0f;
        isMoving = false;
        timer = new Timer(Random.Range(1,3));
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (timer.hitMaxTime())
        {
            xLoc = Random.Range(-9, 9);
            isMoving = true;
            timer.resetTimer();
            timer.setMaxSeconds(Random.Range(1, 3));
        }
        if (isMoving)
            moveToRandomLocation();
        else
            timer.updateTimer(Time.deltaTime); 
	}

    // Test whether the ufo should move to a different location.
    private bool isMoveTime()
    {
        return false;
    }

    // Transform the UFO's position along the x axis to
    // some random position.
    private void moveToRandomLocation()
    {
        float x;
        Vector3 newLocation;

        if (xLoc < transform.position.x)
            x = -2.2f;
        else
            x = 2.2f;

        newLocation = new Vector3(xLoc, transform.position.y, transform.position.z);

        transform.position += new Vector3(Time.deltaTime * x, 0, 0);
#if UFODEBUG
        Debug.Log(transform.position.ToString() + " " + newLocation.ToString());
        Debug.Log(Vector3.Distance(newLocation, transform.position));
#endif
        if (Vector3.Distance(newLocation, transform.position) <= 0.1)
            isMoving = false;
    }
}