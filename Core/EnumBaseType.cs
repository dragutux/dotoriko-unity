///
/// DotOriko v1.0
/// Custom enum
/// By NoxCaos 11.02.2016
/// Source: http://www.codeproject.com/Articles/20805/Enhancing-C-Enums
///

using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace DotOriko.Core {
    public abstract class EnumBaseType<T> where T : EnumBaseType<T> {
        protected static List<T> enumValues = new List<T>();

        public readonly int Key;
        public readonly string Value;

        public EnumBaseType(int key, string value) {
            Key = key;
            Value = value;
            enumValues.Add((T)this);
        }

        protected static ReadOnlyCollection<T> GetBaseValues() {
            return enumValues.AsReadOnly();
        }

        protected static T GetBaseByKey(int key) {
            foreach (T t in enumValues) if (t.Key == key) return t;
            return null;
        }

        protected static T BaseFromString(string val) {
            foreach (T t in enumValues) if (t.Value == val) return t;
            return null;
        }

        public override string ToString() {
            return Value;
        }
    }
}