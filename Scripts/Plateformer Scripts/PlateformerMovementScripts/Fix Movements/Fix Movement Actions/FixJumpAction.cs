using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "FixJumpAction FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/FixJumpAction", order = 3)]
	public class FixJumpAction : AbstractFixMovementAction
	{
		private JumpMovementClass JumpInstance = new JumpMovementClass();
		public TimerClass fixJumpTimer = new TimerClass(1f);
		public FloatVariable fixjumpHeight;

		public StaminaStat staminaStats;

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			//fixMovementComponent = GetFixMovementComponent(FixedObject);
			fixJumpTimer.StartTimer();
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			//fixMovementComponent = GetFixMovementComponent(FixedObject);
			fixJumpTimer.ProcessTimer();
		}
		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			if (fixJumpTimer.TimerIsRunning()) {
				if (staminaStats) {
					if (staminaStats.currentStaminaValue.value > staminaStats.lastStandStaminaValue) {
						staminaStats.currentStaminaValue.value *= .75f;
					}
				}
				JumpAction();
			}
		}

		private void JumpAction()
		{
			float jumpSpeed = JumpInstance.CalculJumpMoveSpeed(fixjumpHeight.value, fixMovementComponent.velocity);

			fixMovementComponent.velocity += Vector2.up * jumpSpeed;
		}
	}
}
