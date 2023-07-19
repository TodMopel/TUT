using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "UISelectionSoundNavAction NavigationAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/UISelectionSoundNavAction", order = 0)]
	public class UISelectionSoundNavAction : NavigationAction
	{
		public string selectionSoundName, actionSoundName;
		public override void OnSelectionDo(GameObject navItem)
		{
			AudioManager audioManager = FindObjectOfType<AudioManager>();
			if (audioManager)
				audioManager.PlaySound(selectionSoundName);
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
		}
		public override void OnActionnDo(GameObject navItem)
		{
			AudioManager audioManager = FindObjectOfType<AudioManager>();
			if (audioManager)
				audioManager.PlaySound(actionSoundName);
		}
	}
}