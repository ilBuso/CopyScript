using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    //Granade
    public float radius;
    public float damage;

    //Countdown
    public float startTime;
    private float countdown;

    //VFX
    public GameObject explosioEffect;

    void Start()
    {
        //Assign
        countdown = startTime;
    }

    void Update()
    {
        //Countdown
        countdown -= Time.deltaTime;

        //Explosion
        if (countdown <= 0f)
        {
            Explode();
        }
    }

    //Explosion
    void Explode()
    {
        Instantiate(explosioEffect, transform.position, transform.rotation);

        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in objectsInRange)
        {
            EnemyHealth target = collider.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    //VFX
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

