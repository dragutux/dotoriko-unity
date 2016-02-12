using UnityEngine;
using System.Collections.Generic;
using DotOriko.Core;

namespace DotOriko.Async.SceneLoad {
	public class SceneSet {
		public List<string> scenes;

		public SceneSet(params string[] list) {
			this.scenes = new List<string> ();
			foreach (var s in list) {
				this.scenes.Add(s);
			}
		}
	}

	public sealed class SceneManagerAsync : DotOrikoSingleton<SceneManagerAsync> {

		public Dictionary<string, GameObject> cachedScenes;

		public void LoadNext() {

		}

		public void LoadNextAsync() {

		}

	}
}
