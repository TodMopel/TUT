using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TodMopel
{
	public class ToggleListner : MonoBehaviour
	{
		public GameObject toggleCheck;
		public BoolVariable boolVariable;

		private void Start()
		{
			SetToggleOnValue();
		}

		private void Update()
		{
			if (toggleCheck.activeSelf != boolVariable.value)
				SetToggleOnValue();
		}

		private void SetToggleOnValue()
		{
			toggleCheck.SetActive(boolVariable.value);
		}
	}
}
