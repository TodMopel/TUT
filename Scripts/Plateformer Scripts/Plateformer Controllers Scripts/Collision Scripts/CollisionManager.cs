using System;
using UnityEngine;

namespace TodMopel
{
    public class CollisionManager : MonoBehaviour
	{
		[SerializeField] private AvatarController AvatarController;
		[SerializeField, Range(0f, 1f)] private float collisionSensitivity = .1f;
		[HideInInspector]
		public bool groundCollision, slopeCollision, wallCollision, cellingCollision;
        private Vector2 normal;

		private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            EvaluateCollision(collision);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            if (collision.gameObject.tag == AvatarController.PlateformTag) {
				if (ZeroContact(collision)) {
					groundCollision = false;
					cellingCollision = false;
					wallCollision = false;
					normal = Vector2.zero;
				} else {
					for (int i = 0; i < collision.contactCount; i++) {
						normal = collision.GetContact(i).normal;

						groundCollision |= TestGroundCollision();
						cellingCollision |= TestCellingCollision();
						wallCollision |= TestWallCollision();
						slopeCollision = TestSlopeCollision();
						if ((normal.x <= collisionSensitivity) && (normal.x >= -collisionSensitivity))
							wallCollision = false;
					}
				}
			}
			AvatarController.onGround = groundCollision;
		}

		private static bool ZeroContact(Collision2D collision) => collision.contactCount == 0;
		private bool TestGroundCollision() => normal.y >= collisionSensitivity;
		private bool TestCellingCollision() => normal.y <= -collisionSensitivity;
		private bool TestWallCollision() => (normal.x >= collisionSensitivity) || (normal.x <= -collisionSensitivity);
		private bool TestSlopeCollision() => (normal.x >= collisionSensitivity/2) && (normal.y >= collisionSensitivity/2);

	}
}