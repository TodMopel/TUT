using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class OneButtonJumpMovement : MonoBehaviour
	{
		[SerializeField] private AvatarController Controller;
		private JumpMovementClass JumpInstance = new JumpMovementClass();

		public FloatVariable jumpHeight;
		public FloatVariable jumpGravityScale;
		public FloatVariable fallingGravity;
		public FloatVariable maxFallingSpeed;

		public TimerClass coyoteTimer = new TimerClass(.1f);
		public TimerClass jumpBufferTimer = new TimerClass(.1f);

		private float defaultGravityScale = 1f;
		private Vector2 velocity;

		private bool canJump;
		public BoolVariable hasJump, isFixed, fixExit, wallGrab;

		private void Update()
		{
			ControlTimers();
			if (ActionInput())
				jumpBufferTimer.StartTimer();
			if (IsGrounded()) {
				coyoteTimer.StartTimer();
				canJump = true;
			}
		}

		private void FixedUpdate()
		{
			velocity = Controller.Body.velocity;

			if (JumpConditions())
				JumpAction();

			if (ContinuousJumpConditions())
				ApplyJumpGravityScale();
			else if (EndJumpConditions()) {
				hasJump.value = true;
				ApplFallingGravityScale();
			} else if (Controller.Body.velocity.y == 0 && StatesController())
				ApplyDefaultGravityScale();

			ControlMaxFallingSpeed();
			ControlDownSideForOneButtonMecanic();

			Controller.Body.velocity = velocity;
		}

		private void JumpAction()
		{
			jumpBufferTimer.StopTimer();
			coyoteTimer.StopTimer();
			canJump = false;

			float jumpSpeed = JumpInstance.CalculJumpMoveSpeed(jumpHeight.value, velocity);

			velocity.y += jumpSpeed;
		}

		private void ControlMaxFallingSpeed()
		{
			if (velocity.y < -maxFallingSpeed.value) {
				velocity = new Vector2(velocity.x, -maxFallingSpeed.value);
			}
		}
		private void ControlDownSideForOneButtonMecanic()
		{
			RaycastHit2D TestDownwardPlateforms = Physics2D.CircleCast(transform.position, .75f, transform.position * Vector2.down, -1.5f, Controller.PlateformLayer);

			if (coyoteTimer.TimerIsRunning() || (Controller.Body.gravityScale == jumpGravityScale.value && !ActionHoldInput()) || (TestDownwardPlateforms && TestDownwardPlateforms.collider.CompareTag(Controller.PlateformTag)))
				hasJump.value = false;
		}

		private bool JumpConditions() => Controller.canMove && canJump && !hasJump.value && jumpBufferTimer.TimerIsRunning() && coyoteTimer.TimerIsRunning() && StatesController();
		private bool ContinuousJumpConditions() => Controller.canMove && ActionHoldInput() && Controller.Body.velocity.y > 0 && StatesController();
		private void ApplyJumpGravityScale() => Controller.Body.gravityScale = jumpGravityScale.value;

		private bool EndJumpConditions() => (!ActionHoldInput() || Controller.Body.velocity.y < 0) && StatesController();
		private void ApplFallingGravityScale() => Controller.Body.gravityScale = fallingGravity.value;
		private void ApplyDefaultGravityScale() => Controller.Body.gravityScale = defaultGravityScale;

		private bool IsGrounded() => Controller.onGround;
		private bool StatesController() => !isFixed.value && !fixExit.value && !wallGrab.value;

		private bool ActionInput() => Controller.inputActionStart;
		private bool ActionHoldInput() => Controller.inputAction;

		private void ControlTimers()
		{
			coyoteTimer.ProcessTimer();
			jumpBufferTimer.ProcessTimer();
		}
	}
}