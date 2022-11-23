using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAndResources.Dice
{
    public class RbExplode : MonoBehaviour
    {
        public float explosionForce;
        Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.AddExplosionForce(explosionForce, transform.position + Random.insideUnitSphere, .5f, 1f, ForceMode.Impulse);
            _rb.AddTorque(Random.Range(0f, 1f) * explosionForce, Random.Range(0f, 1f) * explosionForce, Random.Range(0f, 1f) * explosionForce, ForceMode.Impulse);
        }
    }
}