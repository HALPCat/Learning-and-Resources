using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 0;
    public float Speed = 1f;
    private Vector3 _startPos;
    private Vector3 _endPos;
    public Enemy Target;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            _endPos = Target.transform.position + Vector3.up;
        }
        _timer += Time.deltaTime;
        float t = _timer * Speed;
        transform.position = Vector3.Lerp(_startPos, _endPos, t);
        if(t >= 1)
        {
            if(Target != null)
                Target.TakeDamage(Damage);

            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Initialize(Vector3 origin, Enemy target, int damage)
    {
        _startPos = origin;
        Target = target;
        _endPos = target.transform.position;
        Damage = damage;
    }
}
