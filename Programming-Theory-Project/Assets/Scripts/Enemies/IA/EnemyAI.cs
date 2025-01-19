using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField, InspectorName("Enemy Path")] private Transform[] _navigationPoints;
    [SerializeField, InspectorName("Player Layer")] private LayerMask _playerLayer;
    [SerializeField, InspectorName("Seconds Before Moving")] private float _timeBeforeMoving;
    [SerializeField, InspectorName("Detection Distance")] private float _detectionDistance;
    [SerializeField, InspectorName("Catch Distance")] private float _catchDistance;

    [HideInInspector] public int currentPoint;
    [HideInInspector] public float timeLastMovement;
    [HideInInspector] public NavMeshAgent agent;

    private Ray leftRay;
    private Ray midRay;
    private Ray rightRay;

    public void Patrol()
    {
        //Waits before moving to the next position
        if (timeLastMovement > _timeBeforeMoving)
        {
            if (currentPoint < _navigationPoints.Length)
            {
                //Moves to the next position
                agent.SetDestination(_navigationPoints[currentPoint].position);
                currentPoint++;
                timeLastMovement = 0;
            }
            else
            {
                //Sends the initial position as next position
                currentPoint = 0;
            }
        }
        else
        {
            timeLastMovement += Time.deltaTime;
        }
    }

    //Like patrol but you can set another speed for the enemy
    public void Patrol(float agentSpeed)
    {
        //Waits before moving to the next position
        if (timeLastMovement > _timeBeforeMoving)
        {
            if (currentPoint < _navigationPoints.Length)
            {
                //Moves to the next position
                agent.speed = agentSpeed;
                agent.SetDestination(_navigationPoints[currentPoint].position);
                currentPoint++;
                timeLastMovement = 0;
            }
            else
            {
                //Sends the initial position as next position
                currentPoint = 0;
            }
        }
        else
        {
            timeLastMovement += Time.deltaTime;
        }
    }

    //You can override this method
    public virtual void LookForPlayer()
    {
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Sets 3 rays in front of the enemy to detect the player
        leftRay = new Ray(rayOrigin, -transform.right + transform.forward);
        midRay = new Ray(rayOrigin, transform.forward);
        rightRay = new Ray(rayOrigin, transform.right + transform.forward);

        if (CheckRayHit(leftRay) || CheckRayHit(midRay) || CheckRayHit(rightRay))
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        //Moves the enemy towards the player
        Transform playerTransform = GameObject.Find("Player").transform;
        agent.SetDestination(playerTransform.position);

        //If the enemy catches the player, the game ends
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, _catchDistance, _playerLayer);
        if (playerCollider.Length > 0)
        {
            GameManager.instance.GameOver();
        }
    }

    private bool CheckRayHit(Ray ray)
    {
        return Physics.Raycast(ray, _detectionDistance, _playerLayer);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(1f, 0f, 0f, .5f);

        Gizmos.DrawRay(leftRay);
        Gizmos.DrawRay(midRay);
        Gizmos.DrawRay(rightRay);

        Gizmos.DrawSphere(transform.position, _catchDistance);

    }

}
