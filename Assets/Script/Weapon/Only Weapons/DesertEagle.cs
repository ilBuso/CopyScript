using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesertEagle : MonoBehaviour
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

    //Recoil
    public Vector3 recoilMovement;
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

    void Start()
    {
        //Assign
        camera = Camera.main;

        gunEnd = GameObject.FindGameObjectWithTag("GunEnd").transform;

        laserLine = GetComponent<LineRenderer>();

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
            gun.Shoot(camera, gunEnd, laserLine, damage, range, hitForce);
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

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;

        isReloading = false;
    }


    //Recoil
    private IEnumerator Recoil()
    {
        transform.localEulerAngles += recoilMovement;

        yield return new WaitForSeconds(recoilTime);

        transform.localEulerAngles -= recoilMovement;
    }
}