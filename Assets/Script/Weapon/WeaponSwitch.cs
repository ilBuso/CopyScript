using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    /// <summary>
    ///  This Script is for switching between primary, secondary and knife
    /// </summary>

    //Variable
    private int selectedWeapon;
    public static bool switched;

    void Start()
    {
        //Assign
        selectedWeapon = 0;

        //Set first weapon
        SelectWeapon();
    }

    void Update()
    {
        int previusSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1)) //if 1 is pressed
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) //if 2 is pressed
        {
            selectedWeapon = 1;
        }

        if (previusSelectedWeapon != selectedWeapon) //if the weapon selected is different from before
        {
            SelectWeapon();
            switched = true;
        }
        else
        {
            switched = false;
        }
    }

    void SelectWeapon()
    {
        //Variable
        int i = 0;

        //Switching
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
