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

    //Swing
    public Animator animator;
    private bool isSwinging;
    public float animaitonTime;

    //UI
    public GameObject weaponImage;
    public Text currentAmmoUI;
    public Text maxAmmoUI;

    void Awake()
    {
        weaponImage.SetActive(amIAlive);
        gameObject.SetActive(amIAlive);
    }

    void OnEnable()
    {
        isSwinging = false;
        animator.SetBool("Reloading", false);
    }

    void Start()
    {
        //Assign
        maxAmmo = 0;
        currentAmmo = maxAmmo;

        isSwinging = false;
    }

    void Update()
    {
        //UI
        maxAmmoUI.text = maxAmmo.ToString();
        currentAmmoUI.text = currentAmmo.ToString();

        if (Input.GetButtonDown("Fire1") && isSwinging == false && PlayerHealth.isDead == false)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        isSwinging = true;

        animator.SetBool("Hitting", true); //Start Animation
        yield return new WaitForSeconds(animaitonTime - .25f); //Wait for duration Animation - transition time (.25 by deafult)
        animator.SetBool("Hitting", false); //Finish Animaiton
        yield return new WaitForSeconds(.25f); //Wait transition time

        isSwinging = false;
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
