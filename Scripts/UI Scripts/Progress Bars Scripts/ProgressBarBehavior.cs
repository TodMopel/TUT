using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TodMopel
{
	[ExecuteInEditMode()]
	public class ProgressBarBehavior : MonoBehaviour
	{
		public ProgressBarValues progressBarValues;
		public Image bar;

		private void Update()
		{
			UpdateBar();
		}

		private void UpdateBar()
		{
			float currentoffset = progressBarValues.current.value - progressBarValues.minimum;
			float maximumOffset = progressBarValues.maximum - progressBarValues.minimum;
			float fillAmount = currentoffset / maximumOffset;
			bar.fillAmount = fillAmount;
		}
	}
}
