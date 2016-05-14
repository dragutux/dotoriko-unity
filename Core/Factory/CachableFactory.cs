///
/// DotOriko v1.0
/// Abstract factory
/// By NoxCaos 11.02.2016
///

using System.Collections.Generic;
using UnityEngine;
using System;

namespace DotOriko.Core.Factory {

    public abstract class CachableFactory<T, N> 
        where T : UnityEngine.Object
        where N : struct, IConvertible {

        protected static Dictionary<N, string> typeToUrl;

        private static Dictionary<string, T> cachedItems;

        public static T GetItem(string name) {
            return GetItemByLink(string.Format("{0}/{1}", typeof(T), name));
        }

        public static T GetItem(N type) {
            return GetItemByLink(typeToUrl[type]);
        }

        public static B SpawnItem<B>(N type) where B : MonoBehaviour {
            return UnityEngine.Object.Instantiate(GetItem(type)) as B;
        }

        public static void ClearCache() {
            foreach(var l in cachedItems) {
                Resources.UnloadAsset(l.Value);
            }
            cachedItems.Clear();
        }

        private static T GetItemByLink(string link) {
            if (cachedItems.ContainsKey(link)) {
                return cachedItems[link];
            } else {
                var item = Resources.Load(link) as T;
                if (item != null) cachedItems.Add(link, item);

                return item;
            }
        }
    }
}
