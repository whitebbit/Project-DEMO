// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.46
// 

using Colyseus.Schema;
using UnityEngine;

public partial class Vector2Schema : Schema
{
    [Type(0, "number")] public float x = default(float);

    [Type(1, "number")] public float y = default(float);
}

public static class Vector2SchemaExtensions
{
    public static Vector2 ToVector2(this Vector2Schema vector)
    {
        return new Vector2(vector.x, vector.y);
    }
}