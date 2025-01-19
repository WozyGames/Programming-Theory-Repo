using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyAI
{
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

}
