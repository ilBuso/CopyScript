using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //Script
    public ScreenButtons screenButtons;

    //Health
    private float health;
    public float maxHealth;

    //Sheild
    private float sheild;
    public float maxSheild;

    //UI
    public Slider healthSlider;
    public Slider sheildSlider;

    //Die
    public static bool isDead;
    public float waitTime;
    public float waitBeforeMenu;
    public Animator animator;
    public GameObject deadCamera;
    public GameObject winUI;
    private GameObject weaponCamera;

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

        //Die
        isDead = false;
        weaponCamera = GameObject.FindGameObjectWithTag("WeaponCamera");
    }

    void Update()
    {
        //UI
        healthSlider.value = health;
        sheildSlider.value = sheild;

        //Damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(200);
        }
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
                StartCoroutine(Die());
            }
        }
    }

    public IEnumerator Die()
    {
        isDead = true;

        //Ragdoll
        //-----------

        //Wait
        yield return new WaitForSeconds(waitTime);

        //Deactivate UI
        ScreenButtons.healthUI.SetActive(false);
        ScreenButtons.clockUI.SetActive(false);
        ScreenButtons.weaponUI.SetActive(false);
        weaponCamera.SetActive(false);

        //Activate DeadCamera
        deadCamera.SetActive(true);

        //Move camera
        animator.SetBool("Dead", true);

        //Activate UI
        winUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Wait
        yield return new WaitForSeconds(waitBeforeMenu);

        //Variable
        isDead = false;
        animator.SetBool("Dead", false);
        deadCamera.SetActive(false);
        winUI.SetActive(false);

        //Return to MainMenu  //--//if do any changes do in ScreenButtons.cs too//--//
        Time.timeScale = 1f;
        ScreenButtons.isPaused = false;
        SceneManager.LoadScene("Main_Menu");
    }
}