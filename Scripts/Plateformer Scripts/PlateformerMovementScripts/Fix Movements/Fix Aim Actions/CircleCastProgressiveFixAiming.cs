using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "New CircleCastProgressiveFixAiming", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixAimingModules/CircleCastProgressiveFixAiming", order = 12)]
	public class CircleCastProgressiveFixAiming : AbstractFixAiming
	{
		public float maxAimingModifyer = .45f;
		public float maxRadiusModifyer = .5f;

		public override Vector2 FixAim(GameObject Object)
		{
			fixMovementComponent = Object.GetComponent<PlateformerFixMovement>();

			Vector2 aimingDirection = new Vector2(fixMovementComponent.Controller.inputArrow * maxAimingModifyer, 1);

			for (float radiusModifyer = 0.1f; radiusModifyer < maxRadiusModifyer; radiusModifyer += 0.05f) {
				Vector2 offsetPosition = fixMovementComponent.fixHoldPosition - (aimingDirection - fixMovementComponent.fixHoldPosition).normalized * radiusModifyer;
				RaycastHit2D aimingCast = Physics2D.CircleCast(offsetPosition, radiusModifyer, aimingDirection, fixMovementComponent.maxAimingLength.value, fixMovementComponent.Controller.PlateformLayer);
				bool minDistanceCheck = Vector2.Distance(aimingCast.point, fixMovementComponent.fixHoldPosition) > fixMovementComponent.minAimingLength;
				if (aimingCast.point.y > fixMovementComponent.fixHoldPosition.y && minDistanceCheck) {
					fixHangPosition = aimingCast.point;
					break;
				} else {
					fixHangPosition = Vector2.zero;
				}
			}

			return fixHangPosition;
		}
	}
}
