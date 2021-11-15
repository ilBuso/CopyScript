using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    //Script
    public static bool amIAlive = false;

    //Weapon
    public float damage;

    //Ammo
    private int maxAmmo;
    private int currentAmmo;

    //UI
    public GameObject weaponImage;
    public Text currentAmmoUI;
    public Text maxAmmoUI;

    void Awake()
    {
        weaponImage.SetActive(amIAlive);
        gameObject.SetActive(amIAlive);
    }

    void Start()
    {
        //Assign
        maxAmmo = 0;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        //UI
        maxAmmoUI.text = maxAmmo.ToString();
        currentAmmoUI.text = currentAmmo.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            ///Enemy
            EnemyHealth health = other.GetComponent<EnemyHealth>();

            if (health != null)
            {
                //Damage
                health.TakeDamage(damage);
            }
        }
    }
}
