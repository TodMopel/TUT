using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "ChangeNavigationNavAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/ChangeNavigation", order = 0)]
	public class ChangeNavigationNavAction : NavigationAction
	{
		public BoolVariable boolVariable;

		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			NavigationMenuScript navMenuScript = navItem.transform.parent.gameObject.GetComponent<NavigationMenuScript>();
			navMenuScript.enabled = boolVariable.value;
		}
	}
}