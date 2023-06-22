using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "FocusItemNavAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/FocusItemNavAction", order = 2)]
	public class FocusItemNavAction : NavigationAction
	{
		public BoolVariable itemFocusedOption;

		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
			itemFocusedOption.value = false;
			NavigationMenuScript navMenuScript = navItem.transform.parent.gameObject.GetComponent<NavigationMenuScript>();
			SelectableMenuItem selectableMenuItem = navItem.GetComponent<SelectableMenuItem>();
			if (navMenuScript.interactiveButtonsOption.value && selectableMenuItem.iAmFocused) {
				navMenuScript.enabled = true;
			}
		}
		public override void OnActionnDo(GameObject navItem)
		{
			itemFocusedOption.value = !itemFocusedOption.value;

			NavigationMenuScript navMenuScript = navItem.transform.parent.gameObject.GetComponent<NavigationMenuScript>();
			navMenuScript.enabled = !itemFocusedOption.value;

			SelectableMenuItem selectableMenuItem = navItem.GetComponent<SelectableMenuItem>();
			selectableMenuItem.iAmFocused = itemFocusedOption.value;
			selectableMenuItem.enabled = itemFocusedOption.value;
		}
	}
}
