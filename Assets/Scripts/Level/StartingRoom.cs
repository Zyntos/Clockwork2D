//Kevin Hagen
//26.08.2018

using ProcGen.Level;
using UnityEngine;

namespace Level
{
	public class StartingRoom : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private Door _doorToFirstRoom;
		[SerializeField] private Transform[] _corners;

		#endregion

		#region Properties

		public Door DungeonEntry => _doorToFirstRoom;
		public Vector3 SpawnPosition => _spawnPoint.position;
		public Transform[] Corners => _corners;

		#endregion

		#region Public methods

		public void Setup()
		{
			_doorToFirstRoom.Init(0, Vector2.right);
		}

		#endregion
	}
}