using UnityEngine;
using System.Collections;

// Script to be attached to the UFO's projectile
// prefab. The projectile handles its own logic.
public class UFOProjectile : MonoBehaviour 
{
	private Vector3 movingDirection;
	private float acceleration;
	private DifficultyController diffContr;
	private PlantState plantState;
	private GameObject explosion;
	public AudioClip shootSnd, explosionSnd;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		diffContr = GameObject.Find("Environment").GetComponent<DifficultyController>();
		plantState = GameObject.Find ("Flower").GetComponent<PlantState> (); 
		explosion = (GameObject)Resources.Load("Explosion");
		audioSource = GameObject.Find("Environment").GetComponent<AudioSource>();
		audioSource.PlayOneShot(shootSnd);
		acceleration = (float)(diffContr.GetDifficulty() * 0.4 + 2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Can no longer be seen in the scene.
		if (transform.position.y < -10)
			Destroy (gameObject);

		transform.position += Time.deltaTime * movingDirection * acceleration;
		transform.LookAt(transform.position + new Vector3(0,0,1), plantState.transform.position  - transform.position);
	}

	// Set the direction which the projectile should move in.
	public void SetDirection(Vector3 direction)
	{
		movingDirection = direction;
	}

	// A collision event occurred so we need to check if 
	// the flower was hit.
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Flower") 
		{
			audioSource.PlayOneShot(explosionSnd);
			plantState.TakePureDamage(100f);
			Destroy (gameObject);
		}
	}

	// User hit it so destroy it...
	void OnMouseDown()
	{
		Destroy(gameObject);
		audioSource.PlayOneShot(explosionSnd);
		Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
	}
}
