using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "FixWrapFromHoldAction FixHangPointAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixHangPoint Actions/FixWrapFromHoldAction", order = 31)]
	public class FixWrapFromHoldAction : AbstractHangPointAction
	{
		public float fixWrapOffsetDistance = .1f;
		public float fixWrapProtectedDistance = 1f;

		public override void OnStartConnectionDo(GameObject HangedObject)
		{
			fixHangPointComponent = GetFixHangPointComponent(HangedObject);
		}
		public override void OnStayConnectedDo(GameObject HangedObject)
		{
			fixHangPointComponent = GetFixHangPointComponent(HangedObject);
			ControlWrapRope(fixHangPointComponent);
		}
		public override void OnDisconnectDo(GameObject HangedObject)
		{
			fixHangPointComponent = GetFixHangPointComponent(HangedObject);
		}

		private void ControlWrapRope(FixHangPointBehaviors fixHangPointComponent)
		{
			//Vector2 ControlPositionOffset = OffsetBySensitivityFromSegmentPositionToConnectedBody();
			RaycastHit2D WrapControlRay = Physics2D.Raycast(fixHangPointComponent.GetHoldConnectedBodyPosition(), fixHangPointComponent.CollectInvertedInBetweenRopeDirection(), fixHangPointComponent.CollectInBetweenRopeDistance() - .1f, fixHangPointComponent.PlateformLayer);
			Debug.DrawRay(fixHangPointComponent.GetHoldConnectedBodyPosition(), fixHangPointComponent.CollectInvertedInBetweenRopeDirection(), Color.red);

			if (WrapRopeConditions(WrapControlRay)) {
				CreateNewWrapPoint(WrapControlRay.point);
			}
			if (UnwrapRopeConditions()) {
				UnwrapRopePoint();
			}
		}

		private bool WrapRopeConditions(RaycastHit2D WrapControlRay)
		{
			bool rayTagCondition = WrapControlRay.collider?.gameObject.tag == fixHangPointComponent.PlateformTag;
			bool testItseflCondition = WrapControlRay.collider?.gameObject != fixHangPointComponent.HoldConnectedBody.gameObject;

			bool _distanceConditions = Vector2.Distance(fixHangPointComponent.GetHoldConnectedBodyPosition(), WrapControlRay.point) > fixWrapProtectedDistance;
			return rayTagCondition && testItseflCondition && _distanceConditions;
		}

		private bool UnwrapRopeConditions()
		{
			bool unwrapConditions = false;
			FixSegmentWrapClass CurrentSegmentRopeComponent;
			if (CurrentSegmentRopeComponent = fixHangPointComponent.CurrentFixPointObject.GetComponent<FixSegmentWrapClass>()) {
				float angleFromSegmentToHold = Vector2.Angle(fixHangPointComponent.CollectInBetweenRopeDirection(), CurrentSegmentRopeComponent.segmentOrientation);
				if (ValidAngleToUnwrap(angleFromSegmentToHold))
					unwrapConditions = true;
			}
			return unwrapConditions;
		}
		private static bool ValidAngleToUnwrap(float angleFromSegmentToHold) => angleFromSegmentToHold != 0 && angleFromSegmentToHold <= 90;

		private void CreateNewWrapPoint(Vector2 newFixSegmentPosition)
		{
			GameObject NewFixSegmentPoint = Instantiate(fixHangPointComponent.FixSegmentPointPrefab, newFixSegmentPosition, Quaternion.identity);
			FixSegmentWrapClass segmentComponent = NewFixSegmentPoint.GetComponent<FixSegmentWrapClass>();

			float previousSegmentDistance = Vector2.Distance(fixHangPointComponent.GetCurrentFixSegmentPointPosition(), newFixSegmentPosition);
			fixHangPointComponent.SetConnexionToCurrentFixPoint(segmentComponent.Body, previousSegmentDistance);
			segmentComponent.distanceFromPreviousSegment = previousSegmentDistance;

			fixHangPointComponent.lengthBetweenCurrentToHold = fixHangPointComponent.lengthBetweenCurrentToHold - previousSegmentDistance;
			segmentComponent.PreviousSegment = fixHangPointComponent.CurrentFixPointObject;
			fixHangPointComponent.SetHoldConnectedBodyToSegmentJoint(segmentComponent.Joint, fixHangPointComponent.lengthBetweenCurrentToHold);

			fixHangPointComponent.SetElementParentOfSurroundingPlateform(NewFixSegmentPoint);

			fixHangPointComponent.UpdateHangRopeIndex(+1);

			// Keep at the end of function
			fixHangPointComponent.SetObjectAsCurrentFixPoint(NewFixSegmentPoint);
		}
		private void UnwrapRopePoint()
		{
			fixHangPointComponent.DisconnectCurrentFixPoint();
			FixSegmentWrapClass segmentComponent = fixHangPointComponent.CurrentFixPointObject.GetComponent<FixSegmentWrapClass>();

			fixHangPointComponent.SetObjectAsCurrentFixPoint(segmentComponent.PreviousSegment);

			fixHangPointComponent.lengthBetweenCurrentToHold = fixHangPointComponent.lengthBetweenCurrentToHold + segmentComponent.distanceFromPreviousSegment;
			fixHangPointComponent.SetConnexionToCurrentFixPoint(fixHangPointComponent.HoldConnectedBody, fixHangPointComponent.lengthBetweenCurrentToHold);

			fixHangPointComponent.UpdateHangRopeIndex(-1);
			segmentComponent.Deactivate();
		}
	}
}
