using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	private DifficultyController diff;
	private GameObject environment;

	public GameObject tutorialPrefab1;
	public GameObject tutorialPrefab2;
	public GameObject tutorialPrefab3;
	public GameObject tutorialPrefab4;
	private GameObject wizard;

	int currentDif;
	bool wizardExists;

	// Use this for initialization
	void Start () {
		environment = GameObject.FindGameObjectWithTag ("Environment");
		diff = environment.GetComponent<DifficultyController> ();

		currentDif = -1;
	}
	
	// Update is called once per frame
	void Update () {

		//Check if difficulty has changed
		if (currentDif != diff.GetDifficulty ()){
			currentDif ++;

			wizardExists = true;
			Invoke("DestroyWizard", 4f);

			if (currentDif == 0){
				wizard = Instantiate(tutorialPrefab1) as GameObject;
			}
			else if (currentDif == 1){
				wizard = Instantiate(tutorialPrefab2) as GameObject;
			}
			else if (currentDif == 2){
				wizard = Instantiate(tutorialPrefab3) as GameObject;
			}
			else if (currentDif == 3){
				wizard = Instantiate(tutorialPrefab4) as GameObject;
			}
			else
				wizardExists = false;
		}
	}

	public void DestroyWizard()
	{
		if (wizardExists)
			Destroy(wizard);
	}
	
}
