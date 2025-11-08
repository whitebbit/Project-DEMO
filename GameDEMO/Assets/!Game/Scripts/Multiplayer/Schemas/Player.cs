// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.46
// 

using Colyseus.Schema;

public partial class Player : Schema {
	[Type(0, "ref", typeof(Vector3Schema))]
	public Vector3Schema position = new Vector3Schema();

	[Type(1, "ref", typeof(Vector3Schema))]
	public Vector3Schema velocity = new Vector3Schema();

	[Type(2, "ref", typeof(Vector2Schema))]
	public Vector2Schema rotation = new Vector2Schema();

	[Type(3, "number")]
	public float speed = default(float);

	[Type(4, "int8")]
	public sbyte maxHP = default(sbyte);

	[Type(5, "int8")]
	public sbyte currentHP = default(sbyte);

	[Type(6, "uint8")]
	public byte loss = default(byte);

	[Type(7, "int8")]
	public sbyte wI = default(sbyte);
}

