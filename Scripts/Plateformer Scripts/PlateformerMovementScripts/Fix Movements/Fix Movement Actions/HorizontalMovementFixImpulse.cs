using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "HorizontalMovementFixImpulse FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/HorizontalMovementFixImpulse", order = 20)]
	public class HorizontalMovementFixImpulse : AbstractFixMovementAction
	{
		public AnimationCurve FixAccelerationCurve;
		public FloatVariable fixSpeedValue;
		public FloatVariable fixAccelerationValue;
		[Tooltip("Set à 0 pour infini")]
		public TimerClass fixImpulseTimer = new TimerClass(3f);

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			if (fixImpulseTimer.timerValue > 0)
				fixImpulseTimer.StartTimer();
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			fixImpulseTimer.ProcessTimer();
			bool fixInpulseConditions = fixImpulseTimer.TimerIsRunning() && fixMovementComponent.HorizontalInputValue() != 0;
			if (fixInpulseConditions || fixImpulseTimer.timerValue == 0) {
				float fixAcceleration = FixAccelerationCurve.Evaluate(CalculSpeedValue(fixAccelerationValue.value));
				float desiredDirection = fixMovementComponent.HorizontalInputValue();
				Vector2 forwardFixDirection = fixMovementComponent.CollectForwardFixDirection();
				Vector2 motionDirection = forwardFixDirection * desiredDirection * fixAcceleration * fixSpeedValue.value;

				fixMovementComponent.velocity += motionDirection;
			}
		}
		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
		private float CalculSpeedValue(float factor) => factor * Time.deltaTime / 1;
	}
}
