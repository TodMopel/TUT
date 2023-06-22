using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
	[CreateAssetMenu(fileName = "New ProgressBarValues", menuName = "Tod Unity ToolBox/UI/ProgressBars/Values", order = 1)]
	public class ProgressBarValues : ScriptableObject
	{
		public float minimum;
		public float maximum;
		public FloatVariable current;
	}
}