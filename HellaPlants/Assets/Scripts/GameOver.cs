using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
	private bool canContinue;

	void Start()
	{
		canContinue = false;
		StartCoroutine(DelayContinueButton());
	}

	void Update() 
	{
		if (Input.GetMouseButton (0) && canContinue)
			Application.LoadLevel ("Scene1");
	}

	private IEnumerator DelayContinueButton()
	{
		yield return new WaitForSeconds(2);
		canContinue = true;
	}
}
