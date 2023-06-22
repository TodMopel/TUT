using UnityEngine;
using System;

namespace TodMopel
{
    public class FixSegmentWrapClass : MonoBehaviour
    {
        [HideInInspector]
		public Vector2 segmentOrientation;
        [HideInInspector]
        public float distanceFromPreviousSegment;
        [HideInInspector]
        public GameObject PreviousSegment;

        [HideInInspector]
        public Rigidbody2D Body;
        [HideInInspector]
        public DistanceJoint2D Joint;


        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            Joint = GetComponent<DistanceJoint2D>();
        }

        private void Update()
		{
			SetSegmentOrientation();
		}

		private void SetSegmentOrientation()
		{
			RaycastHit2D OthePlateformsLoockup = Physics2D.CircleCast(transform.position, .5f, transform.position, 0f);

			// Create two perpendicular from previous Segment
			Vector2 OrientationPositive = Vector2.Perpendicular(PreviousSegment.transform.position - transform.position).normalized;
			Vector2 OrientationNegative = Vector2.Perpendicular(transform.position - PreviousSegment.transform.position).normalized;

			// test the two perpendicular from normal and select the closest from last segment
			float anglePositive = Vector2.Angle(OthePlateformsLoockup.normal, OrientationPositive);
			float angleNegative = Vector2.Angle(OthePlateformsLoockup.normal, OrientationNegative);
			segmentOrientation = anglePositive < angleNegative ? OrientationPositive : OrientationNegative;
			Debug.DrawRay(transform.position, segmentOrientation, Color.yellow);
		}

		internal void Deactivate() => gameObject.SetActive(false);
	}
}
