using UnityEngine;

namespace TodMopel
{
	public abstract class AbstractFixAiming : ScriptableObject
	{
		[HideInInspector] public PlateformerFixMovement fixMovementComponent;
		[HideInInspector] public Vector2 fixHangPosition = Vector2.zero;
		public abstract Vector2 FixAim(GameObject Object);
	}
}