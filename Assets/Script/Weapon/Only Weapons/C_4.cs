using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_4 : MonoBehaviour
{
    private GameObject player;

    //Granade
    public float radiusExplosion;
    public float radiusActivation;
    public float damage;

    //VFX
    public GameObject explosioEffect;

    void Start()
    {
        //Assign
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Explosion
        if (Input.GetKeyDown(KeyCode.G) && radiusActivation >= Vector3.Distance(player.transform.position, transform.position) && Thrower.hasThrown == true && ScreenButtons.isPaused == false)
        {
            Explode();

            Thrower.hasThrown = false;
        }
    }

    //Explosion
    void Explode()
    {
        Instantiate(explosioEffect, transform.position, transform.rotation);

        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radiusExplosion);
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
        Gizmos.DrawWireSphere(transform.position, radiusExplosion);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusActivation);

    }
}
