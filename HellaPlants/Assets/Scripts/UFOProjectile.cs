using UnityEngine;
using System.Collections;

public class UFOProjectile : MonoBehaviour 
{
	private Vector3 movingDirection;
	private float acceleration;
	private DifficultyController diffContr;
	private PlantState plantState;
	private GameObject explosion;

	// Use this for initialization
	void Start () 
	{
		diffContr = GameObject.Find("Environment").GetComponent<DifficultyController>();
		plantState = GameObject.Find ("Flower").GetComponent<PlantState> ();
		explosion = (GameObject)Resources.Load("Explosion");
		acceleration = (float)(diffContr.GetDifficulty() * 0.4 + 2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Can no longer be seen in the scene.
		if (transform.position.y < -10)
			Destroy (gameObject);

		transform.position += Time.deltaTime * movingDirection * acceleration;
	}

	public void SetDirection(Vector3 direction)
	{
		movingDirection = direction;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Flower") 
		{
			plantState.TakePureDamage(100f);
			Destroy (gameObject);
		}
	}

	// User hit it so destroy it...
	void OnMouseDown()
	{
		Destroy(gameObject);
		Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
	}
}
