using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Health
    private float health;
    public float maxHealth;

    //Sheild
    private float sheild;
    public float maxSheild;

    void Start()
    {
        //Health
        health = maxHealth;
        sheild = maxSheild;
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        //decrease sheild based on the damage imported from the weapon script
        sheild -= damage;

        if (sheild <= 0f) //if sheild reach 0xp
        {
            //decrease life based on the damage imported from the weapon script
            health -= damage;

            if (health <= 0f) //if life reach 0xp
            {
                //die
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Enemy Died");
    }
}
