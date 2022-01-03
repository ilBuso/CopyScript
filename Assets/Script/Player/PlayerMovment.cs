using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
    /// <summary>
    /// This Script is for movement inputs + movement speed and form + stamina
    /// </summary>

    //Player
    private Rigidbody rb;
    private GameObject player;

    //Ground Check
    private Transform groundCheck;
    private float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;

    //Movment
    private Vector3 input;
    public float walkSpeed;
    public float crouchSpeed;
    public float runSpeed;
    public float dashSpeed;

    //Jump
    public float jumpForce;
    private bool isJumping;
    
    //Run
    private bool isRunning;
    
    //Crouch
    public static bool isCrouched;

    //Dash
    private bool isDashing;
    private float savedDashSpeed;
    public float slowingMultiplier;

    //Stamina
    public Slider slider;
    private float stamina;
    public float maxStamina;
    private float minStaminaToRun;
    public float percentangeOfMinStaminaToRun;

    //
    public bool isMoving;

    void Start()
    {
        //Assign
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
        isMoving = false;
        //
        //slider = GameObject.FindGameObjectWithTag("StaminaSlider").GetComponent<Slider>();

        //GroundCheck
        groundDistance = 0.25f;

        //Jump
        isJumping = false;

        //Run
        isRunning = false;

        //Crouch
        isCrouched = false;

        //Dash
        isDashing = false;
        savedDashSpeed = dashSpeed;

        //Stamina
        minStaminaToRun = (percentangeOfMinStaminaToRun * maxStamina) / 100;
        stamina = maxStamina;

        //UI
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    void Update()
    {
        //UI
        slider.value = stamina;

        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        
        if(isGrounded)
        {
            isJumping = false;
        }

        //Reset Dash
        if(dashSpeed <= 0f || stamina <= 0f)
        {
            isDashing = !isDashing;

            dashSpeed = savedDashSpeed;
            isDashing = false;

            isRunning = false;

            //player.transform.Translate(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            player.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Movement
        ///Input
        input = Vector3.zero; //for the input a vector3(0f, 0f, 0f) is created in for x and y is putted the mouse input so we have all the WASD inputs in a single variable
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        ///Walk
        if (!isCrouched && !isRunning && !isDashing)
        {
            Movement(walkSpeed);
        }

        ///Run
        if (!isCrouched && isRunning && !isDashing)
        {
            Movement(runSpeed);
        }

        ///Crouch
        if (isCrouched && !isRunning && !isDashing)
        {
            Movement(crouchSpeed);
        }

        ///Dash
        if (!isCrouched && isRunning && isDashing)
        {
            dashSpeed -= (Time.deltaTime * slowingMultiplier);

            Movement(dashSpeed);
        }

        //Check for movement
        if (input.normalized.x == 0f && input.normalized.z == 0f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        //Debug.Log(isMoving);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouched && !isDashing && stamina > minStaminaToRun)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isJumping = true;
            isDashing = false;
        }

        //Run
        if ((Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isCrouched && stamina > minStaminaToRun) || (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isCrouched && stamina < minStaminaToRun && isRunning))
        {
            isRunning = !isRunning;
        }

        //Crouch
        if (((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C)) && isGrounded && !isDashing) || (Input.GetButtonDown("Jump") && isCrouched && !isDashing))
        {
            if(!isCrouched)
            {
                player.transform.localScale = new Vector3(1f, .5f, 1f);
            }
            else
            {
                player.transform.localScale = new Vector3(1f, 1f, 1f);
            }

            isCrouched = !isCrouched;
            isRunning = false;
        }
        
        
        //Dash
        if (Input.GetKeyDown(KeyCode.F) && isRunning && !isDashing && !isCrouched)
        {
            Dash();
        }
        

        //Stamina
        if (isRunning)
        {
            if (stamina > 0f)
            {
                stamina -= Time.deltaTime;

                if (isJumping)
                {
                    stamina -= minStaminaToRun; //So its 20% of the total stamina
                }

                if (isDashing)
                {
                    stamina -= Time.deltaTime;
                }
            }
            if (stamina <= 0f)
            {
                isRunning = false;
                isDashing = false;
            }
        }
        else
        {
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime;
            }

            if (isJumping)
            {
                stamina -= minStaminaToRun; //So its 20% of the total stamina
            }
        }
    }

    private void Movement(float speed)
    {
        //Movement
        rb.MovePosition(rb.position + (transform.right * input.normalized.x + transform.forward * input.normalized.z) * speed * Time.deltaTime);
    }

    //Dash
    private void Dash()
    {
        isDashing = !isDashing;
        player.transform.localScale = new Vector3(1f, 0.5f, 1f);
    }
}