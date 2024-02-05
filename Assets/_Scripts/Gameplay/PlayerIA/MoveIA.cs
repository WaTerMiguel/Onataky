using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveIA : MonoBehaviour
{
    [SerializeField] DetectarInimigos detect;
    public Transform target;
    Vector3 directionMove;
    [SerializeField] NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        directionMove = target.transform.position.normalized;
        directionMove.x = directionMove.x * -1f;
        directionMove.y = 0f;
        directionMove.z = directionMove.z * -1f;

        agent.SetDestination(target.position * -1f);

    }
}
