using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAndResources.TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        int HP = 100;
        private Waypoint _prevPoint;
        public Waypoint Target;

        float _timer;
        float _distToTarget;
        public float MovementSpeed = 1f;

        private void Start()
        {
            if(_prevPoint == null)
            {
                _prevPoint = GameManager.Instance.Waypoints[0];
            }
            UpdateTarget();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            float t = _timer / (_distToTarget / MovementSpeed);

            if(t >= 1f)
            {
                t = 0f;
                UpdateTarget();
            }
            transform.position = Vector3.Lerp(_prevPoint.transform.position, Target.transform.position, t);
        }

        void UpdateTarget()
        {
            //Debug.Log("UpdateTarget");
            if(Target == null)
            {
                Target = GameManager.Instance.Waypoints[1];
                _prevPoint = GameManager.Instance.Waypoints[0];
                _distToTarget = Vector3.Distance(_prevPoint.transform.position, Target.transform.position);
                return;
            }

            _timer = 0f;
            for(int i = 0; i < GameManager.Instance.Waypoints.Length; i++)
            {
                // Target found
                if(Target == GameManager.Instance.Waypoints[i])
                {
                    //if last
                    if(i == GameManager.Instance.Waypoints.Length-1)
                    {
                        Die();
                    }else{
                        _prevPoint = Target;
                        Target = GameManager.Instance.Waypoints[i+1];
                        _distToTarget = Vector3.Distance(_prevPoint.transform.position, Target.transform.position);
                    }
                    i = GameManager.Instance.Waypoints.Length;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if(HP <= 0)
            {
                Die();
            }
        }
        
        void Die()
        {
            GameManager.Instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}