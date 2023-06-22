using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "ShrinkCollisionBox FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/ShrinkCollisionBox", order = 10)]
	public class ShrinkCollisionBox : AbstractFixMovementAction
	{
		private const string SPRITE_OBJ_NAME = "Avatar Graphics";
		[Range(.7f, 1f)] public float modifyer;
		private CapsuleCollider2D capsuleCollider;

		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			if (capsuleCollider = GameObject.Find(SPRITE_OBJ_NAME).GetComponent<CapsuleCollider2D>()) {
				Vector2 collisionSize = GetCapsuleColliderSize();
				collisionSize = collisionSize * modifyer;
				ApplySizeToCapsuleCollider(collisionSize);

				ApplyCapsuleColliderOffset();
			}
		}


		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			//fixMovementComponent = GetFixMovementComponent(FixedObject);
		}

		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			if (capsuleCollider = GameObject.Find(SPRITE_OBJ_NAME).GetComponent<CapsuleCollider2D>()) {
				Vector2 collisionSize = GetCapsuleColliderSize();
				collisionSize = collisionSize / modifyer;
				ApplySizeToCapsuleCollider(collisionSize);
			
				ResetCapsuleColliderOffset();
			}
		}

		private Vector2 GetCapsuleColliderSize() => capsuleCollider.size;
		private void ApplySizeToCapsuleCollider(Vector2 collisionSize) => capsuleCollider.size = collisionSize;
		private void ApplyCapsuleColliderOffset() => fixMovementComponent.Controller.Collider.offset = new Vector2(0f, 1 - modifyer);
		private void ResetCapsuleColliderOffset() => fixMovementComponent.Controller.Collider.offset = new Vector2(0f, 0f);
	}
}

