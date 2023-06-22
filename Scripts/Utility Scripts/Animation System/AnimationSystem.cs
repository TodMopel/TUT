using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
	public static class AnimationSystem
	{
		// Continuous
		public static Sprite PlaySpriteAnimation(List<Sprite> currentSpriteList, int frameRate)
		{
			float playTime = Time.time;
			int frame = (int)(playTime * frameRate % currentSpriteList.Count);

			return currentSpriteList[frame];
		}
	}
}
