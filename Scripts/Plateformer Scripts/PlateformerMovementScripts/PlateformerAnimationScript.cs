using System.Collections;
using System.Collections.Generic;
using TodMopel;
using UnityEngine;

public class PlateformerAnimationScript : MonoBehaviour
{
	public AvatarController Controller;
	public SpriteRenderer avatarSpriteRenderer;

	public List<Sprite> AvatarIdleAnimations;
	public List<Sprite> AvatarJumpAnimations;
	public List<Sprite> AvatarRunAnimations;
	public List<Sprite> AvatarFixStayAnimations;
	public List<Sprite> AvatarWallGrabAnimations;
	public List<Sprite> AvatarWallRunAnimations;
	public List<Sprite> AvatarStuntAirAnimations;
	public List<Sprite> AvatarStuntGroundedAnimations;
	public List<Sprite> AvatarAcrobaticAnimations;

	public int frameRate;

	public BoolVariable avatarFixed, wallGrab, avatarStunt, avatarAcrobatic;
	public FloatVariable fixSpeed, jumpSpeed;

	private float avatarVelocityY => Controller.Body.velocity.y;

	private void Update()
	{
		avatarSpriteRenderer.sprite = AnimationSystem.PlaySpriteAnimation(SelectSpriteAnimation(), frameRate);
		HandleSpriteDirection();
	}

	private void HandleSpriteDirection()
	{
		if (!avatarStunt.value && !avatarFixed.value || (avatarStunt.value && !Controller.onGround)) {
			if (!avatarSpriteRenderer.flipX && Controller.inputArrow < 0)
				avatarSpriteRenderer.flipX = true;
			else if (avatarSpriteRenderer.flipX && Controller.inputArrow > 0)
				avatarSpriteRenderer.flipX = false;
		}
	}

	private List<Sprite> SelectSpriteAnimation()
	{
		bool avatarGrounded = Controller.onGround;
		if (avatarStunt.value) {
			if (avatarGrounded)
					return AvatarStuntGroundedAnimations;
			return GetSpriteFromVerticalVelocity_RangeValue(AvatarStuntAirAnimations, jumpSpeed.value);
		}
		if (Controller.canMove) {
			if (avatarFixed.value)
				return GetSpriteFromVerticalVelocity_RangeValue(AvatarFixStayAnimations, -fixSpeed.value * 5);
			if (avatarGrounded && Controller.inputArrow != 0) {
				if (avatarVelocityY <= 1 && avatarVelocityY >= -1)
					return AvatarRunAnimations;
			}
			if (!avatarGrounded) {
				if (avatarAcrobatic.value)
					return AvatarAcrobaticAnimations;
				//if (avatarFixed) {
				//	if (Controller.inputArrow != 0)
				//		return GetSpriteFromVerticalVelocity_RangeValue(AvatarFixStayAnimations, -fixSpeed.value);
				//	else
				//		return GetSpriteFromVerticalVelocity_RangeValue(AvatarFixStayAnimations, fixSpeed.value);
				//}
				bool avatarWallGrabbing = wallGrab.value;
				if (avatarWallGrabbing) {
					if (Controller.Body.velocity.y <= 0)
						return AvatarWallGrabAnimations;
					return AvatarWallRunAnimations;
				}
				else
					return GetSpriteFromVerticalVelocity_RangeValue(AvatarJumpAnimations, jumpSpeed.value);
			}
		}
		return AvatarIdleAnimations;
	}

	private List<Sprite> GetSpriteFromVerticalVelocity_RangeValue(List<Sprite> spriteList, float treshold)
	{
		List<Sprite> SelectedSprite = new List<Sprite>();
		int airIndex = (int)Mathf.Clamp(
				TodUtils.Remap(avatarVelocityY, treshold, -treshold, 0, spriteList.Count),
				0,
				spriteList.Count - 1
			);
		SelectedSprite.Add(spriteList[airIndex]);
		return SelectedSprite;
	}
	private List<Sprite> GetSpriteFromVelocity_LinearValue(List<Sprite> spriteList, float treshold)
	{
		List<Sprite> SelectedSprite = new List<Sprite>();
		int airIndex = (int)Mathf.Clamp(
				TodUtils.Remap(avatarVelocityY, 0, treshold, 0, spriteList.Count),
				0,
				spriteList.Count - 1
			);
		SelectedSprite.Add(spriteList[airIndex]);
		return SelectedSprite;
	}
}
