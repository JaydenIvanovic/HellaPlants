using UnityEngine;
using System.Collections;

// Allows the player to continue the game
// if they wish at the game over screen by tapping
// to continue.
public class GameOver : MonoBehaviour 
{
	private bool canContinue;

	void Start()
	{
		canContinue = false;
		// We want to delay the reading of user input so 
		// the user doesn't instantly tap out of the game over
		// screen by accident when the game ends abruptly.
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
