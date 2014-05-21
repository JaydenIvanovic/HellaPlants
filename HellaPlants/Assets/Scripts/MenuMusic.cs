using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour 
{
	public AudioClip menuMusic;
	public string endScene;
	private static MenuMusic singleton;

	// Use this for initialization
	void Start () 
	{
		if(singleton == null)
		{
			singleton = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.clip = menuMusic;
		audio.Play();

		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevelName == endScene)
			Destroy(gameObject);
	}
}
