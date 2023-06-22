using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class OnPauseSaveSystem : MonoBehaviour
	{
		public VariableListSaveSystem variableList;

		private void Awake()
		{
			SaveManager.Init();
			GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
		}
		private void OnGameStateChanged(GameState newGameState)
		{
			bool save = newGameState == GameState.InGame;
			if (save)
				variableList.SaveMyObject();
			bool load = newGameState == GameState.Paused;
			if (load)
				variableList.LoadMyObject();
		}
	}
}
