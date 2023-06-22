using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TodMopel
{
	public class NavigationMenuPageScript : MonoBehaviour, IDragHandler, IEndDragHandler
	{
		public BoolVariable interactiveButtonsOptions;
		//[SerializeField]
		private List<RectTransform> MenuPageList;

		public int pageIndex;
		private int newIndex;
		private Vector3 StartPosition;
		private Vector3 directionToPage;

		private bool dragging;

		private void Awake()
		{
			pageIndex = newIndex = 0;
			StartPosition = directionToPage = transform.position;
			SetupMenuPageList();
		}

		private void SetupMenuPageList()
		{
			MenuPageList = new List<RectTransform>();
			int i = interactiveButtonsOptions.value ? 0 : 1;
			foreach (NavigationMenuScript child in transform.GetComponentsInChildren<NavigationMenuScript>()) {
				MenuPageList.Add((RectTransform)child.gameObject.transform);
				if (i > 0) {
					DisableChild(child);
				}
				if (i == 0) {
					StartCoroutine(Selection(child));
				}
				i++;
			}
		}
		IEnumerator Selection(NavigationMenuScript child)
		{
			yield return new WaitForEndOfFrame();
			child.MenuItemList[child.menuIndex].Selection();
		}


		private void Update()
		{
			if (transform.position != directionToPage && !dragging) {
				transform.position += (directionToPage - transform.position) * .1f;
			}
			if (newIndex != pageIndex) {
				DisableChild(MenuPageList[pageIndex].GetComponent<NavigationMenuScript>());
				if (interactiveButtonsOptions.value)
					MenuPageList[newIndex].GetComponent<NavigationMenuScript>().enabled = true;
				pageIndex = newIndex;
			}
		}
		private void DisableChild(NavigationMenuScript child)
		{
			child.enabled = false;
		}

		public void GotoPage(int index)
		{
			if (index < MenuPageList.Count) {
				newIndex = index;
				directionToPage = StartPosition + (MenuPageList[0].position - MenuPageList[newIndex].position);
			}
		}
		public void UpdatePage(int index)
		{
			if (index == -1) {
				GotoPage((pageIndex + 1) % MenuPageList.Count);
			} else if (index == -2) {
				GotoPage(pageIndex == 0 ? MenuPageList.Count - 1 : pageIndex - 1);
			} else if (index > -1) {
				GotoPage(index);
			}
		}

		private float percentThreshold = 0.5f;

		public void OnDrag(PointerEventData eventData)
		{
			dragging = true;
			float difference = (eventData.pressPosition.x - eventData.position.x) / 50;
			transform.position = directionToPage - new Vector3(difference, 0, 0);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			ChangePageOnTreshold(eventData);
			dragging = false;
		}

		private void ChangePageOnTreshold(PointerEventData eventData)
		{
			float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
			if (Mathf.Abs(percentage) >= percentThreshold) {
				if (percentage > 0) {
					GotoPage((pageIndex + 1) % MenuPageList.Count);
				} else if (percentage < 0) {
					GotoPage(pageIndex == 0 ? MenuPageList.Count - 1 : pageIndex - 1);
				}
			}
		}
	}
}