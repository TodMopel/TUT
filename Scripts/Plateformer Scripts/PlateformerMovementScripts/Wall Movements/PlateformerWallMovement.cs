using System;
using UnityEngine;

namespace TodMopel {
	public class PlateformerWallMovement : MonoBehaviour
	{
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private AvatarController Controller;
		private JumpMovementClass JumpInstance = new JumpMovementClass();

		public BoolVariable wallGrab, isFixed;

        public FloatVariable wallGravityScale;
        public FloatVariable wallMovementSpeed;
        public FloatVariable wallAcceleration;
        public FloatVariable wallJumpHeight;

		public TimerClass wallCoyoteTimer = new TimerClass(.1f);
		public TimerClass wallJumpBufferTimer = new TimerClass(.1f);

        private Vector2 velocity;
        private float wallCheckDistance = .01f;
        private float avatarHeight = 2f;
        private int numberOfRays = 12;
        public bool wallGrabLedge;

        public Vector2 wallSide;

        private void Start()
		{
            wallGrab.value = false;
        }

		private void Update()
		{
            wallCoyoteTimer.ProcessTimer();
            wallJumpBufferTimer.ProcessTimer();
            if (ActionInput() && wallGrab.value) {
                wallJumpBufferTimer.StartTimer();
            }
        }

		private void FixedUpdate()
		{
			velocity = Controller.Body.velocity;

            if (!isFixed.value && !Controller.onGround && Controller.canMove) {
                if (WallCheck() && !wallGrab.value && velocity.y <= 2) {
                    wallGrab.value = true;
                    velocity = Vector2.zero;
                    Controller.Body.gravityScale = wallGravityScale.value;
				}
            }

            if (wallGrab.value) {
				if (WallRunConditions())
					WallRunAction();
                else if (velocity.y > 0)
                    velocity = Vector2.zero;
            }
			if (WallJumpConditions())
				WallJumpAction();

			if ((!WallCheck() || Controller.onGround) && wallGrab.value || !Controller.canMove) {
                wallGrab.value = false;
                wallCoyoteTimer.StartTimer();
            }

            Controller.Body.velocity = velocity;
		}

		private void WallRunAction()
		{
            float maxSpeedChange = wallAcceleration.value * Time.deltaTime;
            velocity.y = wallGrabLedge ? velocity.y+JumpInstance.CalculJumpMoveSpeed(wallJumpHeight.value, velocity) : Mathf.MoveTowards(velocity.y, wallMovementSpeed.value, maxSpeedChange);
		}

		private void WallJumpAction()
		{
            float jumpSpeed = JumpInstance.CalculJumpMoveSpeed(wallJumpHeight.value, velocity);
            velocity.y += jumpSpeed;
            wallJumpBufferTimer.StopTimer();
        }

		private bool WallCheck()
        {
            bool[] boolArray = new bool[numberOfRays];
            Vector2 wallSideCheck = !spriteRenderer.flipX ? Vector2.right : Vector2.left;
            Vector2 origin = transform.position;
            origin.y += avatarHeight / 2;
            float _offsetStep = avatarHeight / numberOfRays;
            for (int i = 0; i < numberOfRays; i++) {
                RaycastHit2D _Ray = Physics2D.Raycast(origin + (wallSideCheck * .4f), wallSideCheck, wallCheckDistance, Controller.PlateformLayer);

                Debug.DrawRay(origin + (wallSideCheck * .4f), wallSideCheck * wallCheckDistance, Color.white);
                if (_Ray && _Ray.collider.CompareTag(Controller.PlateformTag)) {
                    boolArray[i] = true;
                }
                origin.y -= _offsetStep;
            }

            wallGrabLedge = WallCheckLedge(boolArray);

			int index = 0;
            for (int i = 0; i < numberOfRays; i++) {
                if (boolArray[i] == true) {
                    index += 1;
                }
                if (index > numberOfRays - (numberOfRays / 3)) {
                    wallSide = wallSideCheck;
                    return true;
                }
            }
            return false;
        }
        private bool WallCheckLedge(bool[] boolArray)
        {
            if (boolArray[0] == false && boolArray[numberOfRays - 1] == true) {
                return true;
            }
            return false;
        }

        private bool WallRunConditions()
        {
            bool sameSide = HorizontalInputValue() != 0 && HorizontalInputValue() == wallSide.x;
            return sameSide;
        }
        private bool WallJumpConditions()
        {
            bool oppositeSide = HorizontalInputValue() != 0 && HorizontalInputValue() != wallSide.x;
            bool timerCheck = wallCoyoteTimer.TimerIsRunning() && wallJumpBufferTimer.TimerIsRunning();
            return oppositeSide && timerCheck;
        }
		private bool ActionInput() => Controller.inputActionStart;
        private float HorizontalInputValue() => Controller.inputArrow;
    }
}
