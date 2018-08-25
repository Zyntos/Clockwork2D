using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Random = UnityEngine.Random;

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

		[SerializeField, FormerlySerializedAs("_configuration")] private RoomPreset _preset;
		[SerializeField] private Transform _upperLeftCorner;

		#endregion

		#region Private Fields

		private RoomType _type;
		private Vector2 _gridPosition;

		#endregion

		#region Properties

		public Vector2 UpperLeftCorner => _upperLeftCorner.position;
		public int DoorAmount => _preset.Doors.Count(t => t);
		public Vector2 Dimensions => _preset.RoomDimension;
		public bool[] Doors => _preset.Doors;
		public bool IsBossRoom => _preset.Type == RoomType.Boss;

		#endregion

		#region Public methods

		public void Init(Vector2 position, bool pickRandomType = false)
		{
			_gridPosition = position;
			if (pickRandomType)
			{
				_type = (RoomType) Random.Range(0, (int) RoomType.None);
			}

			if (_type == RoomType.None)
			{
				Log("Initializing a non-existing room. Check whether this is wanted.", LogType.Warning);
				return;
			}

			SpawnMonsters();
		}

		public void InitWithType(RoomType type)
		{
			_type = type;
			Init(Vector2.zero);
		}

		#endregion

		#region Private methods

		private void SpawnMonsters()
		{
		}

		#endregion

		#region Nested type: RoomConfiguration

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