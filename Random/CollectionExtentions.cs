using System;
using System.Collections.Generic;

namespace DotOriko.Random {
	public static class CollectionExtentions {

		static IEnumerable<T> Where<T>(this IEnumerable<T> data, Func<T, bool> predicate) {
			foreach(T value in data) {
				if(predicate(value)) yield return value;
			}
		}
	}
}
