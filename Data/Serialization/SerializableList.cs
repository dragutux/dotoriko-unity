//
//  SerializableList.cs
//
//  Author:
//       Denis Oleynik <denis@meliorgames.com>
//
//  Copyright (c) 2013 Melior Games Inc.
//
//
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace DotOriko.Data.Serialization
{
	[Serializable]
	public class SerializableList<T>:List<T>,ISerializable
	{
		#region ISerializable implementation

		public void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue("1", this.ToArray());
		}

		#endregion

		public SerializableList(SerializationInfo info, StreamingContext context)
		{
			AddRange( (T[])info.GetValue("1",typeof(T[])));
		}

		public SerializableList ()
		{
		}
	}
}