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

    public abstract class CachableFactory<T> where T : MonoBehaviour {

        protected static Dictionary<FactoryType, string> typeToUrl;

        private Dictionary<string, T> cachedItems;

        public virtual T GetItem(FactoryType type) {
            return this.GetItemByLink(typeToUrl[type]);
        }

        public virtual B SpawnItem<B>(FactoryType type) where B : MonoBehaviour {
            return GameObject.Instantiate(this.GetItem(type)) as B;
        }

        private T GetItemByLink(string link) {
            if (this.cachedItems.ContainsKey(link)) {
                return this.cachedItems[link];
            } else {
                var item = Resources.Load(link) as T;
                if (item != null) this.cachedItems.Add(link, item);

                return item;
            }
        }
    }
}
