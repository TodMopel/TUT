using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TodMopel {
    public class GameStateController : MonoBehaviour
    {
		private InputControls inputControls;

		public GameObject PauseMenu, InGameHUD;

		private void Awake()
		{
			SetupGameStateManager();
			SetupInputControls();
			StartCoroutine(ChangeObjectState(GameStateManager.Instance.CurrentGameState == GameState.Paused));
		}

		private void SetupGameStateManager()
		{
			GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
			GameStateManager.Instance.SetState(GameState.InGame);
		}
		private void OnGameStateChanged(GameState newGameState)
		{
			bool pauseMenuActive = newGameState == GameState.Paused ? true : false;
			StartCoroutine(ChangeObjectState(pauseMenuActive));
		}

		IEnumerator ChangeObjectState(bool pauseMenuActive)
		{
			yield return new WaitForEndOfFrame();
			PauseMenu.SetActive(pauseMenuActive);
			InGameHUD.SetActive(!pauseMenuActive);
		}

		private void SetupInputControls()
		{
			inputControls = new InputControls();

			inputControls.Player.Pause.Enable();
			inputControls.Player.Pause.started += PauseInput;
		}

		private void PauseInput(InputAction.CallbackContext obj)
		{
			GameState currentGameState = GameStateManager.Instance.CurrentGameState;
			GameState newGameState = currentGameState == GameState.InGame ? GameState.Paused : GameState.InGame;
			GameStateManager.Instance.SetState(newGameState);
		}

		private void OnDestroy()
		{
			GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
			inputControls.Player.Pause.started -= PauseInput;
		}
	}
}
