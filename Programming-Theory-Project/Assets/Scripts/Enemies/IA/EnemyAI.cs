using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField, InspectorName("Enemy Path")] private Transform[] _navigationPoints;
    [SerializeField, InspectorName("Seconds Before Moving")] private float _timeBeforeMoving;

    [Header("Detection Settings")]
    [SerializeField, InspectorName("Player Layer")] private LayerMask _playerLayer;
    [SerializeField, InspectorName("View Y Offset")] private float _viewHeight;
    [SerializeField, InspectorName("Detection Distance")] private float _detectionDistance;
    [SerializeField, InspectorName("Catch Distance")] private float _catchDistance;

    protected int currentPoint;
    protected float timeLastMovement;
    protected NavMeshAgent agent;
    protected Animator enemyAnimator;

    private Ray leftRay;
    private Ray midRay;
    private Ray rightRay;

    public void Patrol()
    {
        if (!GameManager.instance.isGameOver)
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
            enemyAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        }
    }

    //Like patrol but you can set another speed for the enemy
    public void Patrol(float agentSpeed)
    {
        if (!GameManager.instance.isGameOver)
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
            enemyAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        }
    }

    //You can override this method
    public virtual void LookForPlayer()
    {
        if (!GameManager.instance.isGameOver)
        {
            Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + _viewHeight, transform.position.z);
            //Sets 3 rays in front of the enemy to detect the player
            leftRay = new Ray(rayOrigin, -transform.right + transform.forward);
            midRay = new Ray(rayOrigin, transform.forward);
            rightRay = new Ray(rayOrigin, transform.right + transform.forward);

            if (CheckRayHit(leftRay) || CheckRayHit(midRay) || CheckRayHit(rightRay))
            {
                ChasePlayer();
            }
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
            enemyAnimator.SetTrigger("Catch");
            Unit playerScript = playerCollider[0].GetComponent<Unit>();
            playerScript.enabled = false;
            GameManager.instance.GameOver();
        }
    }

    private bool CheckRayHit(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, _detectionDistance) && hit.transform.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawRay(leftRay);
        Gizmos.DrawRay(midRay);
        Gizmos.DrawRay(rightRay);

        Gizmos.color = new Color(1f, 0f, 0f, .5f);

        Gizmos.DrawSphere(transform.position, _catchDistance);

    }

}
