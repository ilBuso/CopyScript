using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    ///*Buttons*///
    
    //Primary
    public void Rifl()
    {
        Rifle.amIAlive = true;

        Shotgun.amIAlive = false;

        Sniper.amIAlive = false;
    }

    public void Shotgu()
    {
        Rifle.amIAlive = false;

        Shotgun.amIAlive = true;

        Sniper.amIAlive = false;
    }

    public void Snipe()
    {
        Rifle.amIAlive = false;

        Shotgun.amIAlive = false;

        Sniper.amIAlive = true;
    }

    //Secondary
    public void Pistol()
    {
        Pistola.amIAlive = true;

        DesertEagle.amIAlive = false;

        Knife.amIAlive = false;
    }

    public void DesertEagl()
    {
        Pistola.amIAlive = false;

        DesertEagle.amIAlive = true;

        Knife.amIAlive = false;
    }

    public void Knif()
    {
        Pistola.amIAlive = false;

        DesertEagle.amIAlive = false;

        Knife.amIAlive = true;
    }

    //Throwable
    public void Nade()
    {
        Thrower.isUsingNade = true;
    }

    public void C4()
    {
        Thrower.isUsingNade = false;
    }
}
