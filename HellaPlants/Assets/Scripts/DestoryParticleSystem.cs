using UnityEngine;
using System.Collections;

public class DestoryParticleSystem : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().duration);
	}
}
