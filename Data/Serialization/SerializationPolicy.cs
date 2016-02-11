// 
//  StoragePolicy.cs
//  
//  Author:
//       Denis Oleynik <denis@meliorgames.com>
// 
//  Copyright (c) 2013 Melior Games Inc.
// 
//  
using System;
using System.IO;

namespace DespairWorld.Data.Serialization
{
	public abstract class SerializationPolicy
	{
		public abstract String FileExtention{get;}
		
		public abstract void Store<T>(T item,Stream stream);
		public abstract T Restore<T>(Stream stream);
	}
}