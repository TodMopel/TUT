using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TodMopel
{
	public class TextInputListner : MonoBehaviour
	{
		public StringVariables stringVariable;

		private SelectableMenuItem focused;
		private TMP_InputField field;
		bool itemFocused => focused.enabled;
		bool inputFocused;

		private void Start()
		{
			focused = GetComponent<SelectableMenuItem>();
			field = GetComponent<TMP_InputField>();
			field.text = stringVariable.value;
		}

		private void Update()
		{
			if (itemFocused) {
				if (!inputFocused) {
					StartCoroutine(SelectInputField());
				}
			}
		}

		public IEnumerator SelectInputField()
		{
			yield return new WaitForEndOfFrame();
			field.Select();
			OpenMobileKeyboard();
			inputFocused = true;
			focused.enabled = true;
		}

		public void UnselectInputField()
		{
			inputFocused = false;
			focused.enabled = false;
			GetComponent<NavigationMenuItem>().OnUnselectAction();
		}

		private static void OpenMobileKeyboard()
		{
			TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
			Debug.Log("Open Mobile Keyboard");
		}

		public void SetStringValue()
		{
			stringVariable.value = field.text;
		}
	}
}