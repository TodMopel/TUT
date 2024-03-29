using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TodMopel {
    public class GameStateController : MonoBehaviour
    {
		private InputControls inputControls;

		public GameObject[] DeactivateItems;
		public GameObject[] ActivateItems;

		private void Awake()
		{
			SetupGameStateManager();
			SubscribeInputControls();
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

			//if (GameObject.Find("Master Container").GetComponent<MusicManagement>())
				//GameObject.Find("Master Container").GetComponent<MusicManagement>().ChangeLowPassFilterFrequency(newGameState == GameState.Paused);
		}

		IEnumerator ChangeObjectState(bool pauseMenuActive)
		{
			yield return new WaitForEndOfFrame();

			for (int i = 0; i < DeactivateItems.Length; i++) {
				DeactivateItems[i].SetActive(!pauseMenuActive);
			}
			for (int i = 0; i < ActivateItems.Length; i++) {
				ActivateItems[i].SetActive(pauseMenuActive);
			}
		}

		private void SubscribeInputControls()
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

		private void OnEnable()
		{
			SubscribeInputControls();
		}

		private void OnDisable()
		{
			UnsubscribeInputControls();
		}

		private void UnsubscribeInputControls()
		{
			GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
			inputControls.Player.Pause.started -= PauseInput;
			inputControls.Player.Pause.Disable();
		}
	}
}
