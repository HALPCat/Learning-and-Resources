using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KFMovement
{
    public class KFController : MonoBehaviour
    {
        [Header("Movement")]
        public float MaxSpeed = 1f;
        public float Acceleration = 1f;
        public float Deacceleration = 1f;
        public float RotationSpeed = 45f;

        [Header("Camera")]
        public float CameraHeight = 1.6f;
        public float CameraSwaySpeed = 1f;
        public float CameraSwayMagnitude = .1f;

        private float _lastActiveY = 0f;
        private Vector2 _inputVector;
        private float _currentSpeed = 0f;
        private float _distanceMoved = 0f;

        private Transform _cameraTransform;
        private Animator _armAnimator;

        void Start()
        {
            _cameraTransform = transform.GetChild(0);
            _armAnimator = _cameraTransform.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            UpdateInputVector();
            UpdateSpeed();
            UpdateRotation();
            UpdateMovement();
            UpdateCamera();

            _armAnimator.SetBool("Attack", Input.GetButton("Fire1"));
        }

        void UpdateSpeed()
        {
            if(_inputVector.y != 0)
            {
                _currentSpeed += Acceleration * Time.deltaTime;
            }else{
                _currentSpeed -= Deacceleration * Time.deltaTime;
            }
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, MaxSpeed);
        }

        void UpdateInputVector()
        {
            _inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(_inputVector.y != 0)
            {
                _lastActiveY = _inputVector.y;
            }
        }

        void UpdateRotation()
        {
            transform.Rotate(0f, _inputVector.x * RotationSpeed * Time.deltaTime, 0f);
        }

        void UpdateMovement()
        {
            transform.Translate(transform.forward * _lastActiveY * _currentSpeed * Time.deltaTime, Space.World);
            _distanceMoved += _currentSpeed * Time.deltaTime;
        }

        void UpdateCamera()
        {
            Vector3 cameraAnchor = transform.position + Vector3.up * CameraHeight;
            _cameraTransform.position = cameraAnchor + new Vector3(0f, Mathf.Sin(_distanceMoved * CameraSwaySpeed), 0f) * CameraSwayMagnitude;
        }
    }
}