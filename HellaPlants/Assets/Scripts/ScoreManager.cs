using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Follows a singleton pattern. Reads/Writes
// player scores from/to file.
public class ScoreManager : MonoBehaviour 
{
	private static ScoreManager scoreManager;
	private const uint MAX_NUM = 10;
	private string filePath = Application.persistentDataPath +  "/scores.dat";
	private float[] scores;

	// Use this for initialization
	void Start () 
	{	
		scores = new float[MAX_NUM];

		// Singleton style in Unity.
		if (scoreManager == null)
			scoreManager = this;
		else if (scoreManager != this)
			Destroy(gameObject);
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
