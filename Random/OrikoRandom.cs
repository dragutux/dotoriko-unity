using System.Collections.Generic;
using UnityEngine;

namespace DotOriko.Random {

	/// <summary>
	/// DotOriko random extentions and shortcut functions
	/// Based on UnityEngine.Random
	/// </summary>
	public static class OrikoRandom {

		/// <summary>
		/// Provides a probability in a form of boolen.
		/// Looks like 1:prob
		/// </summary>
		/// <param name="prob">The probability</param>
		public static bool Probability(int prob) {
			 return UnityEngine.Random.Range (0, prob) == 0;
		}
	}
}
