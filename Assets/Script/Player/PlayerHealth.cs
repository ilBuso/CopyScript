using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Health
    private float health;
    public float maxHealth;

    //Sheild
    private float sheild;
    public float maxSheild;

    //UI
    public Slider healthSlider;
    public Slider sheildSlider;

    void Start()
    {
        //Health
        health = maxHealth;
        sheild = maxSheild;

        //UI
        healthSlider.maxValue = health;
        healthSlider.value = health;

        sheildSlider.maxValue = sheild;
        sheildSlider.value = sheild;
    }

    void Update()
    {
        //UI
        healthSlider.value = health;
        sheildSlider.value = sheild;
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
        Debug.Log("You Died");
    }
}
