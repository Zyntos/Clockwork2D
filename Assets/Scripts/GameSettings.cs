// Kevin Hagen
// 19.08.2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

[CreateAssetMenu(menuName = "ScriptableSingletons/GameSettings")]
public class GameSettings : ScriptableObjectSingleton<GameSettings>
{
	[Header("Rooms")] public float TileSize;
	public int RoomWidth;
	public int RoomHeight;
	[Range(0, 1)] public float BasicRoomChance;
	[Range(0, 1)] public float TrapRoomChance;
	[Range(0, 1)] public float TreasureRoomChance;
	[Range(0, 1), Tooltip("Does this room contain a chest?")] public float TreasureChestChance;

	[Header("LevelGen")] [Range(0, 1)]
	public float LeaveEarlyChance;

	protected override void OnCreate()
	{
		Debug.Log("Create " + ScriptableName);
	}
}
