using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public float barDisplay; //current progress
    public Vector2 pos;
    public Vector2 size;
    public Texture2D emptyTex;
    public Texture2D fullTex;
    public GUIStyle style;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex, style);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, style);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {

    }

    /*Pass a number between 0 and 1 */
    public void updateDisplay(float percent)
    {
        barDisplay = percent;
    }
}