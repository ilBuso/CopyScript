using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    /// <summary>
    /// This Script is to set the dropdown for the maps in the Main_Menu
    /// </summary>

    private string map;

    void Start()
    {

    }

    void Update()
    {

    }


    ///*Buttons*////
    //Quit
    public void Quit()
    {
        Application.Quit();
    }

    //Play
    public void Play()
    {
        SceneManager.LoadScene(map);
    }


    //Maps
    public void DropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                map = "Warm_Up";
                break;

            case 1:
                map = "Office";
                break;

            case 2:
                map = "Train_Station";
                break;

            case 3:
                map = "Crash_Site";
                break;
        }
    }
}
