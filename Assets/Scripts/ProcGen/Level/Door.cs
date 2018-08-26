//Kevin Hagen
//26.08.2018

using System.Linq;
using UnityEngine;
using Utility;

namespace ProcGen.Level
{
	public class Door : MonoWrapper
	{
		public delegate void DoorEnteredDelegate(int roomID, Vector2 from);
		public static event DoorEnteredDelegate doorEntered;

		private int _targetRoomID;
		private Vector2 _from;
		private SpriteRenderer[] _sprites;
		private BoxCollider2D _physicssCollider2D;

		private void Awake()
		{
			_sprites = GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer sprite in _sprites)
			{
				sprite.enabled = false;
			}

			_physicssCollider2D = GetComponents<BoxCollider2D>().First(col => !col.isTrigger);
			_physicssCollider2D.enabled = false;
		}

		public void Init(int targetRoomID, Vector2 positionInRoom)
		{
			_targetRoomID = targetRoomID;
			_from = -1 * positionInRoom;
		}

		public void Lock()
		{
			_targetRoomID = -1;
			_from = Vector2.positiveInfinity;
			foreach (SpriteRenderer sprite in _sprites)
			{
				sprite.enabled = true;
			}
			_physicssCollider2D.enabled = true;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				doorEntered?.Invoke(_targetRoomID, _from);
			}
		}
	}
}
