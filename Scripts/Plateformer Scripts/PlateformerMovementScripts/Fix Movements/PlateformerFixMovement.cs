using System;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class PlateformerFixMovement : MonoBehaviour
	{
		public AvatarController Controller;

		public GameObject FixHangObject;
		public GameObject FixIndicatorObject;
		[SerializeField] private GameObject FixHoldObject;

		[SerializeField] private AbstractFixAiming FixAimingModule;
		[SerializeField] private AbstractFixMovementAction[] FixActionsModule;

		private FixHangPointBehaviors fixHangPointComponent;

		private Queue<GameObject> FixPointsQueue = new Queue<GameObject>();

		public FloatVariable maxAimingLength;
		[HideInInspector] public float minAimingLength = 1;
		public FloatVariable fixGravityScale;
		public FloatVariable fixExitGravityScale;

		private int maxExistingFixPoints = 3;
		private TimerClass fixInputBufferTime = new TimerClass(.1f);
		public FloatVariable fixExitTimer;
		private TimerClass fixExitTime;

		[HideInInspector] public Vector2 velocity;
		[HideInInspector] public Vector2 fixHoldPosition;
		private Vector2 fixHangPosition;

		public BoolVariable fixStay, fixExit, hasJump, wallGrab;
		[HideInInspector]
		public bool canStayFix = true;
		private void Start()
		{
			fixExitTime = new TimerClass(fixExitTimer.value);
		}

		private void ControlTimers()
		{
			fixInputBufferTime.ProcessTimer();
			fixExitTime.ProcessTimer();
			if (fixExitTime.TimerIsOver())
				fixExit.value = false;
		}

		private void Update()
		{
			ControlTimers();
			ControlFixBuffer();
		}

		private void OnDisable()
		{
			DesactivateFixIndicatior();
		}

		private void FixedUpdate()
		{
			SetHangPosition();
			SetHoldPosition();

			velocity = Controller.Body.velocity;

			if (EnterFixConditions())
				EnterFix();
			if (StayFixCondition())
				FixStay();
			if (ExitFixConditions())
				ExitFix();

			Controller.Body.velocity = velocity;
		}
		private void SetHoldPosition()
		{
			fixHoldPosition = FixHoldObject.transform.position;
		}

		private void SetHangPosition()
		{
			fixHangPosition = FixAimingModule.FixAim(gameObject);
			if (CanEnterFixConditions() && FixAimReturnValidPosition()) {
				SetFixIndicatorPosition();
			} else if (!StayFixCondition() || !FixAimReturnValidPosition()) {
				DesactivateFixIndicatior();
			}
		}

		private void EnterFix()
		{
			FixEnterStatesModifyer();

			CreateFixPoint();

			OnEnterFixActionLoop();
		}

		private void FixStay()
		{
			UpdateHangPointPosition();

			OnStayFixActionLoop();
		}

		internal bool CanEnterFixConditions()
		{
			bool stateConditions = !IsGrounded() && Controller.canMove && hasJump.value && !fixStay.value && !wallGrab.value;
			return stateConditions;
		}
		internal bool EnterFixConditions()
		{
			bool inputConditions = fixInputBufferTime.TimerIsRunning() && ActionHoldInput();
			bool aimingConditions = FixAimReturnValidPosition();
			return CanEnterFixConditions() && inputConditions && aimingConditions;
		}
		internal bool StayFixCondition()
		{
			bool stateConditions = fixStay.value && canStayFix;
			bool inputConditions = ActionHoldInput();
			return stateConditions && inputConditions;
		}
		internal bool ExitFixConditions()
		{
			bool stateConditions = fixStay.value;
			bool inputConditions = !ActionHoldInput() || !canStayFix;
			return stateConditions && inputConditions;
		}

		private void ExitFix()
		{
			ApplyFixExitGravityScale();
			FixExitStateModifyer();

			OnExitFixActionLoop();

			fixExitTime.StartTimer();
			DisconnectFixHangComponent();
		}

		private void DisconnectFixHangComponent()
		{
			if (fixHangPointComponent) {
				fixHangPointComponent.hangRopeSegmentIndex = 0;
				fixHangPointComponent.DisconnectCurrentFixPoint();
			}
		}

		private void CreateFixPoint()
		{
			GameObject FixCurrentHangPoint = Instantiate(FixHangObject, fixHangPosition, Quaternion.identity);

			fixHangPointComponent = FixCurrentHangPoint.GetComponent<FixHangPointBehaviors>();
			fixHangPointComponent.ConnectFixHangPointToBody(Controller.Body, FixHoldObject.transform);

			HandleFixPointQueue(FixCurrentHangPoint);
		}

		private void HandleFixPointQueue(GameObject FixPrimaryHangPoint)
		{
			FixPointsQueue.Enqueue(FixPrimaryHangPoint);
			if (FixPointsQueue.Count > maxExistingFixPoints)
				FixPointsQueue.Dequeue().SetActive(false);
		}

		private void UpdateHangPointPosition() => fixHangPosition = fixHangPointComponent.CurrentFixPointObject.transform.position;

		private void DesactivateFixIndicatior()
		{
			FixIndicatorObject.SetActive(false);
			FixIndicatorObject.transform.position = Vector2.zero;
		}

		private void SetFixIndicatorPosition()
		{
			FixIndicatorObject.SetActive(true);
			FixIndicatorObject.transform.position = fixHangPosition;
		}

		private void ControlFixBuffer()
		{
			if (ActionInput())
				fixInputBufferTime.StartTimer();
		}

		private void FixEnterStatesModifyer()
		{
			ApplyFixGravityScale();
			DesactivateFixIndicatior();
			fixStay.value = true;
			canStayFix = true;
		}
		private void FixExitStateModifyer()
		{
			fixStay.value = false;
			fixExit.value = true;
		}

		private void OnEnterFixActionLoop()
		{
			for (int i = 0; i < FixActionsModule.Length; i++) {
				FixActionsModule[i].OnEnterFixDo(gameObject, fixHangPosition);
			}
		}
		private void OnStayFixActionLoop()
		{
			for (int i = 0; i < FixActionsModule.Length; i++) {
				FixActionsModule[i].OnStayFixDo(gameObject, fixHangPosition);
			}
		}
		private void OnExitFixActionLoop()
		{
			for (int i = 0; i < FixActionsModule.Length; i++) {
				FixActionsModule[i].OnExitFixDo(gameObject, fixHangPosition);
			}
		}

		internal void ApplyFixGravityScale() => Controller.Body.gravityScale = fixGravityScale.value;
		internal void ApplyFixExitGravityScale() => Controller.Body.gravityScale = fixExitGravityScale.value;
		internal Vector2 CollectInBetweenFixDirection() // Vector2 normalized starting from fixHoldPosition
		{
			return (fixHoldPosition - fixHangPosition).normalized;
		}
		internal Vector2 CollectForwardFixDirection()// Tangente du cercle de centre fixHangPosition à fixHoldPosition
		{
			Vector2 relativeFixPosition = Vector2.Perpendicular(fixHoldPosition - fixHangPosition).normalized;
			return relativeFixPosition;
		}
		internal bool FixAimReturnValidPosition() => fixHangPosition != Vector2.zero;
		internal Vector2 CalculRelativeFixedObjectPosition() => fixHangPosition - fixHoldPosition;

		internal bool IsGrounded() => Controller.onGround;
		internal bool ActionInput() => Controller.inputActionStart;
		internal bool ActionHoldInput() => Controller.inputAction;
		internal float HorizontalInputValue() => Controller.inputArrow;
	}
}
