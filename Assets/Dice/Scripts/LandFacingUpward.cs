using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAndResources.Dice
{
    [RequireComponent(typeof(Rigidbody))]
    public class LandFacingUpward : MonoBehaviour
    {
        public LayerMask layerMask;
        public float maxDistance = 5f;
        public Vector3 up = Vector3.up;
        public float Kp = 100;
        public float Kd = 20;

        Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if(Physics.Raycast(_rb.position, Physics.gravity, out RaycastHit hitInfo, maxDistance, layerMask))
            {
                Quaternion.FromToRotation(_rb.rotation * up, hitInfo.normal).ToAngleAxis(out float angle, out Vector3 axis);
                Vector3 err = Mathf.Deg2Rad * angle * axis;
                _rb.AddTorque(Kp * err - Kd * _rb.angularVelocity, ForceMode.Acceleration);
            }
        }
    }
}