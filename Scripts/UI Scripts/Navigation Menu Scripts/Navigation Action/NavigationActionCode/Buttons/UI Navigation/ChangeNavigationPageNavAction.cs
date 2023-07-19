using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "ChangeNavigationPageNavAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/ChangeNavigationPage", order = 0)]
	public class ChangeNavigationPageNavAction : NavigationAction
	{
		public int pageIndex;
		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			NavigationMenuPageScript navPageScript = navItem.transform.parent.transform.parent.gameObject.GetComponent<NavigationMenuPageScript>();
			navPageScript.UpdatePage(pageIndex);
		}
	}
}