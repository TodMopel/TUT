using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public static class TodUtils
	{
		public static float Remap(float fromValue, float fromMin, float fromMax, float toMin, float toMax)
		{
			var fromAbs = fromValue - fromMin;
			var fromMaxAbs = fromMax - fromMin;

			var normalValue = fromAbs / fromMaxAbs;

			var toMaxAbs = toMax - toMin;
			var toAbs = toMaxAbs * normalValue;

			var desiredValue = toAbs + toMin;

			return desiredValue;
		}
	}
}
