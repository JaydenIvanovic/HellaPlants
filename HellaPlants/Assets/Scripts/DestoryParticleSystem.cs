using UnityEngine;
using System.Collections;

// Attach this to a game object with a particle system
// in order to destroy it once the particle system has completed
// its effect.
public class DestoryParticleSystem : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().duration);
	}
}
