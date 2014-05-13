﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardPowerups : MonoBehaviour
{
    private bool wizardExists;
    private float chance;
    private GestureMap.Spell currentPowerup;
    public GameObject wizardPrefab;
    private GameObject wizard;
    public GameObject player;
    private Spells spells;
    public Texture NEarrowTexture;
    public Texture NWarrowTexture;
    public Texture SEarrowTexture;
    public Texture SWarrowTexture;
    List<Gestures.direction> currentGesture;

    // Use this for initialization
    void Start()
    {
        spells = player.GetComponent<Spells>();
        currentGesture = null;
        wizardExists = false;
        currentPowerup = GestureMap.Spell.None;
        chance = 0.0f;
        InvokeRepeating("RandomPowerup", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        if (Exists())
            drawGesture();
    }

    public bool Exists()
    {
        return wizardExists;
    }

    private void ShowWizard()
    {
        if (currentGesture != null)
        {
            Invoke("DestroyWizard", 15f);
            spells.getGestureMap().SetPowerupGesture(currentPowerup, currentGesture);
            wizardExists = true;
            wizard = Instantiate(wizardPrefab) as GameObject;
        }
    }

    public void DestroyWizard()
    {
        if (wizardExists == true)
        {
            spells.getGestureMap().UnsetPowerupGesture(currentGesture);
            currentGesture = null;
            wizardExists = false;
            Destroy(wizard);
        }
    }

    private void drawGesture()
    {
        for (int i = 0; i < currentGesture.Count; i++)
        {
            Texture tex;
            if (currentGesture[i] == Gestures.direction.NE)
                tex = NEarrowTexture;
            else if (currentGesture[i] == Gestures.direction.NW)
                tex = NWarrowTexture;
            else if (currentGesture[i] == Gestures.direction.SE)
                tex = SEarrowTexture;
            else if (currentGesture[i] == Gestures.direction.SW)
                tex = SWarrowTexture;
            else
                return;

            GUI.DrawTexture(new Rect(200 + (i*80), 200, 40, 40), tex, ScaleMode.ScaleAndCrop, true, 0);
        }
    }

    // Set current powerup to a random spell
    private void SetRandomSpell()
    {
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                currentPowerup = GestureMap.Spell.Regeneration;
                currentGesture = GetRandGesture(5);
                break;
            case 1:
                currentPowerup = GestureMap.Spell.SlowTime;
                currentGesture = GetRandGesture(4);
                break;
            case 2:
                currentPowerup = GestureMap.Spell.KillBugs;
                currentGesture = GetRandGesture(3);
                break;
            case 3:
                currentPowerup = GestureMap.Spell.Shield;
                currentGesture = GetRandGesture(2);
                break;
        }
    }

    // Get new random powerup.
    private void RandomPowerup()
    {
        if (wizardExists)
            return;

        // success
        if (chance > Random.Range(0, 1))
        {
            // Choose a random spell.
            SetRandomSpell();
            ShowWizard();
            chance = 0.0f;
        }
        //failure
        else
            chance += 0.05f;
    }

    public Gestures.direction RandomDirection(Gestures.direction dir)
    {
        int r;

        switch (dir)
        {
            case Gestures.direction.NE:
                r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        return Gestures.direction.SE;
                    case 1:
                        return Gestures.direction.NW;
                }
                break;
            case Gestures.direction.NW:
                r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        return Gestures.direction.NE;
                    case 1:
                        return Gestures.direction.SW;
                }
                break;
            case Gestures.direction.SE:
                r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        return Gestures.direction.NE;
                    case 1:
                        return Gestures.direction.SW;
                }
                break;
            case Gestures.direction.SW:
                r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        return Gestures.direction.NW;
                    case 1:
                        return Gestures.direction.SE;
                }
                break;
            case Gestures.direction.NONE:
                r = Random.Range(0, 4);
                switch (r)
                {
                    case 0:
                        return Gestures.direction.NE;
                    case 1:
                        return Gestures.direction.NW;
                    case 2:
                        return Gestures.direction.SE;
                    case 3:
                        return Gestures.direction.SW;
                }
                break;
        }

        return Gestures.direction.NONE;
    }

    public List<Gestures.direction> GetRandGesture(int length)
    {
        List<Gestures.direction> gesture = new List<Gestures.direction>();
        do
        {
            gesture.Clear();
            gesture.Add(RandomDirection(Gestures.direction.NONE));

            for (int i = 1; i < length; i++)
                gesture.Add(RandomDirection(gesture[i - 1]));

            string directions = "";
            if (currentPowerup == GestureMap.Spell.Regeneration)
                directions += "Regeneration: ";
            if (currentPowerup == GestureMap.Spell.KillBugs)
                directions += "Kill Bugs: ";
            if (currentPowerup == GestureMap.Spell.Shield)
                directions += "Shield: ";
            if (currentPowerup == GestureMap.Spell.SlowTime)
                directions += "Slow Time: ";

            for (int i = 0; i < gesture.Count; i++)
            {
                if (gesture[i] == Gestures.direction.NE)
                    directions += "NE ";
                if (gesture[i] == Gestures.direction.NW)
                    directions += "NW ";
                if (gesture[i] == Gestures.direction.SE)
                    directions += "SE ";
                if (gesture[i] == Gestures.direction.SW)
                    directions += "SW ";
            }
            Debug.Log(directions);
        } while (spells.getGestureMap().CheckGestureExists(gesture) == true); //Check the gesture isn't already in use

        return gesture;
    }
}