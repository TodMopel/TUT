using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TodMopel {
	public class SelectableMenuItem : MonoBehaviour
	{
		private InputControls inputControls;
		public BoolVariable itemFocusedOption;
		public bool iAmFocused;

		private void Awake()
		{
			itemFocusedOption.value = false;
			inputControls = new InputControls();
			SubscribeInputControls();
		}

		private void SubscribeInputControls()
		{
			inputControls.UI_Selected.Horizontal.Enable();
			inputControls.UI_Selected.Horizontal.started += ArrowInput;
			inputControls.UI_Selected.Horizontal.canceled += ArrowInput;

			inputControls.UI_Selected.Action.Enable();
			inputControls.UI_Selected.Action.started += ActionInput;
			inputControls.UI_Selected.Action.canceled += UnactionInput;

			StartCoroutine(FirstClickSecu());
		}
		private void OnDestroy()
		{
			UnsubscribeInputControls();
		}

		private void UnsubscribeInputControls()
		{
			inputControls.UI_Selected.Horizontal.started -= ArrowInput;
			inputControls.UI_Selected.Horizontal.canceled -= ArrowInput;
			inputControls.UI_Selected.Horizontal.Disable();

			inputControls.UI_Selected.Action.started -= ActionInput;
			inputControls.UI_Selected.Action.canceled -= UnactionInput;
			inputControls.UI_Selected.Action.Disable();
		}

		private void OnEnable()
		{
			SubscribeInputControls();
		}

		private void OnDisable()
		{
			UnsubscribeInputControls();
			if (!itemFocusedOption.value) {

			}
		}

		private void UnactionInput(InputAction.CallbackContext context)
		{
			if (!itemFocusedOption.value) {

			}
		}

		private void ActionInput(InputAction.CallbackContext context)
		{
			if (!firstClick) {
				if (iAmFocused && itemFocusedOption.value)
					GetComponent<NavigationMenuItem>().Action();
			}
		}

		bool firstClick;
		IEnumerator FirstClickSecu()
		{
			firstClick = true;
			yield return new WaitForSeconds(.3f);
			firstClick = false;
		}

		[HideInInspector]
		public float arrowValue;
		public void ArrowInput(InputAction.CallbackContext context)
		{
			arrowValue = context.ReadValue<float>();
		}
	}
}
