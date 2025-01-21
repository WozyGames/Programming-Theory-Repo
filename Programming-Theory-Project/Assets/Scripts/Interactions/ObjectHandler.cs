using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{

    [Header("Pickable Object Settings")]
    [SerializeField, InspectorName("Object Z Offset")] private float _objectZOffset;
    [SerializeField, InspectorName("Object Y Offset")] private float _objectYOffset;
    [SerializeField, InspectorName("Object Layer")] private LayerMask _objectLayer;
    [SerializeField, InspectorName("Objects Search Radius")] private float _searchRadius;

    private GameObject pickableObject;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickableObject == null)
            {
                GrabOject();
            }
            else
            {
                DropObject();
            }
        }

    }

    private GameObject FindNearestObject()
    {
        //Search for objects around the player in a specific radius
        Collider[] pickableObjects = Physics.OverlapSphere(transform.position, _searchRadius, _objectLayer);

        //Returns the first object it finds
        foreach (Collider pickableObject in pickableObjects)
        {
            return pickableObject.gameObject;
        }
        return null;

    }

    private void GrabOject()
    {

        pickableObject = FindNearestObject();

        if (pickableObject != null)
        {

            playerAnimator.SetTrigger("Grab");

            //Turns off the object's physics and attaches it to the player
            pickableObject.GetComponent<BoxCollider>().enabled = false;
            pickableObject.GetComponent<Rigidbody>().isKinematic = true;
            //Set the object's new position and resets its rotation
            pickableObject.transform.SetParent(transform);
            Vector3 newPosition = new Vector3(0, _objectYOffset, _objectZOffset);
            pickableObject.transform.localPosition = newPosition;
            pickableObject.transform.localRotation = Quaternion.identity;
        }

    }

    private void DropObject()
    {
        playerAnimator.SetTrigger("Drop");

        //Turns on the object's physics and detaches it from the player
        pickableObject.GetComponent<BoxCollider>().enabled = true;
        pickableObject.GetComponent<Rigidbody>().isKinematic = false;
        pickableObject.transform.SetParent(null);
        pickableObject = null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _searchRadius);
    }

}
