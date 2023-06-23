using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class LoopAnimation : MonoBehaviour
	{
		public SpriteRenderer spriteRenderer;

		public AnimationSystem.AnimContainer loopAnimation;

		private void Update()
		{
			spriteRenderer.sprite = AnimationSystem.PlaySpriteAnimation(loopAnimation.spriteList, loopAnimation.frameRate);
		}
	}
}
