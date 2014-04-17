﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ListComparer<T> : IEqualityComparer<List<T>>
{
	public bool Equals(List<T> x, List<T> y)
	{
		return x.SequenceEqual(y);
	}

	public int GetHashCode(List<T> obj)
	{
		int hashcode = 0;
		foreach (T t in obj)
		{
			hashcode ^= t.GetHashCode();
		}
		return hashcode;
	}
}
