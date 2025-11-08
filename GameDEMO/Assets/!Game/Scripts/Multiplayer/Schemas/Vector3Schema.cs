// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.46
// 

using Colyseus.Schema;
using UnityEngine;

public partial class Vector3Schema : Schema {
	[Type(0, "number")]
	public float x = default(float);

	[Type(1, "number")]
	public float y = default(float);

	[Type(2, "number")]
	public float z = default(float);
}

public static class Vector3SchemaExtensions
{
	public static Vector3 ToVector3(this Vector3Schema vector)
	{
		return new Vector3(vector.x, vector.y, vector.z);
	}
}
