using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TodMopel
{
	public class AvatarController : MonoBehaviour
	{
		private InputControls inputControls;

		public LayerMask PlateformLayer;
		public string PlateformTag;
		public Rigidbody2D Body;
		public Collider2D Collider;

		[HideInInspector]
		public bool inputActionStart, inputAction;
		[HideInInspector]
		public float inputArrow;
		//[HideInInspector]
		public bool canMove, onGround, paused;
		public bool autoRun;

		private void Awake()
		{
			SetupGameStateManager();
			SubscribeInputControls();
			if (autoRun)
				inputArrow = 1;
		}

		private void SetupGameStateManager()
		{
			GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
		}

		private void OnGameStateChanged(GameState newGameState)
		{
			canMove = newGameState == GameState.InGame;
			Body.simulated = newGameState == GameState.InGame;

			paused = newGameState == GameState.Paused;
		}

		private void SubscribeInputControls()
		{
			inputControls = new InputControls();

			inputControls.Player.Horizontal.Enable();
			inputControls.Player.Horizontal.started += ArrowInput;
			inputControls.Player.Horizontal.canceled += ArrowInput;

			inputControls.Player.Action.Enable();
			inputControls.Player.Action.started += ActionInputDown;
			inputControls.Player.Action.canceled += ActionInputUp;

			StartCoroutine(FirstClickSecu());
		}

		private void ArrowInput(InputAction.CallbackContext context)
		{
			if (CurrentGameStateInGame() && !autoRun) {
				inputArrow = context.ReadValue<float>();
			}
		}

		bool firstClick;
		IEnumerator FirstClickSecu()
		{
			firstClick = true;
			yield return new WaitForSeconds(.3f);
			firstClick = false;
		}
		private void ActionInputDown(InputAction.CallbackContext context)
		{
			if (!firstClick) {
				if (CurrentGameStateInGame()) {
					StartCoroutine(InputActionPressed());
				}
			}
		}
		IEnumerator InputActionPressed()
		{
			inputActionStart = true;
			yield return new WaitForEndOfFrame();
			inputActionStart = false;
			inputAction = true;
		}

		private void ActionInputUp(InputAction.CallbackContext context)
		{
			if (CurrentGameStateInGame()) {
				inputAction = false;
			}
		}

		private static bool CurrentGameStateInGame()
		{
			return GameStateManager.Instance.CurrentGameState == GameState.InGame;
		}

		private void OnDestroy()
		{
			GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
			ClearInputControls();
		}

		private void ClearInputControls()
		{
			inputControls.Player.Horizontal.started -= ArrowInput;
			inputControls.Player.Horizontal.canceled -= ArrowInput;
			inputControls.Player.Action.started -= ActionInputDown;
			inputControls.Player.Action.canceled -= ActionInputUp;
		}
	}
}
