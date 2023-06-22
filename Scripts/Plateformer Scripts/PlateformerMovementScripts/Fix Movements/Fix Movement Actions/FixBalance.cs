using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "FixBalance FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/FixBalance", order = -10)]
	public class FixBalance : AbstractFixMovementAction
	{
		public BoolVariable balanceActive;
		public TimerClass balanceTimer = new(3f);

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			balanceTimer.StartTimer();
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			balanceTimer.ProcessTimer();
			if (balanceTimer.TimerIsOver() && balanceActive.value)
				fixMovementComponent.canStayFix = false;
		}
		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
	}
}