using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TodMopel
{
	public class SliderListner : MonoBehaviour
	{
		public FloatVariable floatValue;
		private SelectableMenuItem focused;
		private Slider slider;

		private const string TextFormat = "###.##";
		public TMP_Text textValue;
		public float step = 0.01f;
		float stepValue;
		int stepCounter;

		private void Awake()
		{
			focused = GetComponent<SelectableMenuItem>();
			slider = GetComponent<Slider>();
			SetSliderValue();
		}

		bool itemFocused => focused.enabled && focused.iAmFocused;
		float arrowValue => focused.arrowValue;
		TimerClass inputTimer = new(.25f);

		public void Update()
		{
			inputTimer.ProcessTimer();
			if (itemFocused) {
				if (arrowValue == 0) {
					stepValue = step;
					stepCounter = 0;
				}
				else if (arrowValue != 0 && inputTimer.TimerIsOver()) {
					inputTimer.StartTimer();
					floatValue.value += stepValue * arrowValue;
					stepCounter += 1;
					if (stepCounter == 20) {
						stepValue *= 10;
						Mathf.Round(stepValue);
					}
				}
			}
			if (floatValue.value.ToString(TextFormat) != textValue.text)
				SetSliderValue();
		}

		public void OnSliderChanged(float value)
		{
			floatValue.value = value;
		}

		private void SetSliderValue()
		{
			slider.value = floatValue.value;
			textValue.text = floatValue.value.ToString(TextFormat);
		}

	}
}
