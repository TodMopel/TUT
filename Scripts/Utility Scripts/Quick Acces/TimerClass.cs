using System;
using UnityEngine;

namespace TodMopel
{
	[Serializable]
	public class TimerClass
	{
		public float timerValue;
		private float timeValue;

		public TimerClass(float timer)
		{
			timerValue = timer;
		}

		public float CurrentValue() => timeValue;

		public void StartTimer()
		{
			timeValue = timerValue;
		}
		public void StopTimer()
		{
			timeValue = -1;
		}
		public float ProcessTimer()
		{
			if (TimerIsRunning())
				timeValue -= Time.deltaTime;
			return timeValue;
		}
		public bool TimerIsRunning()
		{
			return timeValue > 0;
		}
		public bool TimerIsOver()
		{
			return timeValue <= 0;
		}
	}
}
