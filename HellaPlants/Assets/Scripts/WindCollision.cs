using UnityEngine;
using System.Collections;

public class WindCollision : MonoBehaviour
{
    private BugAI bugAI;

    void Start()
    {
        bugAI = GameObject.Find("Environment").GetComponent<BugAI>();
    }

    void OnParticleCollision(GameObject particleSys)
    {
        Debug.Log("Bug got hit by wind");
        bugAI.RemoveBug(gameObject);
    }
}
