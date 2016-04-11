using UnityEngine;
using System.Collections.Generic;

namespace DotOriko.Core.Pool {
    public sealed class ObjectPool : DotOrikoSingleton<ObjectPool> {

        private Dictionary<string, List<GameObject>> storedObjects;

        private Transform poolParent;

        public void AddObject(GameObject obj, int poolSize) {
            var objName = obj.name;
            if(this.poolParent == null) {
                var pp = new GameObject();
                this.poolParent = pp.transform;
                this.poolParent.name = "Object_pool_container";
                DontDestroyOnLoad(this.poolParent);
            }

            if(this.storedObjects.ContainsKey(objName)) {
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
                this.storedObjects.Add(obj.name, list);
            }
        }

        public void RemoveObject(string name) {

        }
        
        public void Clear() {

        }

        public void SpawnObject(string name) {

        }

        public void DestroyObject(string name) {

        }
    }
}
