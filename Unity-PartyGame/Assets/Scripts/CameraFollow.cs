using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Distance between camera and target
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translationSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float smoothTime = 0.3f;
    private void FixedUpdate() {
        if(target != null) {
            HandleTranslation();
        }
    }

    private void HandleTranslation() {
        transform.position = target.position + offset;
    }
}
