// 
//  Config.cs
//  
//  Author:
//       Denis Oleynik <denis@meliorgames.com>
// 
//  2013 Melior Games Inc.
// 
//  
using System;
using UnityEngine;
using System.IO;
using DotOriko.Data.Serialization;

namespace DotOriko.Data
{
	public enum StorageType
	{
		Documents,
		StreamingAssets
	}
	
	
	public abstract class Config<TConfig,TSerializationPolicy> 
		where TSerializationPolicy:SerializationPolicy,new()
			where TConfig:Config<TConfig,TSerializationPolicy>
	{
		public const string CONFIGS_FOLDER = "Configs";
		#if UNITY_EDITOR
		private static readonly string _applicationDataPath = Application.dataPath;
		#endif
		private static string _documentsPath;
		
		private string _name;
		
		private string _path;
		
		public Config (String name)
		{
			_name = GetNameWithExtension(name);
		}
		
		public virtual void OnRestored()
		{
		}
		
		public static TConfig Load(StorageType storage, string name) 
		{
			return Load(Path.Combine(GetPath(storage), GetNameWithExtension(name)));
		}
		
		private static TConfig Load(StorageType storage, string name, MemoryStream stream) 
		{
			TSerializationPolicy serializer = new TSerializationPolicy();
			
			TConfig config = null;
			
			using(stream)
			{
				config = serializer.Restore<TConfig>(stream);
			}
			
			config._path = Path.Combine(GetPath(storage), GetNameWithExtension(name));
			config.OnRestored();
			
			return config;
		}
		
		private static TConfig Load(string filepath) 
		{
            TSerializationPolicy serializer = new TSerializationPolicy();
			
			TConfig config = null;
			
			using(Stream stream = File.OpenRead(filepath))
			{
				config = serializer.Restore<TConfig>(stream);
			}
			
			config._path = filepath;
			config.OnRestored();
			
			return config;
		}
		
		private static string GetNameWithExtension(string name)
		{
			TSerializationPolicy serializer = new TSerializationPolicy();
			return string.Format("{0}.{1}",name, serializer.FileExtention);
		}
		
		public static bool IsExists (StorageType storage, string name)
		{
			return File.Exists(Path.Combine(GetPath(storage), GetNameWithExtension(name)));
		}
		
		public static string GetPath(StorageType storage)
		{
			if (storage == StorageType.StreamingAssets)
			{
				return GetDocumentsPath();
			}
			
			string folder = string.Empty;
			
			if(storage == StorageType.Documents)
				folder = GetDocumentsPath();
			else
			{
				folder = Application.streamingAssetsPath;
			}
			
			return Path.Combine(folder,CONFIGS_FOLDER);
		}
		
		public static string GetDocumentsPath()
		{
			if (!string.IsNullOrEmpty(_documentsPath))
				return _documentsPath;
			
			#if UNITY_EDITOR
			_documentsPath = System.IO.Path.Combine(_applicationDataPath,"..");
			_documentsPath = System.IO.Path.Combine(_documentsPath,"Data");
			#else
			_documentsPath = Application.persistentDataPath;
			#endif
			
			return _documentsPath;
		}
		
		public void Save(StorageType storage)
		{
			if(string.IsNullOrEmpty(_path))
			{
				#if UNITY_EDITOR
				_path = GetPath( storage );
				#else
				_path = GetPath( storage );
				#endif
				
				if(!Directory.Exists(_path))
					Directory.CreateDirectory(_path);
				
				_path = Path.Combine(_path,_name);
			}
			
			TSerializationPolicy serializer = new TSerializationPolicy();
			
			using(Stream stream = File.Open(_path, FileMode.Create, FileAccess.Write))
			{
				serializer.Store<TConfig>((TConfig)this,stream);
			}
		}
		
		public void Save(StorageType storage, string filename)
		{
			string filenameWithExt = GetNameWithExtension(filename);
			#if UNITY_EDITOR
			_path = (storage == StorageType.Documents)? 
				GetPath( storage ) : CombinePath(_applicationDataPath, "Editor", "packedDocuments", "Configs");
			_path = Path.Combine(_path,filenameWithExt);
			#else
			if(string.IsNullOrEmpty(_path))
			{
				_path = GetPath( storage );
				
				if(!Directory.Exists(_path))
					Directory.CreateDirectory(_path);
				
				_path = Path.Combine(_path,filenameWithExt);
			}
			#endif
			
			TSerializationPolicy serializer = new TSerializationPolicy();
			
			using(Stream stream = File.Open(_path, FileMode.Create, FileAccess.Write))
			{
				serializer.Store<TConfig>((TConfig)this,stream);
			}
		}

		public static string CombinePath(params string[] names)
		{
			string result = names[0];
			
			for (int i = 1; i < names.Length; i++)
			{
				result = Path.Combine(result, names[i]);
			}
			return result;
		}
	}
}