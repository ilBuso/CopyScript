using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    //Wall Countdown
    private GameObject spawnWall;
    private GameObject countdownWallClock;
    private float countdownWall;
    public float maxCountdownWall;

    //Time Passed In Game
    private float seconds;
    private float minutes;
    public float maxTime;  //-----------

    //UI
    public Text wallCountdownUI;
    public Text seconsdsUI;
    public Text minutesUI;

    void Start()
    {
        //Assign
        spawnWall = GameObject.FindGameObjectWithTag("SpawnWall");
        countdownWallClock = GameObject.FindGameObjectWithTag("Countdown");

        //Time Passed In Game
        seconds = 0f;
        minutes = 0f;

        //Wall Countdown
        countdownWall = maxCountdownWall;
    }

    void Update()
    {
        //UI
        wallCountdownUI.text = countdownWall.ToString();
        TheClock(seconds, minutes);


        //Wall Countdown
        if (SceneManager.GetActiveScene().name != "Warm_Up")
        {
            if (countdownWall >= 0f)
            {
                countdownWall -= Time.deltaTime;
            }
            if (countdownWall < 0) //when countdown is below 0 or in tutorial
            {
                spawnWall.SetActive(false); //bluewall desapear
                countdownWallClock.SetActive(false); //countdown UI disapear
            }
        }
        else
        {
            countdownWallClock.SetActive(false); //countdown UI disapear
        }

        //Time Passed In Game
        if (countdownWall < 0f) //if countdown is below 0
        {
            // start clock
            if (seconds < 60f)
            {
                seconds += Time.deltaTime;
            }
            else
            {
                if (minutes < maxTime)
                {
                    seconds = 0f;
                    minutes++;
                }
                else
                {
                    //EndGame()  //-----------
                }
            }
        }
    }

    private void TheClock(float second, float minute)
    {
        //transform valiue from float to int
        int intSecond = (int)second;
        int intMinute = (int)minute;

        //UI
        seconsdsUI.text = intSecond.ToString();
        minutesUI.text = intMinute.ToString();
    }
}
