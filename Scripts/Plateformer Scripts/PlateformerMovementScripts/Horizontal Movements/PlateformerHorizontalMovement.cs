using System;
using UnityEngine;

namespace TodMopel
{
	public class PlateformerHorizontalMovement : MonoBehaviour
	{
		[SerializeField]
		private AvatarController Controller;

		public BoolVariable isFixed, wallGrab;

		public FloatVariable movementSpeed;
		public AnimationCurve AccelerationCurve;
		public FloatVariable groundedAcceleration;
		public FloatVariable airAcceleration;
		public AnimationCurve DecelerationCurve;
		public FloatVariable groundedDeceleration;
		public FloatVariable airDeceleration;

		private TimerClass decelerationTimer = new TimerClass(.5f);
		private Vector2 velocity;

		private void Update()
		{
			ControlTimers();
		}

		private void ControlTimers()
		{
			decelerationTimer.ProcessTimer();
		}

		private void FixedUpdate()
		{
			velocity = Controller.Body.velocity;

			if (HorizontalMovementConditions())
				HorizontalMovementBehaviors();
			else if (!isFixed.value)
				DecelerationBehaviors();

			Controller.Body.velocity = velocity;
		}

		private void DecelerationBehaviors()
		{
			HorizontalMovementStateDecelerationPhase();
			if (HorizontalInputRelease_ImmobileAfterDeceleration())
				FrezeHorizontalVelocity();
		}

		private void HorizontalMovementBehaviors()
		{
			if (HorizontalInputPressed())
				HorizontalMovementStateAccelerationPhase();
			else if (HorizontalInputRelease())
				HorizontalMovementStateDecelerationPhase();
			else if (HorizontalInputRelease_ImmobileAfterDeceleration())
				FrezeHorizontalVelocity();
		}

		private void HorizontalMovementStateAccelerationPhase()
		{
			float acceleration = SelectAccelerationValue();
			float currentAcceleration = AccelerationCurve.Evaluate(GetCurveValue(acceleration));

			Vector2 desiredVelocity = GetHorizontalVelocity();

			decelerationTimer.StartTimer();

			bool changeDirectionOnGround = ((velocity.x > 0 && desiredVelocity.x < 0) || (velocity.x < 0 && desiredVelocity.x > 0)) && Controller.onGround;
			if (changeDirectionOnGround)
				velocity.x = 0;

			velocity.x = AddHorizontalVelocity(desiredVelocity, currentAcceleration);
		}

		private void HorizontalMovementStateDecelerationPhase()
		{
			float deceleration = SelectDecelerationValue();
			float currentSpeed = DecelerationCurve.Evaluate(GetCurveValue(deceleration));

			velocity.x = SlowHorizontalVelocity(currentSpeed);
		}

		private bool HorizontalMovementConditions() => Controller.canMove && !isFixed.value && !wallGrab.value;
		private bool HorizontalInputPressed() => HorizontalInputValue() != 0;
		private bool HorizontalInputRelease_ImmobileAfterDeceleration() => velocity.x == 0;
		private bool HorizontalInputRelease() => HorizontalInputValue() == 0;

		private float AddHorizontalVelocity(Vector2 newVelocity, float acceleration) => Mathf.MoveTowards(velocity.x, newVelocity.x, acceleration);
		private float SlowHorizontalVelocity(float speed) => Mathf.Lerp(velocity.x, 0, speed);
		private void FrezeHorizontalVelocity() => velocity = new Vector2(0, velocity.y);

		private Vector2 GetHorizontalVelocity() => new Vector2(HorizontalInputValue() * movementSpeed.value, Controller.Body.velocity.y);

		private float GetCurveValue(float factor) => factor * Time.deltaTime / 1;

		private float SelectAccelerationValue() => Controller.onGround ? groundedAcceleration.value : airAcceleration.value;
		private float SelectDecelerationValue() => Controller.onGround ? groundedDeceleration.value : airDeceleration.value;

		private float HorizontalInputValue() => Controller.inputArrow;
	}
}