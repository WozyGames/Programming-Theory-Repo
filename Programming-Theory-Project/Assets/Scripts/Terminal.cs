using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    [Header("Pickable Object Settings")]
    [SerializeField, InspectorName("Object Y Offset")] private float _objectOffset;
    [SerializeField, InspectorName("Rotation Speed")] private float _rotationSpeed;

    private GameManager gameManager;
    private GameObject pickableObject;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pickableObject != null)
        {
            RotateObject();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            pickableObject = other.gameObject;
            ShowObjectAtTop();
            gameManager.CheckActiveTerminals();
        }
    }

    private void ShowObjectAtTop()
    {
        pickableObject.GetComponent<BoxCollider>().enabled = false;
        pickableObject.GetComponent<Rigidbody>().isKinematic = true;
        pickableObject.transform.SetParent(transform);
        pickableObject.transform.localPosition = Vector3.up * _objectOffset;
        pickableObject.transform.localRotation = Quaternion.identity;
    }

    private void RotateObject()
    {
        pickableObject.transform.Rotate(Vector3.up * _rotationSpeed);
    }

}
