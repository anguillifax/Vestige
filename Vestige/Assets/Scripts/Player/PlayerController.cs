using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class PlayerController : MonoBehaviour
	{
		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		[Expandable] public PlayerControllerConfig config;
		public PlayerAvatar avatar;
		public Camera cameraMain;

		[Header("Motion")]
		public Transform lookRotation;

		[Header("Holdable")]
		public HoldableHarness harness;
		public RectTransform overlayContainer;

		private Vector3 cursorTarget;
		private Plane cursorRaycastPlane;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			if (!lookRotation)
			{
				lookRotation = transform;
			}

			cursorRaycastPlane = new Plane(Vector3.up, 0);
			harness.overlayContainer = overlayContainer;
		}

		private void OnEnable()
		{
			cursorTarget = transform.position;
		}

		// =========================================================
		// Render Update
		// =========================================================

		private void Update()
		{
			UpdateCursorTarget();
			UpdateHoldableDetach();
			UpdateHoldableActions();
		}

		private void UpdateCursorTarget()
		{
			cursorRaycastPlane.distance = transform.position.y;
			var ray = cameraMain.ScreenPointToRay(Input.mousePosition);
			if (cursorRaycastPlane.Raycast(ray, out float dist))
			{
				cursorTarget = ray.GetPoint(dist);
			}

			Debug.DrawRay(cursorTarget, Vector3.up);
		}

		private void UpdateHoldableDetach()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				harness.Detach();
			}
		}

		private void UpdateHoldableActions()
		{
			harness.SendPrimaryActionAuto(Input.GetButton("Fire1"));
			harness.SendSecondaryActionAuto(Input.GetButton("Fire2"));
		}

		// =========================================================
		// Physics Update
		// =========================================================

		private void FixedUpdate()
		{
			Vector3 vel = avatar.Rigidbody.velocity;
			vel.y = 0;

			Vector3 inputs = new Vector3(
				Input.GetAxisRaw("Horizontal"),
				0,
				Input.GetAxisRaw("Vertical"));
			inputs = Vector3.ClampMagnitude(inputs, 1);

			Vector3 rotated = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0) * inputs;

			vel = Vector3.MoveTowards(
				vel,
				rotated * config.walkVel,
				config.walkAccel * Time.fixedDeltaTime);

			vel.y = avatar.Rigidbody.velocity.y;
			avatar.Rigidbody.velocity = vel;
		}

		// =========================================================
		// Physics Callbacks
		// =========================================================

		private void OnTriggerStay(Collider other)
		{
			if (Input.GetKey(KeyCode.E))
			{
				var holdable = other.attachedRigidbody.GetComponent<IHoldable>();
				if (holdable != null)
				{
					harness.Attach(holdable);
				}
			}
		}

		// =========================================================
		// Public API
		// =========================================================

		public Vector3 GetCameraFocusPoint()
		{
			Vector2 mpos = new Vector2(
				Mathf.Clamp(Input.mousePosition.x, 0, Screen.width) - Screen.width / 2,
				Mathf.Clamp(Input.mousePosition.y, 0, Screen.height) - Screen.height / 2);
			mpos /= Screen.height;

			Vector3 worldOffset = new Vector3(
				mpos.x * config.cameraMouseVertWorldOffset,
				0,
				mpos.y * config.cameraMouseVertWorldOffset);

			Vector3 combined = avatar.transform.position + worldOffset;
			//Debug.DrawRay(combined, Vector3.up);
			//Debug.Log($"Offset {worldOffset} combined {combined}");
			return combined;
		}
	}
}