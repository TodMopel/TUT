using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "PauseInGameNavAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/PauseInGame", order = 9)]
	public class PauseInGameNavAction : NavigationAction
	{
		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			GameState currentGameState = GameStateManager.Instance.CurrentGameState;
			GameState newGameState = currentGameState == GameState.InGame ? GameState.Paused : GameState.InGame;
			GameStateManager.Instance.SetState(newGameState);
		}
	}
}