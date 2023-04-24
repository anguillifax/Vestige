using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestingForLagging : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", 1f, 0.2f, Time.deltaTime);
        nav.SetDestination(target.position);

    }
    public void testforanimation()
    {
        
    }
}
