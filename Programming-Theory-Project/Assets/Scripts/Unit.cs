using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField, InspectorName("Camera Offset")] private Vector3 _cameraOffset;
    [SerializeField, InspectorName("Speed Multiplier")] private float _speedMultiplier;

    private Camera gameCamera;
    private NavMeshAgent playerAgent;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = GameObject.FindObjectOfType<Camera>();
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void LateUpdate()
    {
        CameraMovement();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MoveUnit();
        }
        playerAnimator.SetFloat("Speed", Mathf.Ceil(Mathf.Abs(playerAgent.velocity.x + playerAgent.velocity.z)));

    }

    private void MoveUnit()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            playerAgent.SetDestination(hit.point);
        }
    }

    private void CameraMovement()
    {
        Vector3 delayedPosition = Vector3.Lerp(gameCamera.transform.position, transform.position + _cameraOffset, _speedMultiplier);
        gameCamera.transform.position = delayedPosition;
    }

}
