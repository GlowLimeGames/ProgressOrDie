/*
 * Author: Isaiah Mann 
 * Description: Generic implementation of merge sort
 */

using System;
using System.Collections.Generic;

public class MergeSort<T> : Sort<T> where T : IComparable {

	public T[] run (T[] source) {
		return sortSubArray(source, 0, source.Length - 1);
	}
	
	T[] sortSubArray (T[] source, int start, int end) {
		// Base case: 1 element
		if (start - end == 0) {
			return source;
		}
		// Second base case: 2 elements
		else if (end - start == 1) {
			if (source[start].CompareTo(source[end]) > 0) {
				swap(source, start, end);
			}
			return source;
		}
		// Recursive Step: Split the array
		else {
			int mid = (start + end) / 2;
			sortSubArray(source, start, mid - 1);
			sortSubArray(source, mid, end);
			// Merge the two sorted halves of the array:
			return mergeSubArrays(source, start, mid - 1, mid, end);
		}
	}
	
	// We expect end1 and start2 to be adjacent based on the params of the algorithm
	T[] mergeSubArrays (T[] source, int start1, int end1, int start2, int end2) {
		// Can't declare an array because it can't store generics
		List<T> buffer = new List<T>(end2 - start1);
		int pointer1 = start1;
		int pointer2 = start2;
		int head = Math.Min(start1, start2);
		int tail = Math.Max(end1, end2);
		int index = head;
		while (index <= tail) {
			bool p1AtEnd = pointer1 > end1;
			bool p2AtEnd = pointer2 > end2;
			if (p1AtEnd) {
				buffer.Add(source[pointer2++]);
			} else if (p2AtEnd) {
				buffer.Add(source[pointer1++]);
			} else {
				if (source[pointer2].CompareTo(source[pointer1]) < 1) {
					buffer.Add(source[pointer2++]);
				} else {
					buffer.Add(source[pointer1++]);
				}
			}
			// Always step forward 1
			index++;
		}
		index = head;
		while (buffer.Count > 0) {
			source[index++] = buffer[0];
			buffer.RemoveAt(0);
		}
		return source;
	}
}
