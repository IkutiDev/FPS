using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float attackRange = 1f;
    private bool isProvoked = false;
    private NavMeshAgent navMeshAgent;

    private float distanceToTarget=Mathf.Infinity;
    private static readonly int Move = Animator.StringToHash("move");
    private static readonly int Attack = Animator.StringToHash("attack");

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        
        distanceToTarget=Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        
        GetComponent<Animator>().SetBool(Attack,true);
        Debug.Log("Git attacked ya biscuit!");
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool(Attack, false);
        GetComponent<Animator>().SetTrigger(Move);
        navMeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
    }
}
