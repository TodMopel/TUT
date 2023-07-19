using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "DeleteAudioManagerNavAction NavigationAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/DeleteAudioManagerNavAction", order = 22)]
	public class DeleteAudioManagerNavAction : NavigationAction
	{
		public override void OnSelectionDo(GameObject navItem)
		{
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			Destroy(FindObjectOfType<AudioManager>().gameObject);
		}
	}
}