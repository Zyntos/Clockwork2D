//Kevin Hagen
//26.08.2018

using ProcGen.Level;
using UnityEngine;

namespace Level
{
	public class StartingRoom : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private CharController _character;
		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private Door _doorToFirstRoom;

		#endregion

		#region Unity methods

		private void Start()
		{
			_character.transform.position = _spawnPoint.position;
			_doorToFirstRoom.Init(0, Vector2.right);
		}

		#endregion
	}
}