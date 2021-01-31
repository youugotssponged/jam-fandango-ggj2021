using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public Transform patrolPoint;
    public GameObject view;
    public Material[] materials;
    public bool onHighAlert = false;
    public float chaseSpeed = 6;

    public float huntRadius = 4;

    private float searchTime = 3f;
    private MeshRenderer viewMeshRenderer;

    //private bool isAlive = true;

    private NavMeshAgent nav;
    private EnemyFieldOfView enemyFOV;
    private PatrolPoint pp;
    public AI.ai_states ai;

    private void Start()
    {   
        nav = GetComponent<NavMeshAgent>();
        enemyFOV = GetComponentInChildren<EnemyFieldOfView>();
        pp = FindObjectOfType<PatrolPoint>();
        nav.speed = 2f;
        viewMeshRenderer = view.GetComponent<MeshRenderer>();
        viewMeshRenderer.material = materials[0];
    }

    private void Update()
    {
        switch (ai)
        {
            case AI.ai_states.idle:
                IdleState();
                break;

            case AI.ai_states.patrolling:
                PatrolState();
                break;

            case AI.ai_states.hunting:
                HuntState();
                break;

            case AI.ai_states.searching:
                SearchState();
                break;

            case AI.ai_states.chasing:
                ChaseState();
                break;

            case AI.ai_states.attacking:
                AttackState();
                break;

            default:
                break;
        }

        if (enemyFOV.visibleTargets.Count > 0)
        {
            transform.LookAt(playerTransform.position);
            onHighAlert = true;
            ai = AI.ai_states.chasing;
        }

    }

    void IdleState()
    {
        Vector3 randomPos = Random.insideUnitSphere * pp.patrolRadius;
        Vector3 randomAlertedPos = Random.insideUnitSphere * huntRadius;
        NavMeshHit hit;

        if (onHighAlert)
        {
            if (huntRadius >= 10f)
            {
                viewMeshRenderer.material = materials[0];
                onHighAlert = false;
                nav.speed = 2f;
                huntRadius = 4f;
                ai = AI.ai_states.idle;
            }

            NavMesh.SamplePosition(playerTransform.transform.position + randomAlertedPos, out hit, 20f, NavMesh.AllAreas);

            nav.SetDestination(hit.position);

            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                huntRadius += 2f;
            }
            ai = AI.ai_states.hunting;
        }

        if (!onHighAlert)
        {
            huntRadius = 4f;
            NavMesh.SamplePosition(patrolPoint.transform.position + randomPos, out hit, 20f, NavMesh.AllAreas);
            nav.SetDestination(hit.position);
            ai = AI.ai_states.patrolling;
        }
    }

    void PatrolState()
    {
        if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
        {
            searchTime = 4f;
            ai = AI.ai_states.searching;
        }
    }

    void HuntState()
    {
        viewMeshRenderer.material = materials[1];
        if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
        {
            searchTime = 2f;
            //huntRadius = 5f;
            ai = AI.ai_states.searching;
        }
    }

    void SearchState()
    {
        if (searchTime > 0f && !onHighAlert)
        {
            searchTime -= Time.deltaTime;
            transform.Rotate(0f, 120f * Time.deltaTime, 0f);
        }
        else if (searchTime > 0f && onHighAlert)
        {
            searchTime -= Time.deltaTime;
            transform.Rotate(0f, 200f * Time.deltaTime, 0f);
        }
        else
        {
            ai = AI.ai_states.idle;
        }
    }

    void ChaseState()
    {
        viewMeshRenderer.material = materials[2];
        nav.speed = 4f;
        nav.SetDestination(playerTransform.transform.position);

        if (nav.remainingDistance <= 2f)
        {
            nav.speed = 0f;
        }
        else
        {
            nav.speed = chaseSpeed;
        }

        if (enemyFOV.visibleTargets.Count <= 0)
        {
            ai = AI.ai_states.hunting;
        }
    }

    void AttackState()
    {

    }
}
