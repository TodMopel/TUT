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
		}

		public void OnSelectionAction()
		{
			Selection();
		}
		internal void Selection()
		{
			MenuItemSpriteRenderer.sprite = SelectedMenuItemSprite[0];
			for (int i = 0; i < navActions.Length; i++) {
				navActions[i].OnSelectionDo(gameObject);
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