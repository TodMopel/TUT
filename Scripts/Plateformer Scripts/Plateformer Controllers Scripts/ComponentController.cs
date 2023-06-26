using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
	public class ComponentController : MonoBehaviour
	{
		public AvatarController controller;

		public BoolVariable fixBool;
		private bool fixActive;
		public PlateformerFixMovement fixComponents;

		public BoolVariable wallBool;
		private bool wallActive;
		public PlateformerWallMovement wallComponents;

		public BoolVariable staminaBool;
		private bool staminaActive;
		public PlateformerStaminaManagement staminaComponents;

		public BoolVariable jumpBool;
		private bool jumpActive;
		public OneButtonJumpMovement jumpomponents;

		private bool paused;

		private void Start()
		{
			fixComponents.enabled = fixActive = fixBool.value;
			wallComponents.enabled = wallActive = wallBool.value;
			staminaComponents.enabled = staminaActive = staminaBool.value;
			jumpomponents.enabled = jumpActive = jumpBool.value;
		}

		private void Update()
		{
			if (fixActive != fixBool.value)
				fixComponents.enabled = fixActive = fixBool.value;
			if (wallActive != wallBool.value)
				wallComponents.enabled = wallActive = wallBool.value;
			if (staminaActive != staminaBool.value)
				staminaComponents.enabled = staminaActive = staminaBool.value;
			if (jumpActive != jumpBool.value)
				jumpomponents.enabled = jumpActive = jumpBool.value;

			if (paused != controller.paused) {
				staminaComponents.enabled = wallComponents.enabled = fixComponents.enabled = paused;
				paused = controller.paused;
			}
		}

		public void DesactivateAllComponent()
		{
			jumpomponents.enabled = staminaComponents.enabled = wallComponents.enabled = fixComponents.enabled = false;
		}
	}
}
