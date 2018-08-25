// Kevin Hagen
// 10.08.2018

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace ProcGen.Level
{
	/*
	 * Layout like:
	 *
	 *	  0  1  2  3
	 *	  4  5  6  7
	 *	  8  9 10 11
	 *	 12 13 14 15
	 *
	 */
	public class LevelGenerator : Singleton<LevelGenerator>
	{
		#region Static Stuff

		private const int MaximumSearchAttempts = 1000;
		private static int RoomDataID { get; set; }

		#endregion

		#region Serialize Fields

		[SerializeField] private GameObject _level;
		[SerializeField] [Tooltip("This number is going to be squared")]
		private int _gridSize = 6;
		[SerializeField] private float _minimumRoomMultiplicator = 0.5f;
		[SerializeField] private float _maximumRoomMultiplicator = 0.85f;
		[SerializeField] private float _offsetBetweenRooms = 3.0f;
		[SerializeField] private Room[] _roomPresets;

		#endregion

		#region Private Fields

		private List<RoomData> _roomDatas;
		private Dictionary<RoomData, Room> _roomDictionary;
		private Vector2 _cellSize;
		private int _minimumRoomAmount;
		private int _maximumRoomAmount;
		private int _targetRoomAmount;

		#endregion

		#region Unity methods

		protected override void Awake()
		{
			base.Awake();

			_roomDatas = new List<RoomData>();
			_roomDictionary = new Dictionary<RoomData, Room>();

			float x = 0, y = 0;
			foreach (Room roomPreset in _roomPresets)
			{
				x = roomPreset.Dimensions.x > x ? roomPreset.Dimensions.x : x;
				y = roomPreset.Dimensions.y > y ? roomPreset.Dimensions.y : y;
			}

			_cellSize = new Vector2(x, y);

			_minimumRoomAmount = (int) (_gridSize * _gridSize * _minimumRoomMultiplicator);
			_maximumRoomAmount = (int) (_gridSize * _gridSize * _maximumRoomMultiplicator);
			_targetRoomAmount = Random.Range(_minimumRoomAmount, _maximumRoomAmount);
		}

		private void Start()
		{
			Initialize();
		}

		#endregion

		#region Public methods

		//TODO Call by game manager, when the game starts
		public void Initialize(bool withBoss = true)
		{
			if (_roomPresets.Length == 0)
			{
				Log("_roomPresets are empty. Please set up!", LogType.Error);
				return;
			}

			do
			{
				GenerateBasicLayout();
			} while (_roomDatas.Count == 0);

			UpdateNeighboursAll();
			GenerateAdditionalRooms();

			if (withBoss)
			{
				DetermineBossRoom();
			}

			CreateRoomInstances();
		}

		#endregion

		#region Private methods

		private void GenerateBasicLayout()
		{
			Vector2 nextRoom = new Vector2(0, Random.Range(0, _gridSize));

			int previousYPos = -1;
			for (int i = 0; i < _gridSize; i++)
			{
				nextRoom.x = i;

				int direction = Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1;
				while ((IsInBounds(nextRoom) && !LeaveEarly()) || !_roomDatas.Any(data => data.GridPosition.x == i))
				{
					previousYPos = (int) nextRoom.y;
					_roomDatas.Add(new RoomData(nextRoom));
					nextRoom.y += direction;
				}

				nextRoom.y = previousYPos;
			}
		}

		private void GenerateAdditionalRooms()
		{
			if (_roomDatas.Count > _targetRoomAmount)
			{
				Log("Target Amount of rooms reached. No need to generate more rooms. Target was " + _targetRoomAmount);
				return;
			}

			while (_roomDatas.Count < _targetRoomAmount)
			{
				RoomData roomTobranchFrom;
				int searchAttempts = 0;
				do
				{
					searchAttempts++;
					roomTobranchFrom = PickRandomRoom(true);
				} while ((roomTobranchFrom.Amount > 3) && (searchAttempts < MaximumSearchAttempts));

				if (searchAttempts >= MaximumSearchAttempts)
					break;

				float keepBranchingChance = GameSettings.Instance.KeepBranchingChance;
				while (Random.value < keepBranchingChance)
				{
					RoomData newRoom = FindFreeNeighbour(roomTobranchFrom);
					if (_roomDatas.Contains(newRoom)) continue;

					_roomDatas.Add(newRoom);
					UpdateNeighboursSingle(newRoom);
					foreach (int neighbourID in newRoom.Neighbours)
					{
						if (neighbourID != -1)
							UpdateNeighboursSingle(GetRoomDataForID(neighbourID));
					}

					keepBranchingChance -= GameSettings.Instance.KeepBranchingDecayRate;
				}
			}
		}

		private void DetermineBossRoom()
		{
			List<RoomData> pickList = _roomDatas.Where(data => data.Amount == 1).ToList();
			RoomData roomData;
			if (pickList.Count > 0)
			{
				roomData = pickList[Random.Range(0, pickList.Count)];
			}
			else
			{
				Log("Theres no room with only 1 neighbour, can't determine Boss room", LogType.Warning);
				return;
			}

			List<Room> possibleRooms = _roomPresets.Where(room => room.IsBossRoom).ToList();

			InstantiateRoom(roomData, possibleRooms, Room.RoomType.Boss);
		}

		private void CreateRoomInstances()
		{
			foreach (RoomData roomData in _roomDatas)
			{
				if (_roomDictionary.ContainsKey(roomData)) continue;

				List<Room> possibleRooms = _roomPresets.Where(room =>
				                                              {
					                                              bool[] bs = roomData.Doors;
					                                              return !bs.Where((t, i) => t && !room.Doors[i]).Any();
				                                              }).ToList();

				InstantiateRoom(roomData, possibleRooms);
			}
		}

		private void InstantiateRoom(RoomData roomData, List<Room> possibleRooms, Room.RoomType type = Room.RoomType.Basic)
		{
			Room roomInstance = Instantiate(possibleRooms[Random.Range(0, possibleRooms.Count)], _level.transform);
			Vector2 actualPosition = new Vector2(roomData.GridPosition.x * _cellSize.x * GameSettings.Instance.RoomWidth, roomData.GridPosition.y * _cellSize.y * GameSettings.Instance.RoomHeight);
			actualPosition += -1 * roomInstance.UpperLeftCorner + new Vector2(roomData.GridPosition.x * _offsetBetweenRooms, roomData.GridPosition.y * _offsetBetweenRooms);
			roomInstance.transform.position = actualPosition;
			roomInstance.Init(roomData, type);

			_roomDictionary.Add(roomData, roomInstance);
		}

		private void UpdateNeighboursAll()
		{
			foreach (RoomData roomData in _roomDatas)
			{
				roomData.Neighbours = GetNeighboursForPosition(roomData.GridPosition);
				roomData.Doors = GetDoorsForRoomData(roomData);
			}
		}

		private void UpdateNeighboursSingle(RoomData roomData)
		{
			roomData.Neighbours = GetNeighboursForPosition(roomData.GridPosition);
			roomData.Doors = GetDoorsForRoomData(roomData);
		}

		private RoomData FindFreeNeighbour(RoomData roomTobranchFrom)
		{
			RoomData newRoom;
			int searchAttempts = 0;
			do
			{
				searchAttempts++;
				newRoom = roomTobranchFrom.Copy();
				bool moveHor = Random.value < 0.5f;
				int direction = Random.value < 0.5f ? 1 : -1;

				if (moveHor)
					newRoom.GridPosition.x += direction;
				else
					newRoom.GridPosition.y += direction;
			} while ((_roomDatas.Contains(newRoom) || (newRoom.GridPosition.x > _gridSize) || (newRoom.GridPosition.y > _gridSize) || (newRoom.GridPosition.x < 0) || (newRoom.GridPosition.y < 0)) && (searchAttempts < MaximumSearchAttempts));

			return newRoom;
		}

		private RoomData PickRandomRoom(bool weighted = false)
		{
			if (_roomDatas.Count == 0)
				throw new ArgumentOutOfRangeException(nameof(_roomDatas), "Can't randomly pick a room, because there is no roomData");

			RoomData roomData;

			if (weighted)
			{
				int neighbours;
				int attempts = 0;

				List<RoomData> pickList;
				do
				{
					float roll = Random.value;

					if (roll < GameSettings.Instance.WithOneNeighbourChance)
						neighbours = 1;
					else if (roll < GameSettings.Instance.WithTwoNeighoursChance)
						neighbours = 2;
					else
						neighbours = 3;

					pickList = _roomDatas.Where(data => data.Amount == neighbours).ToList();
					attempts++;
				} while ((pickList.Count == 0) && (attempts < MaximumSearchAttempts));

				if (attempts >= MaximumSearchAttempts)
					pickList = _roomDatas;

				roomData = pickList[Random.Range(0, pickList.Count)];
			}
			else
			{
				roomData = _roomDatas[Random.Range(0, _roomDatas.Count)];
			}

			return roomData;
		}

		private bool[] GetDoorsForRoomData(RoomData roomData)
		{
			bool[] doors = new bool[4];

			for (int i = 0; i < roomData.Neighbours.Length; i++)
			{
				if (roomData.Neighbours[i] != -1)
					doors[i] = true;
			}

			return doors;
		}

		private int[] GetNeighboursForPosition(Vector2 gridPosition)
		{
			int[] neighbours = new int[4];

			for (int i = 0; i < neighbours.Length; i++)
			{
				Vector2 possibleNeighbour = gridPosition + GetDirectionFromIndex(i);
				RoomData neighbour = _roomDatas.FirstOrDefault(room => room.GridPosition == possibleNeighbour);
				if (neighbour != null)
				{
					neighbours[i] = neighbour.ID;
				}
				else
				{
					neighbours[i] = -1;
				}
			}

			return neighbours;
		}

		private RoomData GetRoomDataForID(int id)
		{
			return _roomDatas.FirstOrDefault(room => room.ID == id);
		}

		private Vector2 GetDirectionFromIndex(int index)
		{
			Vector2 retVec;
			switch (index)
			{
				case 0:
					retVec = Vector2.up;
					break;
				case 1:
					retVec = Vector2.down;
					break;
				case 2:
					retVec = Vector2.left;
					break;
				case 3:
					retVec = Vector2.right;
					break;
				default:
					retVec = default(Vector2);
					break;
			}

			return retVec;
		}

		private bool LeaveEarly()
		{
			return Random.value < GameSettings.Instance.LeaveEarlyChance;
		}

		private bool IsInBounds(Vector2 nextRoom)
		{
			return (nextRoom.x < _gridSize) && (nextRoom.x >= 0) && (nextRoom.y < _gridSize) && (nextRoom.y >= 0);
		}

		#endregion

		#region Nested type: RoomData

		[Serializable]
		public class RoomData
		{
			#region Public Fields

			public bool[] Doors;
			public Vector2 GridPosition;
			public int[] Neighbours;

			#endregion

			#region Private Fields

			#endregion

			#region Properties

			public int Amount => Neighbours.Count(i => i != -1);
			public int ID { get; }

			#endregion

			#region Constructors

			public RoomData(Vector2 gridPosition)
			{
				GridPosition = gridPosition;
				ID = RoomDataID++;
			}

			#endregion

			#region Public methods

			public RoomData Copy()
			{
				RoomData copy = new RoomData(GridPosition);
				copy.Doors = Doors;
				copy.Neighbours = Neighbours;

				return copy;
			}

			#endregion
		}

		#endregion
	}
}