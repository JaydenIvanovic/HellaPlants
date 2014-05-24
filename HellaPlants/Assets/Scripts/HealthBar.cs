using UnityEngine;
using System.Collections;

// Handles the displaying of different UI bars
// including sun, water, soil and health.
public class HealthBar : MonoBehaviour
{
    public float barDisplay; //current progress
    public Vector2 pos;
    public Vector2 size;
    public Texture2D emptyTex;
    public Texture2D fullTex;
	public Texture2D fullyEmptyTex;
    public GUIStyle style;
	public float flashThreshold;
	private float flashInterval;
	private float timeToFlash;
	private bool flash;

	void Start()
	{
		flashInterval = 0.35f;
		flash = false;
	}

	void Update()
	{
		if (barDisplay > flashThreshold){
			flash = false;
			timeToFlash = flashInterval;
		}
		else{
			timeToFlash -= Time.deltaTime;
		}

		if (timeToFlash <= 0f){
			timeToFlash = flashInterval;
			if (flash == true)
				flash = false;
			else
				flash = true;
		}
	}

    void OnGUI()
    {
		Vector2 newSize;
		newSize.x = size.x * Screen.width;
		newSize.y = size.y * Screen.height;

        //draw the background:
        GUI.BeginGroup(new Rect(Screen.width * pos.x, Screen.height * pos.y, newSize.x, newSize.y));
		if (flash == false)
			GUI.Box(new Rect(0, 0, newSize.x, newSize.y), emptyTex, style);
		else
			GUI.Box(new Rect(0, 0, newSize.x, newSize.y), fullyEmptyTex, style);

        //draw the filled-in part:
		GUI.BeginGroup(new Rect(0, 0, newSize.x * barDisplay, newSize.y));
		GUI.Box(new Rect(0, 0, newSize.x, newSize.y), fullTex, style);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    /*Pass a number between 0 and 1 */
    public void updateDisplay(float percent)
    {
        barDisplay = percent;
    }
}