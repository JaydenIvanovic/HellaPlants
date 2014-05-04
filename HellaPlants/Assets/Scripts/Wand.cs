using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Not used in the current game. Was way to messy...
public class Wand : MonoBehaviour 
{
    public GameObject fireball;
    private List<GameObject> fireballs;
    public float spellSpeed;

	// Use this for initialization
	void Start () 
    {
        fireballs = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    foreach(GameObject fire in fireballs)
            fire.transform.position += Vector3.up * Time.deltaTime * spellSpeed;

        for (int i = 0; i < fireballs.Count; )
        {
            if (fireballs[0].transform.position.y > 20)
            {
                Destroy(fireballs[0]);
                fireballs.Remove(fireballs[0]);
            }
            else
                i++;
        }
	}
 
    // Called when attached to a wand gameobject...
    void OnMouseDrag()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(Mathf.Clamp(mouse.x, -9f, 9f), gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Called when attached to a wand gameobject...
    void OnMouseDown()
    {
        fireballs.Add( (GameObject) Instantiate(fireball, transform.position + new Vector3(0, 2, 0), Quaternion.identity) );
    }

    // For the spells script to call.
    public void castFireball()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.y = -5.5f;
        fireballs.Add( (GameObject) Instantiate(fireball, mouse, Quaternion.identity) );
    }
}
