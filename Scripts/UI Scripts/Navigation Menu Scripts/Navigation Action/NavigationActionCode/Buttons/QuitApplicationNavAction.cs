using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
	[CreateAssetMenu(fileName = "QuitApplication", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/QuitApplication", order = 100)]
	public class QuitApplicationNavAction : NavigationAction
	{
		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			Debug.Log("Quitting App...");
			Application.Quit();
		}
	}
}