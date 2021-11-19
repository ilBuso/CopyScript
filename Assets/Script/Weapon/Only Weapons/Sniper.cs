using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
{
    //Script
    public static bool amIAlive = false;
    public Gun gun;
    public ScreenButtons screenButtons;

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

    //Aim
    public static bool isAiming;

    public GameObject scopeOverlay;
    private GameObject weaponCamera;
    private Camera mainCamera;
    public float scopedFOV;
    private float normalFOV;

    //UI
    public GameObject weaponImage;
    public Text currentAmmoUI;
    public Text maxAmmoUI;

    void Awake()
    {
        weaponImage.SetActive(amIAlive);
        gameObject.SetActive(amIAlive);
        scopeOverlay.SetActive(amIAlive);
    }

    void OnEnable()
    {
        //Reload
        isReloading = false;
        animator.SetBool("Reloading", false);

        //Aim
        scopeOverlay.SetActive(false);
        isAiming = false;
        screenButtons.crosshairUI.SetActive(false);
    }

    void Start()
    {
        //Assign
        camera = Camera.main;

        gunEnd = GameObject.FindGameObjectWithTag("GunEnd").transform;

        laserLine = GetComponent<LineRenderer>();

        currentAmmo = maxAmmo;
        isReloading = false;

        isAiming = false;
        mainCamera = Camera.main;
        weaponCamera = GameObject.FindGameObjectWithTag("WeaponCamera");
    }

    void Update()
    {
        //UI
        maxAmmoUI.text = maxAmmo.ToString();
        currentAmmoUI.text = currentAmmo.ToString();

        //Aim
        if (Input.GetButtonDown("Fire2") && !isReloading && ScreenButtons.isPaused == false)
        {
            isAiming = !isAiming;

            if (isAiming)
            {
                StartCoroutine(OnScoped());
            }
            else
            {
                OnUnScoped();
            }
        }

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
            if (isAiming)
            {
                isAiming = !isAiming;
                OnUnScoped();
            }

            StartCoroutine(Reload());
        }
    }

    private void OnDisable()
    {
        isReloading = false;
        screenButtons.crosshairUI.SetActive(true);
    }


    //Reload
    private IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true); //Start Animation
        yield return new WaitForSeconds(reloadTime - .25f); //Wait for duration Animation - transition time (.25 by deafult)
        animator.SetBool("Reloading", false); //Finish Animaiton
        yield return new WaitForSeconds(.25f); //Wait transition time

        currentAmmo = maxAmmo; //Reset ammonition

        isReloading = false;
    }

    //Recoil
    private IEnumerator Recoil()
    {
        transform.localPosition -= backMovement;

        yield return new WaitForSeconds(recoilTime);

        transform.localPosition += backMovement;
    }

    //Aim
    IEnumerator OnScoped()
    {
        //animator.SetBool("Scopeed", isAiming);        //-------------------------
        yield return new WaitForSeconds(0.15f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        //Zoom
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;

        //UI
        ScreenButtons.healthUI.SetActive(!isAiming);
        ScreenButtons.clockUI.SetActive(!isAiming);
        ScreenButtons.weaponUI.SetActive(!isAiming);
    }

    void OnUnScoped()
    {
        //animator.SetBool("Scopeed", isAiming);        //-------------------------

        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        //Zoom
        mainCamera.fieldOfView = normalFOV;

        //UI
        ScreenButtons.healthUI.SetActive(!isAiming);
        ScreenButtons.clockUI.SetActive(!isAiming);
        ScreenButtons.weaponUI.SetActive(!isAiming);
    }
}
