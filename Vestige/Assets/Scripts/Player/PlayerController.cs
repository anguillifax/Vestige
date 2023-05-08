using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
		public PlayerHealthbarAvatar healthbar;

		[Header("Motion")]
		public Transform lookRotation;

		[Header("Holdable")]
		public HoldableHarness harness;
		public RectTransform overlayContainer;

		[Header("Health")]
		public UnityEvent died;

		private StandardRecipient systemic;
		private float capacityFire;
		private float capacityWater;
		private Vector3 cursorTarget;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			if (!lookRotation)
			{
				lookRotation = transform;
			}

			harness.overlayContainer = overlayContainer;
			systemic = GetComponent<StandardRecipient>();

			capacityFire = 0;
			capacityWater = 0;
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
			UpdateHoldableAttach();
			UpdateHoldableActions();
			UpdateHealth();
		}

		private void UpdateCursorTarget()
		{
			Plane plane = new Plane(Vector3.up, transform.position);
			var ray = cameraMain.ScreenPointToRay(Input.mousePosition);
			if (plane.Raycast(ray, out float dist))
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

		private void UpdateHoldableAttach()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Vector3 origin = transform.position + config.pickupOffset;

				Collider[] nearby = Physics.OverlapSphere(
					origin,
					config.pickupRadius,
					config.pickupMask,
					QueryTriggerInteraction.Collide);

				IHoldable closest = nearby
					.Where(x => x.attachedRigidbody != null)
					.OrderBy(x => Vector3.SqrMagnitude(origin - x.attachedRigidbody.position))
					.Select(x => x.attachedRigidbody.GetComponent<IHoldable>())
					.FirstOrDefault(x => x != null && x != harness.Target);

				if (closest != null)
				{
					harness.Attach(closest);
				}
			}
		}

		private void UpdateHoldableActions()
		{
			harness.SendInputs(Input.GetButton("Fire1"), Input.GetButton("Fire2"));
		}

		private void UpdateHealth()
		{
			var nonPlayer = systemic.effects.AsEnumerable();
			if (harness.Target != null)
			{
				nonPlayer = systemic.effects.Where(x => x.source != harness.Target.Root);
			}
			bool hasFire = nonPlayer.Any(x => x.ignite || x.burn);
			bool hasWater = nonPlayer.Any(x => x.douse || x.soak);

			void Do(ref float cap, bool grow)
			{
				cap += (grow ? config.healthGrowSpeed : -config.healthDecaySpeed) * Time.deltaTime;
				cap = Mathf.Clamp(cap, 0, config.healthMax);
			}

			Do(ref capacityFire, hasFire);
			Do(ref capacityWater, hasWater);

			healthbar.SetFireCapacity(capacityFire / config.healthMax);
			healthbar.SetWaterCapacity(capacityWater / config.healthMax);

			if (capacityFire == config.healthMax || capacityWater == config.healthMax)
			{
				died.Invoke();
				harness.Detach();
				Destroy(gameObject);
				FindObjectOfType<GameSession>().ReloadSceneAfterDelay();
			}
		}

		// =========================================================
		// Physics Update
		// =========================================================

		private void FixedUpdate()
		{
			UpdateMovement();
			UpdateLookRotation();
		}

		private void UpdateMovement()
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

			Vector3 extraGravity = Mathf.Max(0, config.gravityMultiplier - 1) * Physics.gravity;
			vel.y += extraGravity.y * Time.fixedDeltaTime;

			avatar.Rigidbody.velocity = vel;
		}

		private void UpdateLookRotation()
		{
			Vector3 delta = cursorTarget - transform.position;
			delta.y = 0;
			if (delta.sqrMagnitude > 0)
			{
				Quaternion angle = Quaternion.LookRotation(delta.normalized, Vector3.up);
				avatar.Rigidbody.MoveRotation(angle);
				//harness.transform.rotation = angle;
			}
		}

		// =========================================================
		// Physics Callbacks
		// =========================================================

		private void OnTriggerStay(Collider other)
		{
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