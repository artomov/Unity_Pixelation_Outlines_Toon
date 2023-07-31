using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool shouldRotateObjects;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Transform> objectsToRotateList;

    void Update()
    {
        if (!shouldRotateObjects) return;

        foreach (Transform transform in objectsToRotateList)
        {
            transform.Rotate(Vector3.up*Time.deltaTime*rotationSpeed, Space.World);
        }

    }
}
