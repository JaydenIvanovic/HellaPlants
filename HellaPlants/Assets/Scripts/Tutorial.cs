using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	private DifficultyController diff;
	private GameObject environment;
	private const string PLAYED_TUTE = "PlayedTutorial";
	public GameObject tutorialPrefab1;
	public GameObject tutorialPrefab2;
	public GameObject tutorialPrefab3;
	public GameObject tutorialPrefab4;
	private GameObject wizard;
	private int currentDif;
	private bool wizardExists, canDestroy;
	private float pauseScale = 0f, unPauseTime = 0f;

	// Use this for initialization
	void Start () {
		// PlayerPrefs.DeleteAll() uncomment this if you want to reset playerprefs.

		environment = GameObject.FindGameObjectWithTag ("Environment");
		diff = environment.GetComponent<DifficultyController> ();

		wizardExists = false;
		canDestroy = false;
		currentDif = -1;
	}
	
	// Update is called once per frame
	void Update () {

		//Check if player has already played the tutorial.
		// If so we don't want to show it again.
		if(PlayerPrefs.GetInt(PLAYED_TUTE) == 1)
			return;

		//Check if difficulty has changed.
		if (currentDif != diff.GetDifficulty ()){
			currentDif ++;

			wizardExists = true;
			// Work around as we can't use invoke with a timescale of 0f.
			unPauseTime = Time.realtimeSinceStartup + 3f;

			if (currentDif == 0){
				wizard = Instantiate(tutorialPrefab1) as GameObject;
				Time.timeScale = pauseScale;
			}
			else if (currentDif == 1){
				wizard = Instantiate(tutorialPrefab2) as GameObject;
				Time.timeScale = pauseScale;
			}
			else if (currentDif == 2){
				wizard = Instantiate(tutorialPrefab3) as GameObject;
				Time.timeScale = pauseScale;
			}
			else if (currentDif == 3){
				wizard = Instantiate(tutorialPrefab4) as GameObject;
				Time.timeScale = pauseScale;
				// Played the tutorial to its completion. Remeber this.
				PlayerPrefs.SetInt(PLAYED_TUTE, 1);
			}
			else
				wizardExists = false;
		}
	
		// following on from the above workaround.
		if(wizardExists) {
			if(Time.realtimeSinceStartup > unPauseTime)
				canDestroy = true;
		}

		// If user presses on the screen assume they want to proceed.
		if ( canDestroy && Input.GetMouseButton(0) ) 
			DestroyWizard();

	}

	public void DestroyWizard()
	{
		if (wizardExists)
			Destroy(wizard);

		Time.timeScale = 1f;
		canDestroy = false;
		wizardExists = false;
	}

}
