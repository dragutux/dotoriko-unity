// 
//  XmlColor.cs
//  
//  Author:
//       Denis Oleynik <denis@meliorgames.com>
// 
//  Copyright (c) 2013 Melior Games Inc.
// 
//  
using System;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

namespace DotOriko.Data.Serialization
{
	public sealed class SerializableColor
	{	
	    public SerializableColor() {}
	    public SerializableColor(Color c) 
		{
			a = c.a;
			r = c.r;
			g = c.g;
			b = c.b;
		}

	    public static implicit operator Color(SerializableColor color)
	    {
	        return new Color(color.r,color.g,color.b,color.a);
	    }
	
	    public static implicit operator SerializableColor(Color color)
	    {
	        return new SerializableColor(color);
	    }
	
		[XmlAttribute]
		public float a;
	
		[XmlAttribute]
		public float r;
		
		[XmlAttribute]
		public float g;
		
		[XmlAttribute]
		public float b;
	}
}

