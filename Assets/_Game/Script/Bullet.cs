using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public ParticleSystem hitVFX;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ApplyDame(damage, transform.position);
        }

        if (!other.CompareTag("Enemy"))
        {
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
