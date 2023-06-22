using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[Serializable]
	public class TodAnimationCurve
	{
		public AnimationCurve animationCurve;
		public float inputMinimum = 0, inputMaximum = 1;
		public float outputMinimum = 0, outputMaximum = 1;


		internal float GetValue(float value)
		{
			return Remap(animationCurve.Evaluate(Remap(value, inputMinimum, inputMaximum, 0, 1)), 0, 1, outputMinimum, outputMaximum);
		}

		internal float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
		{
			float fromAbs = from - fromMin;
			float fromMaxAbs = fromMax - fromMin;

			float normalValue = fromAbs / fromMaxAbs;

			float toMaxAbs = toMax - toMin;
			float toAbs = toMaxAbs * normalValue;

			float desiredValue = toAbs + toMin;

			return desiredValue;
		}

	}
}
