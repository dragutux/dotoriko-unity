///
/// DotOriko v1.0
/// Abstract factory
/// By NoxCaos 11.02.2016
///

using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

namespace DotOriko.Core.Factory {
    public abstract class FactoryType : EnumBaseType<FactoryType> {

        public FactoryType(int key, string value) : base(key, value) { }

        public static ReadOnlyCollection<FactoryType> GetValues() {
            return GetBaseValues();
        }

        public static FactoryType GetByKey(int key) {
            return GetBaseByKey(key);
        }

        public static FactoryType FromString(string val) {
            return BaseFromString(val);
        }

    }

    public abstract class CachableFactory<T> where T : Object {

        protected static Dictionary<FactoryType, string> typeToUrl;

        private static Dictionary<string, T> cachedItems;

        public static T GetItem(string name) {
            return GetItemByLink(string.Format("{0}/{1}", typeof(T), name));
        }

        public static T GetItem(FactoryType type) {
            return GetItemByLink(typeToUrl[type]);
        }

        public static B SpawnItem<B>(FactoryType type) where B : MonoBehaviour {
            return GameObject.Instantiate(GetItem(type)) as B;
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
