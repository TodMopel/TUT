using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	public class ParallaxEffect : MonoBehaviour
	{
		public Transform subject;
		public Camera cam;

		private Vector2 startPosition;
		private float startZ;

		private Vector2 travel => (Vector2)cam.transform.position - startPosition;

		private float distanceFromSubject => transform.position.z - subject.position.z;
		private float clippingPlane => cam.gameObject.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane);

		private float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

		private void Start()
		{
			startPosition = transform.position;
			startZ = transform.position.z;
			if (!subject)
				subject = GameObject.Find("Avatar Container").GetComponent<Transform>();
			if (!cam)
				cam = Camera.main;
		}

		private void Update()
		{
			UpdateParallaxPosition();
		}

		private void UpdateParallaxPosition()
		{
			Vector2 newPos = startPosition + travel * parallaxFactor;
			transform.position = new Vector3(newPos.x, newPos.y, startZ);
		}
	}
}