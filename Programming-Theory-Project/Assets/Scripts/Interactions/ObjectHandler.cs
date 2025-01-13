using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{

    [Header("Pickable Object Settings")]
    [SerializeField, InspectorName("Object Z Offset")] private float _objectZOffset;
    [SerializeField, InspectorName("Object Y Offset")] private float _objectYOffset;
    [SerializeField, InspectorName("Object Layer")] private LayerMask _objectLayer;
    [SerializeField, InspectorName("Objects Search Radius")] private float _searchRadius;

    private GameObject pickableObject;

    // Start is called before the first frame update
    void Start()
    {

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

        Collider[] pickableObjects = Physics.OverlapSphere(transform.position, _searchRadius, _objectLayer);

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
            pickableObject.GetComponent<BoxCollider>().enabled = false;
            pickableObject.GetComponent<Rigidbody>().isKinematic = true;
            pickableObject.transform.SetParent(transform);
            Vector3 newPosition = new Vector3(0, _objectYOffset, _objectZOffset);
            pickableObject.transform.localPosition = newPosition;
            pickableObject.transform.localRotation = Quaternion.identity;
        }

    }

    private void DropObject()
    {

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
