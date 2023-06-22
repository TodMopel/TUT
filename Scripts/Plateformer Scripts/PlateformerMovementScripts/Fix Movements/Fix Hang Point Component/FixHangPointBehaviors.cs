using System;
using UnityEngine;
namespace TodMopel
{
	public class FixHangPointBehaviors : MonoBehaviour
	{
		public string OrphanContainerName = "OrphanContainer";
		public LayerMask PlateformLayer;
		public string PlateformTag;

		public LineRenderer FixHangPointLineRenderer;
		public GameObject FixSegmentPointPrefab;

		[SerializeField] private AbstractHangPointAction[] HangPointAction;

		public float fixHangPointElasticity = 1.1f;
		public bool InspectorConnect;

		[HideInInspector]
		public GameObject CurrentFixPointObject;
        [HideInInspector]
		public DistanceJoint2D CurrentFixPointDistanceJoint2D;

        [HideInInspector]
		public Rigidbody2D HoldConnectedBody;
        [HideInInspector]
		public Transform HoldConnectedBodyTransform;
        [HideInInspector]
		public float lengthBetweenHangToHold, lengthBetweenCurrentToHold;
        [HideInInspector]
		public bool bodyIsConnectedToJoint;

		public int hangRopeSegmentIndex = 1;

		public void ConnectFixHangPointToBody(Rigidbody2D BodyToConnect, Transform AnchorBodyTransform)
		{
			AssignHoldConnectedBody(BodyToConnect, AnchorBodyTransform);
			lengthBetweenHangToHold = CollectInBetweenRopeDistance() * fixHangPointElasticity;
			lengthBetweenCurrentToHold = lengthBetweenHangToHold;
			SetConnexionToCurrentFixPoint(BodyToConnect, lengthBetweenHangToHold);
			bodyIsConnectedToJoint = true;
		}

		private void Awake()
		{
			SetObjectAsCurrentFixPoint(gameObject);
			if (InspectorConnect) {
				ConnectFixHangPointToBody(CurrentFixPointDistanceJoint2D.connectedBody, CurrentFixPointDistanceJoint2D.connectedBody.gameObject.transform);
			}

			SetElementParentOfSurroundingPlateform(gameObject);

			// Set a loop of Enter actions
			OnStartConnectionActionLoop();
		}

		private void OnStartConnectionActionLoop()
		{
			for (int i = 0; i < HangPointAction.Length; i++) {
				HangPointAction[i].OnStartConnectionDo(gameObject);
			}
		}

		public void DisconnectCurrentFixPoint()
		{
			if (CurrentFixPointDistanceJoint2D != null) {
				CurrentFixPointDistanceJoint2D.connectedBody = null;
			}
			if (hangRopeSegmentIndex <= 1) {
				bodyIsConnectedToJoint = false;
				RemoveHangRopeLineRenderer();
				// Set a loop of Exit actions
				OnDisconnectActionLoop();
			}
		}

		private void Update()
		{
			if (bodyIsConnectedToJoint)
				OnStayConnectionActionLoop();
		}

		private void OnStayConnectionActionLoop()
		{
			for (int i = 0; i < HangPointAction.Length; i++) {
				HangPointAction[i].OnStayConnectedDo(gameObject);
			}
		}
		private void OnDisconnectActionLoop()
		{
			for (int i = 0; i < HangPointAction.Length; i++) {
				HangPointAction[i].OnDisconnectDo(gameObject);
				// Create physic rope ?
				// Make disapear with ligntning effect
			}
		}

		public void SetElementParentOfSurroundingPlateform(GameObject Element)
		{
			RaycastHit2D OthePlateformsLoockup = Physics2D.CircleCast(Element.transform.position, .5f, Element.transform.position, 0f, PlateformLayer);
			GameObject ParentPlateform;
			if (OthePlateformsLoockup && Element != null) {
				ParentPlateform = OthePlateformsLoockup.collider.gameObject;
				Element.transform.SetParent(ParentPlateform.transform);
			} else if (ParentPlateform = GameObject.Find(OrphanContainerName)) {
				Element.transform.SetParent(ParentPlateform.transform);
			}
		}

		private void LateUpdate()
		{
			if (bodyIsConnectedToJoint)
				DrawFixRopeFromHangPointToHangPoint();
		}
		private void DrawFixRopeFromHangPointToHangPoint()
		{
			GameObject SegmentRopeCounter = CurrentFixPointObject;
			for (int i = hangRopeSegmentIndex - 1; i >= 0; i--) {
				FixHangPointLineRenderer.SetPosition(i, SegmentRopeCounter.transform.position);
				FixSegmentWrapClass SegmentRopeCounterComponent;
				if (SegmentRopeCounterComponent = SegmentRopeCounter.GetComponent<FixSegmentWrapClass>()) {
					SegmentRopeCounter = GetPreviousSegment(SegmentRopeCounterComponent);
				}
			}
			FixHangPointLineRenderer.SetPosition(hangRopeSegmentIndex, GetHoldConnectedBodyPosition());
		}

		private void AssignHoldConnectedBody(Rigidbody2D BodyToConnect, Transform AnchorBodyTransform)
		{
			HoldConnectedBody = BodyToConnect; // Rb for distance Joint
			HoldConnectedBodyTransform = AnchorBodyTransform; // Transform for Distance Test & Line Renderer
		}

		public void SetHoldConnectedBodyToSegmentJoint(DistanceJoint2D Joint, float distance)
		{
			Joint.connectedBody = HoldConnectedBody;
			Joint.distance = distance;
		}

		public void SetConnexionToCurrentFixPoint(Rigidbody2D BodyToConnect, float distance)
		{
			CurrentFixPointDistanceJoint2D.connectedBody = BodyToConnect;
			UpdateCurrentFixRopeDistance(distance);
		}

		public void UpdateCurrentFixRopeDistance(float distance)
		{
			CurrentFixPointDistanceJoint2D.distance = distance;
		}

		public void UpdateHangRopeIndex(int add)
		{
			hangRopeSegmentIndex += add;
			FixHangPointLineRenderer.positionCount = hangRopeSegmentIndex + 1;
		}

		public void SetObjectAsCurrentFixPoint(GameObject Go)
		{
			CurrentFixPointObject = Go;
			CurrentFixPointDistanceJoint2D = Go.GetComponent<DistanceJoint2D>();
		}

		private void RemoveHangRopeLineRenderer() => FixHangPointLineRenderer.positionCount = 0;

		public static GameObject GetPreviousSegment(FixSegmentWrapClass SegmentRopeCounterComponent) => SegmentRopeCounterComponent.PreviousSegment;

		public float CollectInBetweenRopeDistance() => Vector2.Distance(GetHoldConnectedBodyPosition(), GetCurrentFixSegmentPointPosition());
		public Vector2 CollectInBetweenRopeDirection() => (GetHoldConnectedBodyPosition() - GetCurrentFixSegmentPointPosition()).normalized; // Vector2 normalized starting from Hang
		public Vector2 CollectInvertedInBetweenRopeDirection() => (GetCurrentFixSegmentPointPosition() - GetHoldConnectedBodyPosition()).normalized; // Vector2 normalized starting from Hold
		public Vector2 GetHoldConnectedBodyPosition() => HoldConnectedBodyTransform.position;
		public Vector2 GetCurrentFixSegmentPointPosition() => CurrentFixPointObject.transform.position;
	}
}
