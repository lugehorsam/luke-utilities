using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Algorithms {
	
	public static T[] Shuffle<T>(T[] collection) {
		int index = collection.Length - 1;
		T[] newCollection = collection;

		while (index > 0) {
			int newIndex = (int) Mathf.Floor (Random.value * collection.Length);
			T unplacedValue = newCollection [newIndex];
			newCollection [newIndex] = newCollection [index];
			newCollection [index] = unplacedValue;
			index--;
		}	

		return newCollection;
	}

}
