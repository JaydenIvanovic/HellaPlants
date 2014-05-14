using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Our own list comparator for comparing the sequence
// of gesture directions.
public class ListComparer<T> : IEqualityComparer<List<T>>
{
	// Test whether the two list are equal (have the same elements.)
	public bool Equals(List<T> x, List<T> y)
	{
		return x.SequenceEqual(y);
	}

	// Its recommended to override this to for efficient insertion
	// and lookups.
	public int GetHashCode(List<T> obj)
	{
		int hashcode = 0;

		// For each gesture directional swipe in the list...
		foreach (T t in obj)
		{
			// Add the swipe to the hashcode using XOR.
			hashcode ^= t.GetHashCode();
		}

		// Will be a unique identifier for the sequence of swipes.
		return hashcode;
	}
}
