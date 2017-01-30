/*
 * Author: Isaiah Mann 
 * Description: Generic implementation of insertion sort
 */

using System;

public class InsertionSort<T> : Sort<T> where T : IComparable {

	public T[] run (T[] source) {
		for (int i = 1; i < source.Length; i++) {
			for (int j = i; j > 0; j--) {
				if (source[j].CompareTo(source[j-1]) < 0) {
					swap(source, j, j-1);
				} else {
					break;
				}
			}
		}
		return source;
	}

}
