﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Follows a singleton pattern. Reads/Writes
// player scores from/to file.
public class ScoreManager : MonoBehaviour 
{
	private static ScoreManager scoreManager;
	private const uint MAX_NUM = 5;
	private string filePath; 
	private float[] scores; // Sorted from highest to lowest.
	
	void Awake () 
	{	
		filePath = Application.persistentDataPath +  "/scores.dat";
		// Debug.Log (filePath);
		scores = new float[MAX_NUM];

		// Singleton style in Unity.
		if (scoreManager == null)
			scoreManager = this;
		else if (scoreManager != this)
			Destroy(gameObject);

		Load();
	}

	public float[] GetScores()
	{
		return scores;
	}

	// Initialize all scores to zero.
	private void InitScores()
	{
		for(int i = 0; i < MAX_NUM; ++i)
			scores[i] = 0;
	}
	
	// The score is higher than the lowest one recorded,
	// or there exists space for additional scores.
	public bool IsHighScore(float score)
	{
		if( scores[MAX_NUM - 1] < score )
			return true;
		return false;
	}

	// Add a new high score to the board.
	// This calls IsHighScore() for insurance that
	// the new score to be added is actually a high score.
	public void AddScore(float score)
	{
		if( IsHighScore(score) )
		{
			// Find the insertion point. As there is only a small
			// number of recorded scores, O(n) search is fine.
			for(int i = 0; i < MAX_NUM; ++i)
			{
				if(scores[i] < score)
				{
					// Move all the scores down by 1 from the end
					// of the array to the score to be replaced.
					for(uint j = MAX_NUM - 1; j > i; --j)
						scores[j] = scores[j - 1];
					// Put the new score into the appropriate position.
					scores[i] = score;
					// Finished necessary logic.
					return; 
				}
			}
		}
	}

	// Save the current scores to file.
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fstream = File.Create(filePath);

		ScoreData data = new ScoreData(scores); // Add to transfer object.

		bf.Serialize(fstream, data);
		fstream.Close();
	}

	// Load the scores from the file.
	public void Load()
	{
		if ( File.Exists(filePath) )
		{
			// Get the data from the file.
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fstream = File.Open(filePath, FileMode.Open);
			ScoreData scoreData = bf.Deserialize(fstream) as ScoreData;
			fstream.Close();

			// Put the data back into the array.
			scores = scoreData.GetScores().Clone() as float[];
		}
		else
			InitScores();
	}
	
	// Extending a MonoBehaviour can cause errors when being read/written
	// to a file. Therefore we have an intermediary 'transfer class'.
	[Serializable]
	private class ScoreData
	{
		private float[] scores;
		
		public ScoreData(float[] scoreData)
		{
			scores = scoreData;
		}

		public float[] GetScores()
		{
			return scores;
		}
	}
}
