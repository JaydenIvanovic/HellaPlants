using UnityEngine;
using System.Collections;

public class LoadLevelonClick : MonoBehaviour {
	public string lv;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel(lv);
		}
	}
}
