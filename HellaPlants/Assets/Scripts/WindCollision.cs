//#define WINDDEBUG

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
#if WINDDEBUG
        Debug.Log("Bug got hit by wind");
#endif
        bugAI.RemoveBug(gameObject);
    }
}
