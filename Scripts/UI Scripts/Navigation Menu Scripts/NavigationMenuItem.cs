using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace TodMopel
{
	[ExecuteInEditMode]
	public class NavigationMenuItem : MonoBehaviour
	{
		[SerializeField]
		private NavigationAction[] navActions;

		[SerializeField]
		private Image MenuItemSpriteRenderer;
		[SerializeField]
		private Sprite[] UnselectedMenuItemSprite, SelectedMenuItemSprite, PressedMenuItemSprite;
		[HideInInspector]
		public bool selected;

		public void OnUnselectAction()
		{
			Unselection();
		}
		internal void Unselection()
		{
			MenuItemSpriteRenderer.sprite = UnselectedMenuItemSprite[0];
			for (int i = 0; i < navActions.Length; i++) {
				navActions[i].OnUnselectionDo(gameObject);
			}
			selected = false;
		}

		public void OnSelectionAction()
		{
			Selection();
		}
		internal void Selection()
		{
			if (!selected) {
				MenuItemSpriteRenderer.sprite = SelectedMenuItemSprite[0];
			
				for (int i = 0; i < navActions.Length; i++) {
					navActions[i].OnSelectionDo(gameObject);
				}
				selected = true;
			}
		}

		public void OnClickAction()
		{
			Action();
			Unselection();
		}
		internal void Action()
		{
			MenuItemSpriteRenderer.sprite = PressedMenuItemSprite[0];
			for (int i = 0; i < navActions.Length; i++) {
				navActions[i].OnActionnDo(gameObject);
			}
		}
	}
}