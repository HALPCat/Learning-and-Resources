using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public Vector3[] ShapePoints = new Vector3[3];

    Vector3 GetBezierPoint(float t)
    {
        Vector3 a = Vector3.Lerp(ShapePoints[0], ShapePoints[1], t);
        Vector3 b = Vector3.Lerp(ShapePoints[1], ShapePoints[2], t);
        Vector3 c = Vector3.Lerp(a, b, t);
        return c;
    }

    void OnDrawGizmos()
    {
        if(ShapePoints.Length <= 2)
        {
            return;
        }

        int detail = 10;
        //Vector3.Lerp(points[0], Vector3.Lerp(points[1], points[2], t), t);

        /*
        for(int i = 0; i < points.Length; i++)
        {
            if(i < points.Length-1)
            {
                Gizmos.DrawLine(transform.position + points[i], transform.position + points[i+1]);
            }
        }
        */
        for(int i = 0; i <= detail; i++)
        {
            float t = (float)i / (float)detail;
            Vector3 c = GetBezierPoint(t);
            Gizmos.DrawSphere(transform.position + c, .1f);
        }
    }
}
