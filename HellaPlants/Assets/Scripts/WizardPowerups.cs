using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardPowerups : MonoBehaviour 
{
	private bool wizardExists;
	private float chance;
	public enum Powerups {Regeneration, SlowTime, KillBugs, Shield, None};
	private Powerups currentPowerup;
	public GameObject wizardPrefab;
	private GameObject wizard;

	// Use this for initialization
	void Start () 
	{
		wizardExists = false;
		currentPowerup = Powerups.None;
		chance = 0.0f;
		InvokeRepeating ("RandomPowerup", 0f, 5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetRandGesture (5);
	}

	// The wizard exits.
	public bool Exists()
	{
		return wizardExists;
	}

	// The curret powerup available.
	public Powerups Powerup()
	{
		return currentPowerup;
	}

	private void ShowWizard()
	{
		wizardExists = true;
		wizard = Instantiate (wizardPrefab) as GameObject;
	}

	private void DestroyWizard()
	{
		wizardExists = false;
		Destroy (wizard);
	}

	// Set current powerup to a random spell
	private void SetRandomSpell()
	{
		int r = Random.Range (0, 4);
		switch(r)
		{
			case 0:
				currentPowerup = Powerups.Regeneration;
				break;
			case 1:
				currentPowerup = Powerups.SlowTime;
				break;
			case 2:
				currentPowerup = Powerups.KillBugs;
				break;
			case 3: 
				currentPowerup = Powerups.Shield;
				break;
		}
	}

	// Get new random powerup.
	private void RandomPowerup()
	{
		if (wizardExists)
			return;

		// success
		if( chance > Random.Range(0, 1) )
		{
			// Choose a random spell.
			SetRandomSpell();
			ShowWizard();
			chance = 0.0f;
			Invoke("DestroyWizard", 6f);
		}
		else
			//failure
			chance += 0.05f;
	}

	public Gestures.direction RandomDirection(Gestures.direction dir)
	{
		int r;

		switch(dir)
		{
			case Gestures.direction.NE:
				r = Random.Range (0, 2);
				switch(r)
				{
					case 0:
						return Gestures.direction.SE;
					case 1:
						return Gestures.direction.NW;
				}
				break;
			case Gestures.direction.NW:
				r = Random.Range (0, 2);
				switch(r)
				{
					case 0:
						return Gestures.direction.NE;
					case 1:
						return Gestures.direction.SW;
				}
				break;
			case Gestures.direction.SE:
				r = Random.Range (0, 2);
				switch(r)
				{
					case 0:
						return Gestures.direction.NE;
					case 1:
						return Gestures.direction.SW;
				}
				break;
			case Gestures.direction.SW:
				r = Random.Range (0, 2);
				switch(r)
				{
					case 0:
						return Gestures.direction.NW;
					case 1:
						return Gestures.direction.SE;
				}
				break;
			case Gestures.direction.NONE:
				r = Random.Range (0, 4);
				switch(r)
				{
					case 0:
						return Gestures.direction.NE;
					case 1:
						return Gestures.direction.NW;
					case 2:
						return Gestures.direction.SE;
					case 3: 
						return Gestures.direction.SW;
				}
				break;
		}

		return Gestures.direction.NONE;
	}

	// CHECK THAT IT DOESNT GENERATE GESTURES ALREADY IN USE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	public List<Gestures.direction> GetRandGesture (int length)
	{
		List<Gestures.direction> gesture = new List<Gestures.direction>();

		gesture.Add (RandomDirection (Gestures.direction.NONE));

		for(int i = 1; i < length; i++)
			gesture.Add(RandomDirection(gesture[i - 1]));

		string directions = "";
		for(int i = 0; i < gesture.Count; i++)
		{
			if(gesture[i] == Gestures.direction.NE)
				directions += "NE ";
			if(gesture[i] == Gestures.direction.NW)
				directions += "NW ";
			if(gesture[i] == Gestures.direction.SE)
				directions += "SE ";
			if(gesture[i] == Gestures.direction.SW)
				directions += "SW ";
		}
		Debug.Log (directions);
		return gesture;
	}
}
