using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "OpenSceneNavAction NavigationAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/OpenScene", order = 20)]
	public class OpenSceneNavAction : NavigationAction
	{
		public string sceneName;
		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}