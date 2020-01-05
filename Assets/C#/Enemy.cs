using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("傷害"), Range(0, 100)]
    public float damage = 20;

    public Transform checkPoint;

    private Rigidbody2D r2d;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "玩家")
        {
            Track(collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "玩家" && collision.transform.position.y < transform.position.y +1)
        {
            collision.gameObject.GetComponent<Character>().Damage(damage);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, -checkPoint.up * 1.5f);
    }

    private void Move()
    {
        r2d.AddForce(-transform.up * speed);
        
        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, -checkPoint.up, 1.5f, 1 << 8);

        if (hit == true)
        {
            transform.eulerAngles += new Vector3(0, 0, 180);
            
        }

    }

    private void Track(Vector3 target)
    {
        if (target.y < transform.position.y)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }



}
