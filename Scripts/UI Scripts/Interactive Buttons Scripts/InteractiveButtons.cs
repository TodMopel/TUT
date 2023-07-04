using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TodMopel
{
	public class InteractiveButtons : MonoBehaviour
	{
		[SerializeField]
		private BoolVariable interactiveButtonsOptions;

		private InputControls inputControls;
		[SerializeField]
		private Image leftButtonImage, rightButtonImage, actionButtonImage;
		[SerializeField]
		private Sprite[] LeftButtonSprites;
		[SerializeField]
		private Sprite[] RightButtonSprites;
		[SerializeField]
		private Sprite[] ActionButtonSprites;

		bool buttonVisible;
		private void Start()
		{
			SubscribeInputControls();
			SetButtonsVisibility();

			leftButtonImage.sprite = LeftButtonSprites[0];
			rightButtonImage.sprite = RightButtonSprites[0];
			actionButtonImage.sprite = ActionButtonSprites[0];
		}

		private void SubscribeInputControls()
		{
			inputControls = new InputControls();

			inputControls.Player.Horizontal.Enable();
			inputControls.Player.Horizontal.started += ArrowInput;
			inputControls.Player.Horizontal.canceled += ArrowInput;

			inputControls.Player.Action.Enable();
			inputControls.Player.Action.started += ActionInput;
			inputControls.Player.Action.canceled += UnactionInput;
			StartCoroutine(FirstClickSecu());
		}

		private void OnEnable()
		{
			SubscribeInputControls();
		}

		private void OnDisable()
		{
			UnsubscribeInputControls();
		}

		private void OnDestroy()
		{
			UnsubscribeInputControls();
		}

		private void UnsubscribeInputControls()
		{
			inputControls.Player.Horizontal.Disable();
			inputControls.Player.Horizontal.started -= ArrowInput;
			inputControls.Player.Horizontal.canceled -= ArrowInput;

			inputControls.Player.Action.Disable();
			inputControls.Player.Action.started -= ActionInput;
			inputControls.Player.Action.canceled -= UnactionInput;
		}


		private void UnactionInput(InputAction.CallbackContext context)
		{
			if (actionButtonImage)
				actionButtonImage.sprite = ActionButtonSprites[0];
		}

		private void ActionInput(InputAction.CallbackContext context)
		{
			if (!firstClick && actionButtonImage)
				actionButtonImage.sprite = ActionButtonSprites[1];
		}

		bool firstClick;
		IEnumerator FirstClickSecu()
		{
			firstClick = true;
			yield return new WaitForSeconds(.3f);
			firstClick = false;
		}

		private void ArrowInput(InputAction.CallbackContext context)
		{
			float arrowValue = context.ReadValue<float>();
			if (arrowValue == 0) {
				leftButtonImage.sprite = LeftButtonSprites[0];
				rightButtonImage.sprite = RightButtonSprites[0];
			} else {
				if (arrowValue < 0) {
					leftButtonImage.sprite = LeftButtonSprites[1];
				} else if (arrowValue > 0) {
					rightButtonImage.sprite = RightButtonSprites[1];
				}
			}
		}

		private void Update()
		{
			if (buttonVisible != interactiveButtonsOptions.value)
				SetButtonsVisibility();
		}

		public void SetButtonsVisibility()
		{
			buttonVisible = interactiveButtonsOptions.value;
			float visible = buttonVisible == true ? 1 : 0;
			Color newColor = new(leftButtonImage.color.r, leftButtonImage.color.g, leftButtonImage.color.b, visible);
			leftButtonImage.color = newColor;
			rightButtonImage.color = newColor;
			actionButtonImage.color = newColor;
		}
	}
}