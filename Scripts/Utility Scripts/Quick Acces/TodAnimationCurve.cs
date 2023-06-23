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
			return TodUtils.Remap(animationCurve.Evaluate(TodUtils.Remap(value, inputMinimum, inputMaximum, 0, 1)), 0, 1, outputMinimum, outputMaximum);
		}
	}
}
