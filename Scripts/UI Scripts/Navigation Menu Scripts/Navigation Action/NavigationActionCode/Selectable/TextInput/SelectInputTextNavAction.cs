using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "SelectInputTextNavAction NavigationAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/SelectInputText", order = 13)]
	public class SelectInputTextNavAction : NavigationAction
	{
		public BoolVariable itemFocusedOption;
		private TMP_InputField inputField;

		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			inputField = navItem.GetComponent<TMP_InputField>();
			if (itemFocusedOption.value) {
				inputField.Select();
				TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
				Debug.Log("Open Keyboard");
			}
		}
	}
}