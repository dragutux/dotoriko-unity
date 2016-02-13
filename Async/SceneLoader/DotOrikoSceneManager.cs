///
/// DotOriko v1.0
/// Async scene loading manager
/// By NoxCaos 11.02.2016
///
using UnityEngine;
using DotOriko.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DotOriko.Async.SceneLoad {
    public enum SceneStatus {
        Active,
        Hidden,
        Acitvating,
        Deactivating,
        NotCached
    }

	public class SceneSet {
		public List<string> scenes;

		public SceneSet(params string[] list) {
			this.scenes = new List<string> ();
			foreach (var s in list) {
				this.scenes.Add(s);
			}
		}
	}

	public sealed class DotOrikoSceneManager : DotOrikoSingleton<DotOrikoSceneManager> {

		public Dictionary<string, SceneController> cachedScenes { get; private set; }

        public Action<float, string> OnCacheProgressChanged;
        public Action<string> OnSceneCached;
        public Action OnSceneSetCached;

        public void CacheSet(SceneSet set) {
            this.CacheSetWithLogic(set, new CenterArrangementLogic());
        }

        public void CacheSetWithLogic(SceneSet set, ISceneArrangmentLogic logic) {
            this.StartCoroutine(this.CacheScenes(set.scenes, logic));
        }

        private IEnumerator CacheScenes(List<string> scenes, ISceneArrangmentLogic logic) {
            int i = 0, prog = 0;
            var total = scenes.Count * 3;
            foreach (var s in scenes) {
                this.UpdateProgress(prog/total, "Loading scene " + s);
                yield return Application.LoadLevelAdditiveAsync(s);
                var loc = GameObject.Find(s);
                var locTr = loc.transform;
                var locRoot = loc.GetComponent<SceneController>();
                prog++;
                this.UpdateProgress(prog / total, "Processing scene " + s);

                this.cachedScenes.Add(s, locRoot);
                logic.AddScene(i, ref locTr);
                locRoot.SceneObjects = new List<GameObject>();
                yield return new WaitForEndOfFrame();

                foreach (Transform g in loc.transform) {
                    g.gameObject.SetActive(false);
                    locRoot.SceneObjects.Add(g.gameObject);
                }
                prog++;
                this.UpdateProgress(prog / total, "Finished loading " + s);
                if (this.OnSceneCached != null) this.OnSceneCached(name);
            }
            if (this.OnSceneSetCached != null) this.OnSceneSetCached();
        }

        private void UpdateProgress(float prog, string eve) {
            if (this.OnCacheProgressChanged != null)
                this.OnCacheProgressChanged(prog, eve);
        }

        public void ShowScene(string name) {
            if (this.cachedScenes.ContainsKey(name)) {
                this.cachedScenes[name].Activate();
            } else {
                Debug.LogError("[DotOriko] SceneManager has no '" + name + "' cached");
            }
        }

        public void HideScene(string name) {
            if (this.cachedScenes.ContainsKey(name)) {
                this.cachedScenes[name].Deactivate();
            } else {
                Debug.LogError("[DotOriko] SceneManager has no '" + name + "' cached");
            }
        }

        public SceneStatus GetSceneStatusOf(string name) {
            if (this.cachedScenes.ContainsKey(name)) {
                return this.cachedScenes[name].status;
            } else return SceneStatus.NotCached;
        }

    }
}
