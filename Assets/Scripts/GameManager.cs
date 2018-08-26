//Kevin Hagen
//25.08.2018

using System;
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

	private void Update()
	{
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

	private void OnStateChange(GameState oldState, GameState newState)
	{
		switch (oldState)
		{
			case GameState.Start:
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

	private GameState GetNextState()
	{
		GameState newState = _state;
		switch (_state)
		{
			case GameState.Start:
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

		return newState;
	}

	#endregion
}