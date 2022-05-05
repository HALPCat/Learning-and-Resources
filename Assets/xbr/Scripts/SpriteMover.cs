using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace xBR
{
    public class SpriteMover : MonoBehaviour
    {
        public bool SlowDiagonals = false;
        public int PixelsPerUnit = 120;
        public float PixelsPerSecond = 1f;
        private float _pps;
        float _factor;// = 1f/(120f);
        [SerializeField]
        float _timeSinceLastMove;

        void Awake()
        {
            _factor = 1f/(float)PixelsPerUnit;
        }

        void Update()
        {
            _pps = PixelsPerSecond;
            Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(SlowDiagonals)
            {
                if(inputVector.x != 0 && inputVector.y != 0)
                {
                    _pps = PixelsPerSecond * 0.707f;
                }
            }
            
            if(_timeSinceLastMove < 1/_pps)
            {
                _timeSinceLastMove += Time.deltaTime;
            }else{
                _timeSinceLastMove = 1/_pps;
            }

            if(_timeSinceLastMove >= 1/_pps && inputVector != Vector2.zero)
            {
                Vector3 newPos = transform.position;
                newPos.x += _factor * Mathf.Round(inputVector.x);
                newPos.y += _factor * Mathf.Round(inputVector.y);
                transform.position = newPos;
                _timeSinceLastMove = 0f;
            }
        }
    }
}