using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class GroundCollision : MonoBehaviour
	{
		public AvatarController avatarController;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == avatarController.PlateformTag) {
				avatarController.onGround = true;
			}
		}
		private void OnTriggerStay2D(Collider2D collision)
		{
			if (collision.gameObject.tag == avatarController.PlateformTag) {
				avatarController.onGround = true;
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			StartCoroutine(ResetGroundValue());
		}

		private IEnumerator ResetGroundValue()
		{
			yield return new WaitForEndOfFrame();
			avatarController.onGround = false;
		}
	}
}
