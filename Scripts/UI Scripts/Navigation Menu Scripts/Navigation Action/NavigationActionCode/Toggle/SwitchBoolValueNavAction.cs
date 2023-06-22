using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "SwitchBoolValueNavAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/SwitchBoolValueNavAction", order = 12)]
	public class SwitchBoolValueNavAction : NavigationAction
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
			boolVariable.value = !boolVariable.value;
		}
	}
}
