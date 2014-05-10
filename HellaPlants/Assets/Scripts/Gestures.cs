//#define GESTURESDEBUG

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Deciphers the gesture sequence from the players input.
public class Gestures : MonoBehaviour {

	private struct swipe{
		public direction dir;
		public int count;
		public int index;
	};

	public enum direction{N,NE,E,SE,S,SW,W,NW,NONE}; //NONE is used if the position didn't change
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
		if (recording == false && Input.GetMouseButtonDown (0)) {
			recording = true;
			rawData.Clear();
		} 
		else if (recording == true && Input.GetMouseButtonUp (0)) {
			recording = false;
			decipherGesture ();
		}

		if (recording == true) {
			Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			rawData.Add(mousePosition);
		}
	}

	// Determines the gesture sequence, calling setGesture
	// of the Spells class to cast the appropriate spell.
	private void decipherGesture()
	{

		/*Quantize the data into compass directions*/
		List<direction> quantizedData = new List<direction> ();
		for (int i = 0; i < rawData.Count; i++) {

			if (i == rawData.Count - 1 || ((Vector2)rawData[i] == (Vector2)rawData[i+1])){
				continue; //Ignore the last element and any repeating elements
			}
			else{
				direction newDir = findDirection ((Vector2)rawData[i],(Vector2)rawData[i+1]);
				if (newDir != direction.NONE)
					quantizedData.Add (newDir);
			}
		}

		int accuracyThreshold = 3; //This number dictates how many wrong results are allowed before a swipe is broken
		int lengthThreshold = 4; //This number dictates how long a swipe must be to register. A value of 1 means every movement will count

		/*Filter the data into a smaller set*/
		List<swipe> swipeList = new List<swipe> ();
		List<direction> filteredData = new List<direction> ();
		for (int i = 0; i < quantizedData.Count; i++) {

			bool cont = false;

			/*Check if an existing swipe can be added to*/
			for (int p = 0; p < swipeList.Count; p++){
				swipe s = swipeList[p];
				if (s.dir == quantizedData[i]){
					s.count++;
					s.index = i;
					swipeList[p] = s;
					cont = true;
					break;
				}
			}

			/*Otherwise, create a new one*/
			if (cont == false){
				swipe newSwipe = new swipe();
				newSwipe.dir = quantizedData[i];
				newSwipe.index = i;
				newSwipe.count = 1;
				swipeList.Add (newSwipe);
			}

			/*Check all existing swipes, and remove outdated ones*/
			for (int p = 0; p < swipeList.Count;){
				if (swipeList[p].index < i - accuracyThreshold){
					if (swipeList[p].count >= lengthThreshold){
						filteredData.Add (swipeList[p].dir);
					}
					swipeList.RemoveAt(p);
				}
				else
					p++;
			}
		}

		/*Add any remaining and valid swipes to the list*/
		for (int p = 0; p < swipeList.Count; p++){
			if (swipeList[p].count >= lengthThreshold){
				filteredData.Add (swipeList[p].dir);
			}
		}

#if GESTURESDEBUG
		string debugString = "";
		foreach (direction d in filteredData) {
			debugString += d;
			debugString += ",";
		}
		Debug.Log (debugString);
#endif

		if (filteredData.Count == 0)
			return;

		spells.setGesture (filteredData);
	}

	// Returns the direction as interpreted by the angle between
	// two vectors.
	private direction findDirection(Vector2 start, Vector2 end)
	{
		float xChange = end.x - start.x;
		float yChange = end.y - start.y;

        //Uncomment this code to make GR 8-directional instead of 4
        /*
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
         * */
			if (xChange > 0 && yChange > 0)
				return direction.NE;
			if (xChange > 0 && yChange < 0)
				return direction.SE;
			if (xChange < 0 && yChange > 0)
				return direction.NW;
			if (xChange < 0 && yChange < 0)
				return direction.SW;
			else
				return direction.NONE;
        /*
		}
         */
	}

}
