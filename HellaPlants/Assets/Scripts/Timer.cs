using UnityEngine;
using System.Collections;

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

    public void updateTimer(float deltaTime)
    {
        current += deltaTime;
 
        if (current >= maxSeconds)
            hitMax = true;
    }

    public bool hitMaxTime()
    {
        return hitMax;
    }

    public void resetTimer()
    {
        current = 0.0f;
        hitMax = false;
    }

    public void setMaxSeconds(float max)
    {
        maxSeconds = max;
    }
}
