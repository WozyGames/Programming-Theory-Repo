using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField, InspectorName("Enemy Path")] private Transform[] _navigationPoints;
    [SerializeField, InspectorName("Seconds Before Moving")] private float _timeBeforeMoving;    
    [SerializeField, InspectorName("Detection Distance")] private float detectionDistance;
    [SerializeField, InspectorName("Player Layer")] private LayerMask playerLayer;

    private int currentPoint;
    private float timeLastMovement;
    private NavMeshAgent agent;    

    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        timeLastMovement = 0;
        agent = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {

        Patrol();
        LookForPlayer();
    }

    private void Patrol()
    {
        if (timeLastMovement > _timeBeforeMoving)
        {
            if (currentPoint < _navigationPoints.Length)
            {
                agent.SetDestination(_navigationPoints[currentPoint].position);
                currentPoint++;
            }
            else
            {
                currentPoint = 0;
            }
            timeLastMovement = 0;
        }
        else
        {
            timeLastMovement += Time.deltaTime;
        }
    }

    private void LookForPlayer()
    {

        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ray = new Ray(rayOrigin, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance, playerLayer))
        {
            Debug.Log("hit");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray);
    }

}
