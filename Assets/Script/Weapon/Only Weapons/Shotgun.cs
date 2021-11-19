using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{
    //Script
    public static bool amIAlive = false;
    public Gun gun;

    //Camera
    private new Camera camera;
    private Transform gunEnd;

    //VFX
    private LineRenderer laserLine;

    //Values
    public float range;
    public float damage;
    public float hitForce;
    public float reloadTime;

    public float fireRate;
    private float nextTimeToFire;

    //Ammo
    public int maxAmmo;
    private int currentAmmo;

    private bool isReloading;
    public Animator animator;

    //Recoil
    public Vector3 backMovement;
    public float recoilTime;

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
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Start()
    {
        //Assign
        camera = Camera.main;

        gunEnd = GameObject.FindGameObjectWithTag("GunEnd").transform;

        laserLine = GetComponent<LineRenderer>();
        nextTimeToFire = 0f;

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Update()
    {
        //UI
        maxAmmoUI.text = maxAmmo.ToString();
        currentAmmoUI.text = currentAmmo.ToString();

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire && ScreenButtons.isPaused == false && !isReloading)
        {
            //Shoot
            int i = 0;
            while (i != 8)
            {
                gun.Shoot(camera, gunEnd, laserLine, damage, range, hitForce);

                i++;
            }
            nextTimeToFire = Time.time + 1f / fireRate;

            //Recoil
            StartCoroutine(Recoil());


            currentAmmo--; //Decrese ammonition by one because you havve shooted
        }
        
        //Reload
        if (((Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) || currentAmmo == 0) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void OnDisable()
    {
        isReloading = false;
    }

    //Reload
    private IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true); //Start Animation
        yield return new WaitForSeconds(reloadTime - .25f); //Wait for duration Animation - transition time (.25 by deafult)
        animator.SetBool("Reloading", false); //Finish Animaiton
        yield return new WaitForSeconds(.25f); //Wait transition time

        currentAmmo = maxAmmo;

        isReloading = false;
    }


    //Recoil
    private IEnumerator Recoil()
    {
        transform.localPosition -= backMovement;

        yield return new WaitForSeconds(recoilTime);

        transform.localPosition += backMovement;
    }
}
