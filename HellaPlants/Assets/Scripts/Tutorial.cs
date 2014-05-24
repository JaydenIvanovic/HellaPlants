using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	private DifficultyController diff;
	private GameObject environment;
	private const string PLAYED_TUTE = "PlayedTutorial";
	public GameObject tutorialPrefab0;
	public GameObject tutorialPrefab1;
	public GameObject tutorialPrefab2;
	public GameObject tutorialPrefab3;
	public GameObject tutorialPrefab4;
	public GameObject tapPrefab;
	private GameObject wizard, tap;
	private int currentDif;
	private bool wizardExists, canDestroy;
	private float pauseScale = 0f, unPauseTime = 0f;
	private bool firstScreen;

	// Use this for initialization
	void Start () {
		firstScreen = false;
		PlayerPrefs.DeleteAll ();

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
			unPauseTime = Time.realtimeSinceStartup + 1.5f;

			if (currentDif == 0){
				if (firstScreen == false)
					wizard = Instantiate(tutorialPrefab0) as GameObject;
				else
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
			}
			else
				wizardExists = false;
		}
	
		// following on from the above workaround.
		if(wizardExists) {
			if(Time.realtimeSinceStartup > unPauseTime) {
				canDestroy = true;
				if(tap == null)
					tap = Instantiate(tapPrefab) as GameObject;
			}
		}

		// If user presses on the screen assume they want to proceed.
		if ( canDestroy && Input.GetMouseButton(0) )
		{
			DestroyWizard();
			Destroy(tap);

			// Played the tutorial to its completion. Remember this.
			if(currentDif == 3)
				PlayerPrefs.SetInt(PLAYED_TUTE, 1);
		}

	}

	public void DestroyWizard()
	{
		if (firstScreen == false) {
			currentDif--;
			firstScreen = true;
		}
		if (wizardExists)
			Destroy(wizard);

		Time.timeScale = 1f;
		canDestroy = false;
		wizardExists = false;
	}

}
