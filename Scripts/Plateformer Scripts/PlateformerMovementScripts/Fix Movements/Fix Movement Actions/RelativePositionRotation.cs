using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "RelativePositionRotation FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/RelativePositionRotation", order = 1)]
	public class RelativePositionRotation : AbstractFixMovementAction
	{
		private const string SPRITE_OBJ_NAME = "Avatar Graphics";
		private int defaultRotation = 0;

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			//fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);

			Vector2 relativeObjectPosition = fixPosition - (Vector2)FixedObject.transform.position;
			float currentRotation = CalculObjectRotationToApply(relativeObjectPosition);
			ApplyObjectRotation(FixedObject, currentRotation);
		}

		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			//fixMovementComponent = GetFixMovementComponent(FixedObject);
			ApplyObjectRotation(FixedObject, defaultRotation);
		}

		private static void ApplyObjectRotation(GameObject FixedObject, float currentRotation)
		{
			Quaternion rotation = Quaternion.AngleAxis(currentRotation, Vector3.forward);
			FixedObject.transform.Find(SPRITE_OBJ_NAME).transform.rotation = rotation; // Rotate avatar sprite object
																					   //FixedObject.transform.rotation = rotation; // Rotate avatar object
		}

		private static float CalculObjectRotationToApply(Vector2 relativeObjectPosition)
		{
			float angle = Mathf.Atan2(relativeObjectPosition.y, relativeObjectPosition.x) * Mathf.Rad2Deg;
			float currentRotation = angle - 90;
			return currentRotation;
		}
	}
}