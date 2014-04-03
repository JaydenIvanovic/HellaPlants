using UnityEngine;
using System;
using System.Collections;

public class Gestures : MonoBehaviour {
	public enum direction{N,NE,E,SE,S,SW,W,NW};
	private ArrayList rawData;
	private bool recording;
	private Spells spells;



	// Use this for initialization
	void Start () {
		spells = GetComponent<Spells> ();
		recording = false;
		rawData = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		if (recording == false && Input.GetMouseButtonDown (0)) { //TODO: Change this to check for touch, not mouse
			recording = true;
			rawData.Clear();
		} 
		else if (recording == true && Input.GetMouseButtonUp (0)) { //TODO: Change this to check for touch, not mouse
			recording = false;
			decipherGesture ();
		}

		if (recording == true) {
			Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //TODO: Change this to find touch position
			rawData.Add(mousePosition);
		}
	}

	private void decipherGesture()
	{
		//TODO: Currently this method considers the entire gesture as one direction
		//      It needs to quantize the raw data into sections, then call findDirection for each one
		Vector2 first = (Vector2)rawData [0];
		Vector2 last = (Vector2)rawData [rawData.Count - 1];
		direction dir = findDirection (first, last);
		Debug.Log ("Gesture recognised as: " + dir);
		spells.setGesture (dir);
	}

	private direction findDirection(Vector2 start, Vector2 end)
	{
		float xChange = end.x - start.x;
		float yChange = end.y - start.y;

		if (Math.Abs (xChange) > (Math.Abs (yChange) * 1.5)) {
			if (xChange > 0)
				return direction.E;
			else
				return direction.W;
		}
		else if (Math.Abs (yChange) > (Math.Abs (xChange) * 1.5)){
			if (yChange > 0)
				return direction.N;
			else
				return direction.S;
		}
		else{
			if (xChange > 0 && yChange > 0)
				return direction.NE;
			if (xChange > 0 && yChange < 0)
				return direction.SE;
			if (xChange < 0 && yChange > 0)
				return direction.NW;
			else
				return direction.SW;
		}
	}

}
