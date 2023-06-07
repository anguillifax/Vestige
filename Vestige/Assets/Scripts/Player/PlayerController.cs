using FMODUnity;
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

		[Header("Cross Reference")]
		public PlayerHealthbarAvatar healthbar;
		public Transform lookRotation;
		public RectTransform overlayContainer;
		public Camera cameraMain;

		[Header("Health")]
		public UnityEvent died;

		[Header("Sound")]
		public StudioEventEmitter evSpawn;
		public StudioEventEmitter evDeath;
		public StudioEventEmitter evFootstep;

		[Header("Debug")]
		public InspectorCallbackButton testKill = new InspectorCallbackButton("Kill Player");

		private PlayerAvatar avatar;
		private Rigidbody rbody;
		private HoldableHarness harness;
		private StandardRecipient systemic;
		private float curFireHealth;
		private float curWaterHealth;
		private Vector3 cursorTarget;
		private float curFootstepDist;

		// =========================================================
		// Properties
		// =========================================================

		public float HealthFire
		{
			get => curFireHealth;
			set => curFireHealth = Mathf.Clamp(value, 0, config.healthFire.max);
		}

		public float HealthWater
		{
			get => curWaterHealth;
			set => curWaterHealth = Mathf.Clamp(value, 0, config.healthWater.max);
		}

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			harness = GetComponentInChildren<HoldableHarness>();
			systemic = GetComponent<StandardRecipient>();
			avatar = GetComponent<PlayerAvatar>();
			rbody = GetComponent<Rigidbody>();

			healthbar = FindObjectOfType<PlayerHealthbarAvatar>();
			overlayContainer = GameObject.FindWithTag("PlayerHoldableInstructionContainer").GetComponent<RectTransform>();
			cameraMain = Camera.main;
			testKill.callback = Kill;

			harness.overlayContainer = overlayContainer;
			lookRotation = GameObject.FindWithTag("PlayerVCam").transform;

			HealthFire = config.healthFire.max;
			HealthWater = config.healthWater.max;
		}

		private void Start()
		{
			evSpawn.Play();
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
			UpdateObliterate();
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

			if (nonPlayer.Any(x => x.ignite))
			{
				HealthFire -= config.healthFire.depleteRate * Time.deltaTime;
				avatar.SetHurtEffect(true);
			}
			else
			{
				avatar.SetHurtEffect(false);
			}

			if (nonPlayer.Any(x => x.douse))
			{
				HealthWater -= config.healthWater.depleteRate * Time.deltaTime;
			}

			if (nonPlayer.Any(x => x.regenerate))
			{
				HealthFire -= config.healthRegenHurt;
			}

			healthbar.SetFireCapacity(curFireHealth / config.healthFire.max);
			healthbar.SetWaterCapacity(curWaterHealth / config.healthWater.max);

			if (curFireHealth == 0 || curWaterHealth == 0)
			{
				Kill();
			}
		}

		private void UpdateObliterate()
		{
			if (systemic.effects.Any(x => x.obliterate))
			{
				Kill();
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
			Vector3 vel = rbody.velocity;
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

			vel.y = rbody.velocity.y;

			Vector3 extraGravity = Mathf.Max(0, config.gravityMultiplier - 1) * Physics.gravity;
			vel.y += extraGravity.y * Time.fixedDeltaTime;

			rbody.velocity = vel;

			avatar.SetWalk(vel.x, vel.z);

			curFootstepDist += new Vector2(vel.x, vel.z).magnitude * Time.fixedDeltaTime;
			if (curFootstepDist > config.footstepDist)
			{
				curFootstepDist %= config.footstepDist;

				var ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), Vector3.down);
				var hits = Physics.RaycastAll(ray, 0.4f, config.footstepMask);
				var filtered = hits
					.Where(x => !(x.rigidbody && x.rigidbody == rbody))
					.OrderBy(x => x.distance);

				foreach (RaycastHit h in filtered)
				{
					FootstepSurface surf = null;
					if (h.rigidbody && h.rigidbody.TryGetComponent<FootstepSurface>(out var s))
					{
						surf = s;
					}
					if (surf == null && h.transform.TryGetComponent<FootstepSurface>(out var s2))
					{
						surf = s2;
					}

					if (surf)
					{
						if (config.foostepEnableLogging)
						{
							Debug.Log($"Footstep `{surf.type}` from {surf.name}", surf);
						}

						evFootstep.SetParameter("Type", (int)surf.type);
						evFootstep.Play();
						break;
					}
				}
			}
		}

		private void UpdateLookRotation()
		{
			Vector3 delta = cursorTarget - transform.position;
			delta.y = 0;
			if (delta.sqrMagnitude > 0)
			{
				Quaternion angle = Quaternion.LookRotation(delta.normalized, Vector3.up);
				rbody.MoveRotation(angle);
				//harness.transform.rotation = angle;
			}
		}

		// =========================================================
		// Physics Callbacks
		// =========================================================

		private void OnTriggerStay(Collider other)
		{
			if (other.gameObject.layer == config.regenBonfireLayer)
			{
				HealthFire += config.healthFire.regenerateRate * Time.fixedDeltaTime;
				HealthWater += config.healthWater.regenerateRate * Time.fixedDeltaTime;
			}
		}

		// =========================================================
		// Public API
		// =========================================================

		public void Kill()
		{
			died.Invoke();
			avatar.StartDeathEffects();
			evDeath.Play();
			harness.Detach();
			Destroy(gameObject);
			FindObjectOfType<GameSession>().RespawnAfterDelay();
		}

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