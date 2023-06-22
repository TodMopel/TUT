using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class ParallaxRepeater : MonoBehaviour
	{
		private Sprite sprite;
		private Transform subject;
		private SpriteRenderer spriteRenderer;

		private Color myColor;

		public bool repeatY;

		private Vector2 spriteSize;

		private Vector2 startPosition;

		float startZ;

		private GameObject currentChild;
		private GameObject horizontalChild;
		private GameObject verticalChild;
		private GameObject cornerChild;

		private void Start()
		{
			subject = GetComponent<ParallaxEffect>().cam.transform;
			spriteRenderer = GetComponent<SpriteRenderer>();
			sprite = spriteRenderer.sprite;
			myColor = spriteRenderer.color;
			spriteRenderer.enabled = false;

			startPosition = transform.position;
			startZ = transform.position.z;
			spriteSize = sprite.bounds.size;

			CreateStartSprites();
		}

		private void CreateStartSprites()
		{
			currentChild = CreateParallaxSprite(startPosition, Vector2.zero);
			horizontalChild = CreateParallaxSprite(startPosition, Vector2.right);
			if (repeatY) {
				verticalChild = CreateParallaxSprite(startPosition, Vector2.up);
				cornerChild = CreateParallaxSprite(horizontalChild.transform.position, Vector2.up);
			}
		}

		private GameObject CreateParallaxSprite(Vector2 position, Vector2 direction)
		{
			float size = Mathf.Abs(direction.x) > 0 ? spriteSize.x : spriteSize.y;

			GameObject spriteObject = new GameObject();
			Vector3 spritePosition = position + direction * size;
			spritePosition.z = startZ;
			spriteObject.transform.position = spritePosition;
			spriteObject.transform.SetParent(transform);
			SpriteRenderer childSpriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
			childSpriteRenderer.sprite = sprite;
			childSpriteRenderer.color = myColor;

			return spriteObject;
		}

		private void Update()
		{
			HandleRepeatingParallax();
		}

		private void HandleRepeatingParallax()
		{
			Vector3 subjectPosition = subject.transform.position;
			Vector3 currentPosition = currentChild.transform.position;
			Vector3 horizontalChildPosition = horizontalChild.transform.position;
			Vector3 verticalChildPosition = verticalChild.transform.position;
			Vector3 cornerChildPosition = cornerChild.transform.position;

			bool switchLeft = subjectPosition.x < currentPosition.x && horizontalChildPosition.x > currentPosition.x;
			bool switchRight = subjectPosition.x > currentPosition.x && horizontalChildPosition.x < currentPosition.x;
			float horizontalSwitchValue = switchRight ? 1 : -1;

			bool switchHorizontalChildPosition = switchLeft || switchRight;
			if (switchHorizontalChildPosition) {
				horizontalChild.transform.position = new Vector3(horizontalChildPosition.x + 2 * spriteSize.x * horizontalSwitchValue, horizontalChildPosition.y, horizontalChildPosition.z);
				horizontalChildPosition = horizontalChild.transform.position;
				if (repeatY) {
					cornerChild.transform.position = new Vector3(cornerChildPosition.x + 2 * spriteSize.x * horizontalSwitchValue, cornerChildPosition.y, cornerChildPosition.z);
					cornerChildPosition = cornerChild.transform.position;
				}
			}

			switchLeft = subjectPosition.x < currentPosition.x - spriteSize.x/2 && horizontalChildPosition.x < currentPosition.x;
			switchRight = subjectPosition.x > currentPosition.x + spriteSize.x / 2 && horizontalChildPosition.x > currentPosition.x;

			bool switchCurrentChild = switchLeft || switchRight;
			if (switchCurrentChild) {
				StartCoroutine(SwapCurrentHorizontalChildObject());
			}

			if (repeatY) {
				bool switchDown = subjectPosition.y < currentPosition.y && verticalChildPosition.y > currentPosition.y;
				bool switchUp = subjectPosition.y > currentPosition.y && verticalChildPosition.y < currentPosition.y;
				float verticalSwitchValue = switchUp ? 1 : -1;

				bool switchVerticalChildPosition = switchUp || switchDown;
				if (switchVerticalChildPosition) {
					verticalChild.transform.position = new Vector3(verticalChildPosition.x, verticalChildPosition.y + 2 * spriteSize.y * verticalSwitchValue, verticalChildPosition.z);
					cornerChild.transform.position = new Vector3(cornerChildPosition.x, cornerChildPosition.y + 2 * spriteSize.y * verticalSwitchValue, cornerChildPosition.z);
					verticalChildPosition = verticalChild.transform.position;
				}

				switchDown = subjectPosition.y < currentPosition.y - spriteSize.y / 2 && verticalChildPosition.y < currentPosition.y;
				switchUp = subjectPosition.y > currentPosition.y + spriteSize.y / 2 && verticalChildPosition.y > currentPosition.y;

				switchCurrentChild = switchDown || switchUp;
				if (switchCurrentChild) {
					StartCoroutine(SwapCurrentVerticalChildObject());
				}
			}
		}

		private IEnumerator SwapCurrentVerticalChildObject()
		{
			yield return new WaitForEndOfFrame();
			GameObject swapObject = verticalChild;
			verticalChild = currentChild;
			currentChild = swapObject;

			swapObject = cornerChild;
			cornerChild = horizontalChild;
			horizontalChild = swapObject;
		}

		private IEnumerator SwapCurrentHorizontalChildObject()
		{
			yield return new WaitForEndOfFrame();
			GameObject swapObject = horizontalChild;
			horizontalChild = currentChild;
			currentChild = swapObject;
			if (repeatY) {
				swapObject = cornerChild;
				cornerChild = verticalChild;
				verticalChild = swapObject;
			}
		}
	}
}
