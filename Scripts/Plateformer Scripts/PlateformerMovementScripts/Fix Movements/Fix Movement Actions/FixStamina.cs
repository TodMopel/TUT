using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "FixStamina FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/FixStamina", order = -9)]
	public class FixStamina : AbstractFixMovementAction
	{
		public StaminaStat fixStaminaStat;

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			PlateformerStaminaManagement StaminaComponent = FixedObject.GetComponent<PlateformerStaminaManagement>();
			if (StaminaComponent.enabled) {
				if (fixStaminaStat.currentStaminaValue.value < 0)
					fixMovementComponent.canStayFix = false;
			}
		}
		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
	}
}