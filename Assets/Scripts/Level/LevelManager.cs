//Kevin Hagen
//26.08.2018

using System.Collections.Generic;
using System.Linq;
using ProcGen.Level;
using UnityEngine;
using Utility;

namespace Level
{
	public class LevelManager : Singleton<LevelManager>
	{
		#region Static Stuff

		public static int GetIndexFromDirection(Vector2 vector)
		{
			int retInd;
			if (vector == Vector2.up)
				retInd = 0;
			else if (vector == Vector2.down)
				retInd = 1;
			else if (vector == Vector2.left)
				retInd = 2;
			else if (vector == Vector2.right)
				retInd = 3;
			else
				retInd = -1;

			return retInd;
		}

		public static Vector2 GetDirectionFromIndex(int index)
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

		#endregion

		#region Serialize Fields

		[SerializeField] private Vector3 _offScreenPosition = new Vector3(-40, 0, 0);
		[SerializeField] private GameObject _startRoomPrefab;
		[SerializeField] private CharController _character;
		[SerializeField] private Grid _levelGrid;

		#endregion

		#region Private Fields

		private List<Room> _rooms;

		#endregion

		#region Properties

		public Grid LevelGrid => _levelGrid;
		public int CurrentRoomID { get; private set; }
		public StartingRoom StartRoom { get; private set; }
		public Transform[] Corners { get; private set; }

		#endregion

		#region Unity methods

		private void OnEnable()
		{
			Door.doorEntered += OnDoorEntered;
		}

		private void OnDisable()
		{
			Door.doorEntered -= OnDoorEntered;
		}

		protected override void Awake()
		{
			base.Awake();
			//_character.gameObject.SetActive(false);
			CurrentRoomID = -1;

			GameObject startRoomObj = Instantiate(_startRoomPrefab, _levelGrid.transform);
			startRoomObj.transform.position = _offScreenPosition;
			StartRoom = startRoomObj.GetComponent<StartingRoom>();
			StartRoom.Setup();
		}

		private void Start()
		{
			_rooms = LevelGenerator.Instance.GenerateLevel(_levelGrid);
			MapDrawer.Instance.DrawpMap(_rooms);
			Corners = StartRoom.Corners;
		}

		#endregion

		#region Private methods

		private void OnDoorEntered(int roomID, Vector2 from)
		{
			CurrentRoomID = roomID;
			//Entering Starting room
			//TODO still need to set corners for starting room
			if (roomID == -1)
			{
				_character.transform.position = StartRoom.DungeonEntry.transform.position + new Vector3(from.x, from.y, 0) * -3;
				Corners = StartRoom.Corners;
				return;
			}

			Room room = GetRoomByID(roomID);
			_character.transform.position = room.Doors[GetIndexFromDirection(from)].transform.position + new Vector3(from.x, from.y, 0) *-1;
			Corners = room.Corners;
		}

		private Room GetRoomByID(int id)
		{
			return _rooms.FirstOrDefault(room => room.OwnID == id);
		}

		#endregion
	}
}