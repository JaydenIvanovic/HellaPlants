using UnityEngine;
using System.Collections;

// Implementation of a timer to have timed events.
// In the future we may replace the code in the project 
// using this with coroutines. C# is pretty fancy!
public class Timer
{
    private float maxSeconds{get;set;}
    private float current;
    private bool hitMax;

	public Timer(float max)
    {
        maxSeconds = max;
        current = 0.0f;
        hitMax = false;
    }

	// Increments the timer with the time passed
	// since last update.
    public void updateTimer(float deltaTime)
    {
        current += deltaTime;
 
        if (current >= maxSeconds)
            hitMax = true;
    }

	// Check to see if the timer has hit the max(goal) time.
    public bool hitMaxTime()
    {
        return hitMax;
    }

	// Rest the timer to 0.
    public void resetTimer()
    {
        current = 0.0f;
        hitMax = false;
    }

	// Set a new goal time for the timer.
    public void setMaxSeconds(float max)
    {
        maxSeconds = max;
    }
}
