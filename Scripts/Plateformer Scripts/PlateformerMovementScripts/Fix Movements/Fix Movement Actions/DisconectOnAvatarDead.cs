using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "DisconectOnAvatarDead FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/DisconectOnAvatarDead", order = 20)]
	public class DisconectOnAvatarDead : AbstractFixMovementAction
	{
		public BoolVariable avatarAlive;
		public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
		public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
			if (!avatarAlive.value) {
				fixMovementComponent.canStayFix = false;
			}
		}
		public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
		{
			fixMovementComponent = GetFixMovementComponent(FixedObject);
		}
	}
}