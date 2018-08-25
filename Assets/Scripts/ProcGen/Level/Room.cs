// Kevin Hagen
// 20.08.2018

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace ProcGen.Level
{
	public class Room : MonoWrapper
	{
		#region RoomType enum

		public enum RoomType
		{
			Basic,
			Treasure,
			Trap,
			Boss,
			None
		}

		#endregion

		#region Serialize Fields

		[SerializeField] [FormerlySerializedAs("_configuration")]
		private RoomPreset _preset;
		[SerializeField] private Transform _upperLeftCorner;

		#endregion

		#region Private Fields

		private RoomType _type;
		private Vector2 _gridPosition;
		private bool[] _availableDoors;
		private int[] _neighbourIDs;

		#endregion

		#region Properties

		public Vector2 Dimensions => _preset.RoomDimension;
		public int DoorAmount => _preset.Doors.Count(t => t);
		public bool[] Doors => _preset.Doors;
		public bool IsBossRoom => _preset.Type == RoomType.Boss;
		public Vector2 UpperLeftCorner => _upperLeftCorner.position;

		#endregion

		#region Public methods

		public void Init(LevelGenerator.RoomData roomData, RoomType type)
		{
			if (type == RoomType.None)
			{
				Log("Initializing a non-existing room. Check whether this is wanted.", LogType.Warning);
				return;
			}

			_type = type;
			_gridPosition = roomData.GridPosition;
			_availableDoors = roomData.Doors;
			_neighbourIDs = roomData.Neighbours;

			SpawnMonsters();
		}

		#endregion

		#region Private methods

		private void SpawnMonsters()
		{
		}

		#endregion

		#region Nested type: RoomPreset

		[Serializable]
		public struct RoomPreset
		{
			#region Public Fields

			[Tooltip("Up/Down/Left/Right")] public bool[] Doors;
			public int MaxMonsters;
			public List<EnemyController> PossibleEnemiesList;
			public Vector2 RoomDimension;
			public RoomType Type;

			#endregion
		}

		#endregion
	}
}