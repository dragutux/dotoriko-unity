// 
//  XmlStoragePolicy.cs
//  
//  Author:
//       Denis Oleynik <denis@meliorgames.com>
// 
//  Copyright (c) 2013 Melior Games Inc.
// 
//  
using System;
using System.Xml.Serialization;
using System.IO;

namespace DotOriko.Data.Serialization
{
	public sealed class XmlSerializationPolicy:SerializationPolicy
	{
		public XmlSerializationPolicy()
		{
		}
		
		public override string FileExtention
		{
			get 
			{
				return "xml";
			}
		}
		
		public override T Restore<T> (Stream stream)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			return (T)xmlSerializer.Deserialize(stream);
		}

		public override void Store<T> (T item,Stream stream)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

			xmlSerializer.Serialize(stream,item);
		}
	}
}