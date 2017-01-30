/*
 * Author: Isaiah Mann 
 * Description: Generic superclass for sorting algorithms
 */

using System;

public class Sort<T>  where T : IComparable {

	public Sort() {

	}
	
	// To be overridden in subclass
	public T[] run (T[] source) {
		return source;
	}
	
	protected void swap (T[] source, int index1, int index2) {
		T temp = source[index1];
		source[index1] = source[index2];
		source[index2] = temp;
	}

}
