using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject BulletPrefab;
    public int Damage = 20;
    public float Cooldown;
    private float _attackTimer;
    public float Range;
    public Enemy Target;

    float _smallestDist = 0f;
    
    void Update()
    {
        UpdateCooldown();
        UpdateTarget();
        if(CanFire())
        {
            Fire();
        }
    }

    private void UpdateTarget()
    {
        if(GameManager.Instance.Enemies.Length == 0)
        {
            return;
        }
        _smallestDist = 100f;
        foreach(Enemy e in GameManager.Instance.Enemies)
        {
            float distance = Vector3.Distance(e.transform.position, transform.position);
            if(distance <= Range && distance < _smallestDist)
            {
                _smallestDist = distance;
                Target = e;
            }
        }
    }

    private void Fire()
    {
        if(Target != null)
        {
            GameObject go = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            go.GetComponent<Bullet>().Initialize(transform.position, Target, Damage);
            _attackTimer = 0;
        }
    }

    private void UpdateCooldown()
    {
        if(_attackTimer < Cooldown)
        {
            _attackTimer += Time.deltaTime;
        }else{
            _attackTimer = Cooldown;
        }
    }

    private bool CanFire()
    {
        return _attackTimer >= Cooldown;
    }
}
