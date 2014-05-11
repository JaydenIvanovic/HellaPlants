using UnityEngine;
using System.Collections;

// This has to be attached to the main camera to actually work...
// YOU CANT CHANGE LINE WIDTH WITH GL LINE WIDTH!
public class DrawLine : MonoBehaviour 
{
	public Material material;
	public float xOrigin, yOrigin; // Where you want the shape to begin being drawn from.
	private WizardPowerups wizard;

	// Use this for initialization
	void Start () 
	{
		wizard = GameObject.Find("Environment").GetComponent<WizardPowerups>();
	}
	
	// Update is called once per frame
	void OnPostRender () 
	{
		if(wizard.Exists())
		{
			GL.PushMatrix();
				material.SetPass(0);
				GL.Color(Color.black);
				NorthEastLine(xOrigin, yOrigin);
			GL.PopMatrix();
		}
	}
	
	private void NorthEastLine(float x, float y)
	{
		GL.Begin(GL.LINES);
			GL.Vertex3(x, y, -5f);
			GL.Vertex3(x + 3, yOrigin + 3, -5f);
		GL.End();
	}

	private void NorthWestLine()
	{

	}

	private void SouthEastLine()
	{

	}

	private void SouthWestLine()
	{

	}
}
