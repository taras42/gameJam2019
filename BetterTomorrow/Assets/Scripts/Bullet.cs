using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        CharacterBehaviour enemy = hitInfo.GetComponent<CharacterBehaviour>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }

        Destroy(gameObject);
    }
}
