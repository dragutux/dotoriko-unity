using UnityEngine;
using System.Collections.Generic;
using LinqTools;

namespace DotOriko.Core.Pool {
    public sealed class ObjectPool : DotOrikoSingleton<ObjectPool> {

        private Dictionary<string, List<GameObject>> storedObjects;

        private Transform poolParent;

        /// <summary>
        /// Adds new object type to pool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="poolSize"></param>
        public void AddObject(GameObject obj, int poolSize) {
            var objName = obj.name;
            if(this.poolParent == null) {
                var pp = new GameObject();
                this.poolParent = pp.transform;
                this.poolParent.name = "Object_pool_container";
                DontDestroyOnLoad(this.poolParent);
            }

            if(storedObjects.ContainsKey(objName)) {
                Errors.Log(Errors.objectPoolAddError);
            } else {
                var list = new List<GameObject>();
                var par = new GameObject();
                par.transform.SetParent(this.poolParent);
                par.name = obj.name + "_container";
                list.Add(par);

                for(int i=0; i<poolSize; i++) {
                    var g = Instantiate(obj) as GameObject;
                    g.SetActive(false);
                    g.transform.SetParent(par.transform);
                    list.Add(g);
                }
                storedObjects.Add(obj.name, list);
            }
        }

        /// <summary>
        /// Removes object type from pool
        /// </summary>
        /// <param name="name"></param>
        public void RemoveObject(string name) {
            if (storedObjects.ContainsKey(name)) {
                var pair = storedObjects[name];
                foreach(var g in pair) {
                    Destroy(g);
                }
            } else Errors.Log("Object pool remove error: No such object");
        }
        
        /// <summary>
        /// Clears all the pool
        /// </summary>
        public void Clear() {
            foreach(var k in storedObjects) {
                RemoveObject(k.Key);
            }
        }

        /// <summary>
        /// Gets item from pool
        /// </summary>
        /// <param name="name"></param>
        public GameObject SpawnObject(string name) {
            if (storedObjects.ContainsKey(name)) {
                var unactive = storedObjects[name].Where(o => !o.activeInHierarchy).FirstOrDefault();

                if (unactive == null) {
                    var g = Instantiate(storedObjects[name][0]) as GameObject;
                    g.transform.SetParent(GameObject.Find(name + "_container").transform);
                    storedObjects[name].Add(g);
                    return g;
                } else {
                    unactive.SetActive(true);
                    return unactive;
                }

            } else Errors.Log("Object pool remove error: No such object");
            return null;
        }

        /// <summary>
        /// Returns item to pool
        /// </summary>
        /// <param name="name"></param>
        public void DestroyObject(GameObject obj) {
            if (storedObjects.ContainsKey(obj.name)) {
                obj.SetActive(false);
            } else Errors.Log("Object pool remove error: No such object");
        }
    }
}
