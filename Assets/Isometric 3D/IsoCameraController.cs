using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCameraController : MonoBehaviour
{
    public float RotationSpeed = 90f;
    float[] _cameraPositions = { 0f, 90f, 180f, 270f };
    private int _rotIndex = 0;
    private int _rotDir = 0;
    private float _rotSpeed, _targetRotY, _rotDelta = 0f;
    [SerializeField]
    private bool _moving = false;
    Vector2 _inputVector;

    void Update()
    {
        UpdateInput();
        UpdateCamera();
        RotateCamera();
    }

    void UpdateCamera()
    {
        if(_moving)
        {
            return;
        }

        if(_inputVector.x != 0)
        {
            _moving = true;
            if(_inputVector.x > 0)
            {
                _rotDir = 1;
                _rotIndex++;
                if(_rotIndex > _cameraPositions.Length-1)
                {
                    _rotIndex = 0;
                }
            }else if(_inputVector.x < 0)
            {
                _rotDir = -1;
                _rotIndex--;
                if(_rotIndex < 0)
                {
                    _rotIndex = _cameraPositions.Length-1;
                }
            }
            _targetRotY = _cameraPositions[_rotIndex];
            //Debug.Log("_rotIndex" + _rotIndex + ", _targetRotY = " + _targetRotY + ", euler Y: " + transform.rotation.eulerAngles.y);
        }
    }

    void RotateCamera()
    {
        if(!_moving)
        {
            return;
        }
        
        _rotSpeed = _rotDir * RotationSpeed * Time.deltaTime;
        _rotDelta += Mathf.Abs(_rotSpeed);
        transform.Rotate(0f, _rotSpeed, 0f, Space.World);

        if(_rotDelta >= 90f)
        {
            Vector3 cEulers = transform.rotation.eulerAngles;
            cEulers.y = _targetRotY;
            transform.rotation = Quaternion.Euler(cEulers);
            _moving = false;
            _rotDelta = 0f;
        }
    }

    void UpdateInput()
    {
        _inputVector.x = Input.GetAxisRaw("Horizontal");
        _inputVector.y = Input.GetAxisRaw("Vertical");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
