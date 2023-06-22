using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
	public class AcrobaticMovement : MonoBehaviour
	{
		public AvatarController Controller;
		public PlateformerFixMovement fixComponent;
		public StaminaStat staminaStat;

		public BoolVariable avatarAcrobaticState;

		private bool triggerState;

		private void Update()
		{
			if (Controller.canMove)
				ControlAccrobaticState();
		}
		private void ControlAccrobaticState()
		{
			if (fixComponent.ActionInput() && !fixComponent.FixAimReturnValidPosition() && fixComponent.CanEnterFixConditions())
				StartCoroutine(trigg());
			if (triggerState && fixComponent.ActionHoldInput())
				avatarAcrobaticState.value = true;
			else
				avatarAcrobaticState.value = false;
			if (!fixComponent.ActionHoldInput() || !fixComponent.CanEnterFixConditions())
				triggerState = false;
			//if (Controller.avatarProgressionStats.avatarAcrobaticState && fixComponent.IsGrounded()) { // break stamina if langing on acrobatic state
			//	fixStaminaStat.currentFixStaminaValue = -.1f;
			//}
		}

		private IEnumerator trigg()
		{
			yield return new WaitForEndOfFrame();
			triggerState = true;
		}
	}
}
