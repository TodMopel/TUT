using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "FixExitBoostImpulse FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/FixExitBoostImpulse", order = 20)]
	public class FixExitBoostImpulse : AbstractFixMovementAction
	{
		public FloatVariable fixExitBoostForce;
		public FloatVariable fixExitBoostGravityScale;
		public float minAngle = 65, maxAngle = 95;

		//public Color fixStaminaIndicatorColor;

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			//fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);

			//float fixExitAngle = Vector2.Angle(fixMovementComponent.CollectInBetweenFixDirection(), Vector2.up);
			//if (IsBetween(fixExitAngle, minAngle, maxAngle)) {
			//	FixStaminaManagement fixStaminaComponent = FixedObject.GetComponent<FixStaminaManagement>();
			//	if (fixMovementComponent.fixStamina)
			//		fixStaminaComponent.fixStaminaBar.color = fixStaminaIndicatorColor;
			//}
		}
		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);

			float fixExitAngle = Vector2.Angle(fixMovementComponent.CollectInBetweenFixDirection(), Vector2.up);
			bool fixBoostAngle = fixExitAngle > minAngle && fixExitAngle < maxAngle;
			if (fixBoostAngle) {
				Vector2 fixExitDirection = fixMovementComponent.CollectForwardFixDirection();
				fixMovementComponent.Controller.Body.AddForce(fixExitDirection * fixExitBoostForce.value, ForceMode2D.Impulse);
				fixMovementComponent.Controller.Body.gravityScale = fixExitBoostGravityScale.value;
				//if (fixMovementComponent.fixStamina) {
				//	FixStaminaManagement fixStaminaComponent = FixedObject.GetComponent<FixStaminaManagement>();
				//	fixStaminaComponent.fixStaminaStat.currentFixStaminaValue = fixStaminaComponent.fixStaminaStat.maxFixStaminaValue;
				//}
			}
		}
	}
}