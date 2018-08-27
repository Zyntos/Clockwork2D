// Kevin Hagen
// 20.08.2018

using System;
using System.Collections.Generic;
using Level;
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
		[SerializeField] private GameObject _cornersObj;

		#endregion

		#region Private Fields

		private bool[] _availableDoors;
		private int[] _neighbourIDs;
		private Transform[] _corners;
		private RoomType _type;

		#endregion

		#region Properties

		public Vector2 Dimensions => _preset.RoomDimension;
		public Door[] Doors => _preset.Doors;
		public bool IsBossRoom => _preset.Type == RoomType.Boss;
		public Sprite MiniMapSprite => _preset.MinimapSprite;
		public Vector2 UpperLeftCorner => _corners[0].position;
		public Vector2 GridPosition { get; private set; }
		public int OwnID { get; private set; }

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
			GridPosition = roomData.GridPosition;
			_availableDoors = roomData.Doors;
			_neighbourIDs = roomData.Neighbours;
			OwnID = roomData.ID;

			_corners = new Transform[_cornersObj.transform.childCount];
			for (int i = 0; i < _corners.Length; i++)
			{
				_corners[i] = _cornersObj.transform.GetChild(i);
			}

			LockUnavailableDoors();
			InitializeDoors();
			SpawnMonsters();
		}

		#endregion

		#region Private methods

		private void LockUnavailableDoors()
		{
			for (int i = 0; i < _availableDoors.Length; i++)
			{
				if (!_availableDoors[i] && _preset.Doors[i])
				{
					_preset.Doors[i].Lock();
				}
			}
		}

		private void InitializeDoors()
		{
			for (int i = 0; i < _preset.Doors.Length; i++)
			{
				if (_availableDoors[i] && _preset.Doors[i])
					_preset.Doors[i].Init(_neighbourIDs[i], LevelManager.GetDirectionFromIndex(i));
			}
		}

		private void SpawnMonsters()
		{
		}

		#endregion

		#region Nested type: RoomPreset

		[Serializable]
		public struct RoomPreset
		{
			#region Public Fields

			[Tooltip("Up/Down/Left/Right")] public Door[] Doors;
			public int MaxMonsters;
			public Sprite MinimapSprite;
			public List<EnemyController> PossibleEnemiesList;
			public Vector2 RoomDimension;
			public RoomType Type;

			#endregion
		}

		#endregion
	}
}