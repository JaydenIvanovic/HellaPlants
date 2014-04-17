using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureMap 
{
	public enum Spell{Sun, Rain, Fert, Wind, Shield, None};
	private Dictionary<List<Gestures.direction>, Spell> gestureMap;

	// Use this for initialization
	public GestureMap () 
	{
		gestureMap = new Dictionary<List<Gestures.direction>, Spell> (new ListComparer<Gestures.direction>());
		SetSunGestures();
		SetRainGestures();
		SetFertGestures();
		SetWindGestures();
	}

	// All the recognized sun gesture sequences.
	void SetSunGestures()
	{
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.N, 
		                                              Gestures.direction.E, 
		                                              Gestures.direction.S, 
			                                          Gestures.direction.W}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.E, 
		                                              Gestures.direction.S, 
		                                              Gestures.direction.W, 
			                                          Gestures.direction.N}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.S, 
		                                              Gestures.direction.W, 
		                                              Gestures.direction.N, 
			                                          Gestures.direction.E}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.W, 
		                                              Gestures.direction.N, 
		                                              Gestures.direction.E, 
			                                          Gestures.direction.S}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.N, 
		                                              Gestures.direction.W, 
		                                              Gestures.direction.S, 
			                                          Gestures.direction.E}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.E, 
		                                              Gestures.direction.N, 
		                                              Gestures.direction.W, 
			                                          Gestures.direction.S}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.S, 
		                                              Gestures.direction.E, 
		                                              Gestures.direction.N, 
			                                          Gestures.direction.W}, 
		               Spell.Sun);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.W, 
		                                              Gestures.direction.S, 
		                                              Gestures.direction.E, 
			                                          Gestures.direction.N}, 
		               Spell.Sun);
	}

	// All the recognized rain gesture sequences.
	void SetRainGestures()
	{
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.E, 
		                                              Gestures.direction.SW}, 
		               Spell.Rain);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.NE, 
		                                              Gestures.direction.W, 
		                                              Gestures.direction.NE}, 
		               Spell.Rain);
	}

	// All the recognized fertilizer gesture sequences.
	void SetFertGestures()
	{
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.SE}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.NW, 
		                                              Gestures.direction.NE}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.S,
													  Gestures.direction.SE,
													  Gestures.direction.S}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.SE,
													  Gestures.direction.S}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.S,
													  Gestures.direction.SE}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.E,
													  Gestures.direction.SE}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.S,
													  Gestures.direction.SE,
													  Gestures.direction.E}, 
		               Spell.Fert);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SW, 
		                                              Gestures.direction.SE,
													  Gestures.direction.E}, 
		               Spell.Fert);
	}

	// All the recognized wind gesture sequences.
	void SetWindGestures()
	{
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SE, 
		                                              Gestures.direction.SW}, 
		               Spell.Wind);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SE,
													  Gestures.direction.W,
		                                              Gestures.direction.SW}, 
		               Spell.Wind);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SE,
													  Gestures.direction.E,
		                                              Gestures.direction.SW},
		               Spell.Wind);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SE,
													  Gestures.direction.S,
		                                              Gestures.direction.SW},
		               Spell.Wind);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SE,
													  Gestures.direction.S,
		                                              Gestures.direction.W}, 
		               Spell.Wind);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.SE,
													  Gestures.direction.SW,
		                                              Gestures.direction.W}, 
		               Spell.Wind);
		gestureMap.Add(new List<Gestures.direction>(){Gestures.direction.NE, 
		                                              Gestures.direction.NW}, 
		               Spell.Wind);
	}

	// Get the spell corresponding to this sequence. Returns a none spell
	// if no spell is recognized given the sequence.
	public Spell GetSpell(List<Gestures.direction> sequence)
	{
		Spell spell = Spell.None;
		if (gestureMap.TryGetValue (sequence, out spell))
			return spell;
		else
			return Spell.None;
	}
}
