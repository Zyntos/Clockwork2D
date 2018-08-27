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
		#region Serialize Fields

		[SerializeField] private GameObject _tilePrefab;

		#endregion

		#region Private Fields

		private CharController _characterController;
		private List<SpriteRenderer> _renderers;
		private Camera _minimapCamera;
		private Vector2 cellSize;

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

			_characterController = GameObject.FindWithTag("Player").GetComponent<CharController>();

			_minimapCamera = GetComponentInChildren<Camera>();
			_minimapCamera.clearFlags = CameraClearFlags.Depth;
			_minimapCamera.orthographic = true;
			_minimapCamera.depth = 1;
			_minimapCamera.rect = new Rect(50,50, 350, 200);
		}

		private void LateUpdate()
		{
			transform.position = _characterController.transform.position;
		}

		#endregion

		#region Public methods

		public void DrawpMap(List<Room> rooms)
		{
			foreach (Room room in rooms)
			{
				GameObject mapTile = Instantiate(_tilePrefab, transform);
				cellSize = LevelGenerator.Instance.CellSize;
				Vector2 actualPosition = new Vector2(room.GridPosition.x * cellSize.x, room.GridPosition.y * cellSize.y);

				if (room.Dimensions.x < cellSize.x)
					actualPosition.x++;
				if (room.Dimensions.y < cellSize.y)
					actualPosition.y--;

				SpriteRenderer sr = mapTile.GetComponent<SpriteRenderer>();
				sr.sprite = room.MiniMapSprite;
				sr.enabled = false;
				_renderers.Add(sr);
			}
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
		}

		//TODO Maybe some fancy animation stuff?
		private void UnlockRoom(int roomid)
		{
			_renderers[roomid].enabled = true;
		}

		#endregion
	}
}