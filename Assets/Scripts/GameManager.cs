//Kevin Hagen
//25.08.2018

using System;
using Level;
using UnityEngine;
using Utility;

public class GameManager : Singleton<GameManager>
{
	#region GameState enum

	public enum GameState
	{
		Start,
		StartRoom,
		Dungeon,
		BossFight,
		GameOver
	}

	#endregion

	#region Serialize Fields

	[SerializeField] private CharController _character;

	#endregion

	#region Private Fields

	private GameState _state;

	#endregion

	#region Unity methods

	protected override void Awake()
	{
		base.Awake();

		_state = GameState.Start;
	}

	private void Start()
	{
	}

	private void Update()
	{
		switch (_state)
		{
			case GameState.Start:
				//IDLE/StartScreen or stuff like that
				break;
			case GameState.StartRoom:
				//Handle whatever needs to be done in startroom here
				break;
			case GameState.Dungeon:
				break;
			case GameState.BossFight:
				break;
			case GameState.GameOver:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		UpdateState();
	}

	#endregion

	#region Private methods

	private void UpdateState()
	{
		GameState newState = GetNextState();
		if (_state.CompareTo(newState) != 0)
		{
			GameState oldState = _state;
			_state = newState;
			OnStateChange(oldState, newState);
		}
	}

	private GameState GetNextState()
	{
		GameState newState = _state;
		switch (_state)
		{
			case GameState.Start:
				//Should implement startscreen or something here, so that we idle here until the game starts
				return GameState.StartRoom;
			case GameState.StartRoom:
				if (LevelManager.Instance.CurrentRoomID == 0)
					return GameState.Dungeon;
				break;
			case GameState.Dungeon:
				break;
			case GameState.BossFight:
				break;
			case GameState.GameOver:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		return newState;
	}

	private void OnStateChange(GameState oldState, GameState newState)
	{
		switch (oldState)
		{
			case GameState.Start:
				//Close UI or whatever here
				break;
			case GameState.StartRoom:
				break;
			case GameState.Dungeon:
				break;
			case GameState.BossFight:
				break;
			case GameState.GameOver:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		switch (newState)
		{
			case GameState.Start:
				break;
			case GameState.StartRoom:
				//_character.SpawnAt(LevelManager.Instance.StartRoom.SpawnPosition);
				break;
			case GameState.Dungeon:
				break;
			case GameState.BossFight:
				break;
			case GameState.GameOver:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}



    public void Spawn()
    {
        _character.SpawnAt(LevelManager.Instance.StartRoom.SpawnPosition);
    }

	#endregion
}