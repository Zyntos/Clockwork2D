//Kevin Hagen
//26.08.2018

using System.Collections.Generic;
using ProcGen.Level;
using UnityEngine;
using Utility;

namespace Level
{
	public class MapDrawer : Singleton<MapDrawer>
	{
		private const int MapTileHeight = 5;
		private const int MapTileWidth = 10;

		#region Serialize Fields

		[SerializeField] private GameObject _tilePrefab;

		#endregion

		#region Private Fields
	
		private List<SpriteRenderer> _renderers;
		private Camera _minimapCamera;
		private Vector3 _offset;

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
			
			_renderers = new List<SpriteRenderer>();
			_offset = new Vector3(0, 0, -10);
			
			_minimapCamera = GetComponentInChildren<Camera>();
			_minimapCamera.clearFlags = CameraClearFlags.Depth;
			_minimapCamera.orthographic = true;
			_minimapCamera.depth = 1;
			_minimapCamera.rect = new Rect(0.1f,0.9f, 0.35f, 0.2f);
		}

		#endregion

		#region Public methods

		public void DrawpMap(List<Room> rooms)
		{
			foreach (Room room in rooms)
			{
				GameObject mapTile = Instantiate(_tilePrefab, transform);
				Vector2 cellSize = LevelGenerator.Instance.CellSize;
				Vector2 actualPosition = new Vector2(room.GridPosition.x * cellSize.x * MapTileWidth, room.GridPosition.y * cellSize.y * MapTileHeight);

				if (room.Dimensions.x < cellSize.x)
					actualPosition.x++;
				if (room.Dimensions.y < cellSize.y)
					actualPosition.y--;
				mapTile.transform.position = actualPosition;

				SpriteRenderer sr = mapTile.GetComponent<SpriteRenderer>();
				sr.sprite = room.MiniMapSprite;
				sr.enabled = false;
				_renderers.Add(sr);
			}

			_minimapCamera.transform.position = _renderers[0].transform.position;
		}

		#endregion

		#region Private methods

		//TODO maybe use linerenderer + 1 point per door so that i can connect them?
		private void DrawPathsBetweenRooms()
		{
		}

		private void OnDoorEntered(int roomid, Vector2 from)
		{
			if (!_renderers[roomid].enabled)
				UnlockRoom(roomid);

			ShowRoom(roomid);
		}

		//TODO Maybe some fancy animation stuff?
		private void UnlockRoom(int roomid)
		{
			_renderers[roomid].enabled = true;
		}

		private void ShowRoom(int index)
		{
			Vector3 targetPosition = _renderers[index].transform.position + _offset;
			_minimapCamera.transform.position = Vector3.Lerp(_minimapCamera.transform.position, targetPosition, 0.3f);	
		}

		#endregion
	}
}