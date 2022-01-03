using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    /// <summary>
    ///  This Script contains all the mechanics for every throwable, the things not here are in thee dedicated script of the gun
    /// </summary>

    //Script
    public static bool isUsingNade = true;

    //Throw
    public float throwForce;
    private GameObject throwablePosition;
    public GameObject GranadePrefab;
    public GameObject C4Prefab;
    private GameObject prefab;

    //Ammo
    private int nadeNumber;
    public int maxNade;
    private int cNumber;
    public int maxC;
    public static bool hasThrown;

    void Start()
    {
        //Assign
        throwablePosition = GameObject.FindGameObjectWithTag("ThrowablePosition");

        nadeNumber = maxNade;
        cNumber = maxC;
        hasThrown = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !hasThrown && ScreenButtons.isPaused == false && PlayerHealth.isDead == true &&((nadeNumber > 0 && cNumber == maxC) || (nadeNumber == maxNade && cNumber > 0)))
        {
            if (isUsingNade)
            {
                prefab = GranadePrefab;

                nadeNumber--;
            }
            else
            {
                prefab = C4Prefab;

                cNumber--;
                hasThrown = true;
            }

            //Throw
            GameObject granade = Instantiate(prefab, throwablePosition.transform.position, Quaternion.identity);

            granade.GetComponent<Rigidbody>().AddForce(throwablePosition.transform.forward * throwForce);
        }
    }
}
