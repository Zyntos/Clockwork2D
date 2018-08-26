﻿//Kevin Hagen
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
		public CharController Character;

		private List<Room> _rooms;

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
			Character.gameObject.SetActive(false);
		}

		private void Start()
		{
			_rooms = LevelGenerator.Instance.GenerateLevel();
			OnDoorEntered(1, Vector2.right);
			Character.gameObject.SetActive(true);
		}

		private void OnDoorEntered(int roomID, Vector2 from)
		{
			Room room = GetRoomByID(roomID);
			Character.transform.position = room.Doors[GetIndexFromDirection(from)].transform.position + new Vector3(from.x, from.y, 0) * -3;
		}

		private Room GetRoomByID(int id)
		{
			return _rooms.FirstOrDefault(room => room.OwnID == id);
		}

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
	}
}