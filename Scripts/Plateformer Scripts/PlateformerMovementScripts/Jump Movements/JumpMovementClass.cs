using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TodMopel
{
	public class JumpMovementClass
	{
		public float CalculJumpMoveSpeed(float jumpHeight, Vector2 velocity)
		{
			float jumpSpeed = JumpSpeedFormula(jumpHeight);

			if (AscendingMomentum(velocity))
				jumpSpeed = ModulateJumpSpeed(jumpSpeed, velocity);
			else if (DescendingMomentum(jumpSpeed, velocity))
				jumpSpeed += CompensateJumpSpeed(velocity);

			return jumpSpeed;
		}

		private float JumpSpeedFormula(float height)
		{
			return Mathf.Sqrt(-2f * Physics2D.gravity.y * height);
		}

		private bool AscendingMomentum(Vector2 velocity) => velocity.y > 0;
		private float ModulateJumpSpeed(float jumpSpeed, Vector2 velocity) => Mathf.Max(jumpSpeed - velocity.y, 0f);

		private bool DescendingMomentum(float maxFallingSpeed, Vector2 velocity) => velocity.y < -maxFallingSpeed;
		private float CompensateJumpSpeed(Vector2 velocity) => Mathf.Abs(velocity.y);
	}
}
