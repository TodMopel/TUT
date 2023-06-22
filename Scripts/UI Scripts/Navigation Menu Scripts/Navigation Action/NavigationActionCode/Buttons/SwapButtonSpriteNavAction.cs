using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "SwapButtonSpriteNavAction NavigationAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/SwapButtonSpriteNavAction", order = 16)]
	public class SwapButtonSpriteNavAction : NavigationAction
	{

		[SerializeField]
		private Sprite SelectedMenuItemSprite, PressedMenuItemSprite;

		public override void OnSelectionDo(GameObject navItem)
		{
			Button ButtonRenderer = navItem.GetComponent<Button>();
			ButtonRenderer.targetGraphic.GetComponent<Image>().sprite = SelectedMenuItemSprite;
		}
		public override void OnUnselectionDo(GameObject navItem)
		{
			Button ButtonRenderer = navItem.GetComponent<Button>();
			ButtonRenderer.targetGraphic.GetComponent<Image>().sprite = SelectedMenuItemSprite;
		}
		public override void OnActionnDo(GameObject navItem)
		{
			Button ButtonRenderer = navItem.GetComponent<Button>();
			ButtonRenderer.targetGraphic.GetComponent<Image>().sprite = PressedMenuItemSprite;
		}
	}
}