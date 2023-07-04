using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TodMopel
{
	public class FloatValueListner : MonoBehaviour
	{
		public FloatVariable floatValue;
		public TMP_Text textValue;

		public string floatFormat = "###";

		private float reminder;

		private void Update()
		{
			if (reminder != floatValue.value)
				SetValue();
		}

		private void SetValue()
		{
			textValue.text = floatValue.value.ToString(floatFormat);
			reminder = floatValue.value;
			Debug.Log("coucou");
		}
	}
}
