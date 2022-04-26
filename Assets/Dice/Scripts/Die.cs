using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dice
{
    public class Die : MonoBehaviour
    {
        enum AxisColors { }
        Rigidbody rb;
        public enum sides { Right, Up, Front, Left, Down, Back };
        public sides TopSide;
        public int Value;
        readonly Vector3[] sideVectors = { Vector3.right, Vector3.up, Vector3.forward, Vector3.right*-1, Vector3.up*-1, Vector3.forward*-1 };
        public Vector3[] worldSides = new Vector3[6];
        public Vector3 topVec;


        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            topVec = Vector3.zero;
            for(int i = 0; i < sideVectors.Length; i++)
            {
                worldSides[i] = transform.TransformPoint(sideVectors[i]);

                if(worldSides[i].y > topVec.y)
                {
                    Debug.Log("Update");
                    topVec = worldSides[i];
                    TopSide = (sides)i;
                    UpdateValue();
                }
            }
        }

        void UpdateValue()
        {
            switch(TopSide)
            {
                case sides.Up:
                    Value = 5;
                    break;
                case sides.Front:
                    Value = 1;
                    break;
                case sides.Left:
                    Value = 2;
                    break;
                case sides.Back:
                    Value = 3;
                    break;
                case sides.Right:
                    Value = 4;
                    break;
                case sides.Down:
                    Value = 6;
                    break;
            }
        }

        void OnMouseDown()
        {
            Roll();
        }

        void Roll()
        {
            InstantRoll();
            rb.AddExplosionForce(Random.Range(10f, 20f), transform.position, .5f, 1f, ForceMode.Impulse);
            rb.angularVelocity = Random.onUnitSphere * Random.Range(5f, 10f);
            //rb.angularVelocity = new Vector3(Random.Range(10f, 20f), Random.Range(10f, 20f), Random.Range(10f, 20f));
        }

        void InstantRoll()
        {
            transform.Rotate(Random.Range(0, 4) * 90f, Random.Range(0, 4) * 90f, Random.Range(0, 4) * 90f);
        }

        void OnDrawGizmos()
        {
            Color gizmoColor = Color.white;
            Gizmos.DrawLine(transform.position, topVec*2);
            Gizmos.DrawSphere(topVec*2, .1f);
            Vector3 lineDir = Vector3.zero;

            for(int i = 0; i < 6; i++)
            {
                switch(i)
                {
                    case 0:
                    gizmoColor = Color.red;
                        break;
                    case 1:
                    gizmoColor = Color.green;
                        break;
                    case 2:
                    gizmoColor = Color.blue;
                        break;
                    case 3:
                    gizmoColor = Color.cyan;
                        break;
                    case 4:
                    gizmoColor = Color.magenta;
                        break;
                    case 5:
                    gizmoColor = Color.yellow;
                        break;
                    default:
                        Debug.Log("Die DrawGizmos defaulted!");
                        break;
                }
                
                Gizmos.color = gizmoColor;
                Gizmos.DrawLine(transform.position, worldSides[i]);
                Gizmos.DrawSphere(worldSides[i], .1f);
            }
        }
    }
}