using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder
{

    // The context is a shared object every node has access to.
    // Commonly used components and subsytems should be stored here
    // It will be somewhat specfic to your game exactly what to add here.
    // Feel free to extend this class 
    public class Context
    {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody physics;
        public NavMeshAgent agent;
        public SphereCollider sphereCollider;
        public BoxCollider boxCollider;
        public CapsuleCollider capsuleCollider;
        public CharacterController characterController;
        // Add other game specific systems here
        public EnemyManager manager;


        public static Context CreateFromGameObject(GameObject gameObject)
        {
            // Fetch all commonly used components
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.physics = gameObject.GetComponent<Rigidbody>();
            context.agent = gameObject.GetComponent<NavMeshAgent>();
            context.sphereCollider = gameObject.GetComponent<SphereCollider>();
            context.boxCollider = gameObject.GetComponent<BoxCollider>();
            context.capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            context.characterController = gameObject.GetComponent<CharacterController>();
            context.manager = gameObject.GetComponent<EnemyManager>();

            // Add whatever else you need here...

            return context;
        }
        public void playAnimation(string animationName, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.Play(animationName);
        }
        #region Special effects
        public void CheckIsFired()
        {
            if (manager.isFired)
            {
                SetOnFire();
            }
        }
        public void SetOnFire()
        {
            if (!manager.isFiredPlayed)
            {
                playAnimation("catchFire", true);
                manager.TurnOnFire();
                manager.isFiredPlayed = true;
                agent.enabled = false;
            }
        }
        public bool PlayerInSight()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, manager.detectionRadius, manager.playerMask);
            for (int i = 0; i < colliders.Length; i++)
            {

                Transform player = colliders[i].transform;
                // DP_CharacterStats characterStats = colliders[i].transform.GetComponent<DP_CharacterStats>();
                if (player != null && player.tag == "Player")
                {
                    Vector3 targetDirection = player.position - transform.position;
                    float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewAbleAngle < manager.maxDetectionAngle && viewAbleAngle > manager.minDetectionAngle)
                    {
                        return true;

                    }
                }
            }
            return false;

        }


        #endregion

    }
}