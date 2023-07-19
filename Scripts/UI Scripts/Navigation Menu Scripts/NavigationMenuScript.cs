using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;

namespace TodMopel
{
	public class NavigationMenuScript : MonoBehaviour
	{
		public BoolVariable interactiveButtonsOption, itemFocusedOption;
		private InputControls inputControls;

		[HideInInspector]
		public List<NavigationMenuItem> MenuItemList;
		[HideInInspector]
		public int menuIndex = 0;
		private TimerClass inputHoldTimer = new TimerClass(.3f);

		private void Awake()
		{
			inputControls = new InputControls();
			SetupMenuItemList();
		}

		private void SubscribeInputControls()
		{
			inputControls.UI_Menu.Horizontal.Enable();
			inputControls.UI_Menu.Horizontal.started += ArrowInput;
			inputControls.UI_Menu.Horizontal.canceled += ArrowInput;

			inputControls.UI_Menu.Action.Enable();
			inputControls.UI_Menu.Action.started += ActionInput;
			inputControls.UI_Menu.Action.canceled += UnactionInput;

			StartCoroutine(FirstClickSecu());
		}
		private void OnDestroy()
		{
			UnsubscribeInputControls();
		}

		private void UnsubscribeInputControls()
		{
			inputControls.UI_Menu.Horizontal.Disable();
			inputControls.UI_Menu.Horizontal.started -= ArrowInput;
			inputControls.UI_Menu.Horizontal.canceled -= ArrowInput;

			inputControls.UI_Menu.Action.Disable();
			inputControls.UI_Menu.Action.started -= ActionInput;
			inputControls.UI_Menu.Action.canceled -= UnactionInput;
		}

		private void OnEnable()
		{
			SubscribeInputControls();
			itemFocusedOption.value = false;
			MenuItemList[menuIndex].Selection();
		}

		private void OnDisable()
		{
			UnsubscribeInputControls();
			if (!itemFocusedOption.value && MenuItemList[menuIndex])
				MenuItemList[menuIndex].Unselection();
		}

		private void UnactionInput(InputAction.CallbackContext context)
		{
			if (!itemFocusedOption.value)
				MenuItemList[menuIndex].Selection();
		}

		private void ActionInput(InputAction.CallbackContext context)
		{
			if (!firstClick && MenuItemList[menuIndex])
				MenuItemList[menuIndex].Action();
		}

		private bool firstClick;
		private IEnumerator FirstClickSecu()
		{
			firstClick = true;
			yield return new WaitForSeconds(.3f);
			firstClick = false;
		}

		float arrowValue;
		public void ArrowInput(InputAction.CallbackContext context)
		{
			arrowValue = context.ReadValue<float>();
		}

		private void SetupMenuItemList()
		{
			MenuItemList = new List<NavigationMenuItem>();
			foreach (NavigationMenuItem child in transform.GetComponentsInChildren<NavigationMenuItem>()) {
				MenuItemList.Add(child.gameObject.GetComponent<NavigationMenuItem>());
			}
		}

		private void Update()
		{
			ArrowNavigationSystem();
		}

		private void ArrowNavigationSystem()
		{
			inputHoldTimer.ProcessTimer();
			if (arrowValue == 0) {
				inputHoldTimer.StopTimer();
			} else {
				if (arrowValue < 0 && inputHoldTimer.TimerIsOver()) {
					inputHoldTimer.StartTimer();
					SelectPreviousMenuItem();
				} else if (arrowValue > 0 && inputHoldTimer.TimerIsOver()) {
					inputHoldTimer.StartTimer();
					SelectNextMenuItem();
				}
			}
		}

		public void SelectNextMenuItem()
		{
			MenuItemList[menuIndex].Unselection();
			menuIndex += 1;
			MenuItemList[menuIndex %= MenuItemList.Count].Selection();
		}

		public void SelectPreviousMenuItem()
		{
			MenuItemList[menuIndex].Unselection();
			bool menuIndexWillBeNegative = menuIndex == 0;
			if (menuIndexWillBeNegative)
				menuIndex = MenuItemList.Count - 1;
			else
				menuIndex -= 1;
			MenuItemList[menuIndex].Selection();
		}
	}
}