using UnityEngine;
using Newtonsoft.Json;
using DotOriko.Core;
using System;
using System.IO;

namespace DotOriko.Data {

    public enum StorageType {
        Documents, Resources
    }

    public abstract class Config<TConfig> where TConfig : Config<TConfig> {

        public const string CONFIGS_FOLDER = "Configs";
        public const string EXTENTION = ".json";

        private static string _documentsPath;

        private string _name;

        public Config(string name) {
            _name = name + EXTENTION;
        }

        public virtual void OnRestored() { }

        public static TConfig Load(StorageType type, string name) {
            if (type == StorageType.Resources) {
                TextAsset tAsset = null;
                try {
                    tAsset = Resources.Load(CONFIGS_FOLDER + "/" + name) as TextAsset;
                } catch (Exception e) {
                    Errors.Log("[Data Config Load] EXCEPTION: {0}", e.Message);
                }
                return Load(tAsset);
            } else if (type == StorageType.Documents) {
                TConfig config = null;
                try {
                    var data = File.ReadAllText(Path.Combine(GetDocumentsPath(), name + EXTENTION));
                    config = JsonConvert.DeserializeObject<TConfig>(data);
                    config.OnRestored();
                } catch (IOException e) {
                    Errors.Log("[Data Config Load] EXCEPTION: {0}", e.Message);
                }
                return config;
            }
            return null;
        }

        public static TConfig Load(TextAsset asset) {
            TConfig config = null;

            try {
                config = JsonConvert.DeserializeObject<TConfig>(asset.text);
                config.OnRestored();
            } catch (Exception e) {
                Errors.Log("[Data Config Convert] EXCEPTION: {0}", e.Message);
            }

            return config;
        }

        public static string GetDocumentsPath() {
            if (!string.IsNullOrEmpty(_documentsPath))
                return _documentsPath;

#if UNITY_EDITOR
            _documentsPath = Path.Combine(Application.dataPath, "..");
            _documentsPath = Path.Combine(_documentsPath, "Data");
            _documentsPath = Path.Combine(_documentsPath, CONFIGS_FOLDER);
            Directory.CreateDirectory(_documentsPath);
#else
		    _documentsPath = Application.persistentDataPath;
#endif

            return _documentsPath;
        }

        public string GetJson() {
            return JsonConvert.SerializeObject(this);
        }

        public void Save() {
            try {
                File.WriteAllText(Path.Combine(GetDocumentsPath(), _name), GetJson());
            } catch (IOException e) {
                Errors.Log("[Data Config] EXCEPTION: {0}", e.Message);
            }
        }
    }
}
