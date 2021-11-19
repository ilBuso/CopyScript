using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenButtons : MonoBehaviour
{
    //Pause-Resume
    public static bool isPaused; //is static because of the input sistem so when is paused the camera doesn't move
    private GameObject pauseScreenUI;

    //UI
    public static GameObject healthUI;
    public static GameObject weaponUI;
    public static GameObject clockUI;
    public GameObject crosshairUI;

    void Start()
    {
        //Assign
        pauseScreenUI = GameObject.FindGameObjectWithTag("PauseScreen");
        healthUI = GameObject.FindGameObjectWithTag("HealthUI");
        weaponUI = GameObject.FindGameObjectWithTag("WeaponUI");
        clockUI = GameObject.FindGameObjectWithTag("ClockUI");

        //Pause
        pauseScreenUI.SetActive(false);

    }

    void Update()
    {
        //Stop match if "escape" is pressede 
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (isPaused)
            {
                //resume gameplay
                Resume();
            }
            else
            {
                //stop gameplay
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Pause
        pauseScreenUI.SetActive(false); //deactivate pause screen
        Time.timeScale = 1f; //resume time
        isPaused = false;

        //Cursor
        Cursor.lockState = CursorLockMode.Locked; //cursor get loked in the middle of the screen
        Cursor.visible = false; //cursor become invisible

        //OtherUI
        healthUI.SetActive(true);
        weaponUI.SetActive(true);
        clockUI.SetActive(true);
    }

    private void Pause()
    {
        //Pause
        pauseScreenUI.SetActive(true); //activate pause screen
        Time.timeScale = 0f; //stop time
        isPaused = true;

        //Cursor
        Cursor.lockState = CursorLockMode.None; //cursor in no more loked in the middle of the screen
        Cursor.visible = true; //cursor become visible

        //OtherUI
        healthUI.SetActive(false);
        weaponUI.SetActive(false);
        clockUI.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; //resume time
        isPaused = false;
        SceneManager.LoadScene("Main_Menu"); // whewn the botton is pressed the schene "Main_Menu" gets loaded
    }
}
