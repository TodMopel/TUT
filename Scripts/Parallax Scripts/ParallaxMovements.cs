using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class ParallaxMovements : MonoBehaviour
	{
		public Transform subject;
		public Camera cam;
		public Sprite sprite;
		private SpriteRenderer spriteRenderer;

		public bool repeatY;

		private Vector2 spriteSize;
		private Vector2 startPosition;
		float startZ;

		private void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			spriteRenderer.enabled = false;

			spriteSize = sprite.bounds.size;
			startPosition = transform.position;
			startZ = transform.position.z;

			CreateStartSprites();
		}

		private void CreateStartSprites()
		{
			throw new NotImplementedException();
		}
	}
}