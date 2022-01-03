using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /// <summary>
    /// This Script contains all the mechanics for every gun, the things not here (as reload....) are in thee dedicated script of the gun
    /// </summary>

    public static GameObject hitCrosshair;
    public static float weaponRange;
    public GameObject muzzleFlash;
    public GameObject hitWall;
    public GameObject hitEnemy;
    public GameObject bulletMark;

    //Recoil
    private Quaternion angle;
    public float minOffset;
    public float maxOffset;
    public static int shotFires;
    private float time;
    public float timeToNormalRecoil;
    private bool StartStopwatch;

    void Start()
    {
        //Assign
        hitCrosshair = GameObject.FindGameObjectWithTag("HitCrosshair");
        hitCrosshair.SetActive(false);
        
        angle = Quaternion.identity;
        
        time = 0f;
        StartStopwatch = false;
    }

    void Update()
    {
        //Recoil
        if (StartStopwatch)
        {
            time += Time.deltaTime;
        }

        if (WeaponSwitch.switched == true)
        {
            StartStopwatch = false;
            shotFires = 0;
            time = 0f;
        }
    }

    public void Shoot(Camera gunCamera, Transform gunEnd, LineRenderer laserLine, float damage, float range, float hitForce)
    {
        //Recoil
        if (!StartStopwatch)
        {
            StartStopwatch = true;
        }

        //Raycast
        Vector3 rayOrigin = gunCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));  //Crate vector for raycast
        RaycastHit hit; //Declare raycast to store info

        //VFX
        weaponRange = range;
        laserLine.SetPosition(0, gunEnd.position);

        GameObject particleSystemMuzzle = Instantiate(muzzleFlash, gunEnd.position, Quaternion.LookRotation(gunEnd.forward));
        Destroy(particleSystemMuzzle, 2f);
        
        //Recoil
        if (time <= timeToNormalRecoil)
        {
            shotFires++;
            time = 0f;
        }
        else
        {
            StartStopwatch = false;

            if (Shotgun.amIAlive == false)
            {
                shotFires = 0;
            }
            else
            {
                shotFires = 3;
            }

            time = 0f;
        }

        if (shotFires >= 4 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>().isMoving == true)
        {
            if (PlayerMovment.isCrouched == true)
            {
                angle.eulerAngles = new Vector3(Random.Range(minOffset / 2, maxOffset / 2), Random.Range(minOffset / 2, maxOffset / 2), Random.Range(minOffset / 2, maxOffset / 2));
            }
            else
            {
                angle.eulerAngles = new Vector3(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset));
            }
        }
        else
        {
            angle = Quaternion.identity;
        }

        //Hit
        if (Physics.Raycast(rayOrigin, angle * gunCamera.transform.forward, out hit, range)) //true if hit something
        {
            //VFX
            laserLine.SetPosition(1, hit.point);
            StartCoroutine(LaserLine(laserLine));

            ///Enemy
            EnemyHealth health = hit.collider.GetComponentInParent<EnemyHealth>(); //GetComponentInParent because he ave to search the script all the way in the first game object

            if (health != null)
            {
                //Damage
                health.TakeDamage(damage);

                //UI
                StartCoroutine(HitCrossahir());

                //VFX
                GameObject particleSystemEnemy = Instantiate(hitEnemy, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(particleSystemEnemy, 2f);
            }
            else
            {
                if (hit.transform.tag != "BlueWall")
                {
                    //VFX
                    GameObject particleSystemWall = Instantiate(hitWall, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(particleSystemWall, 2f);

                    GameObject bulletMarkSprite = Instantiate(bulletMark, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                    Destroy(bulletMarkSprite, 10f);
                }
            }

            ///Object
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (Camera.main.transform.forward * weaponRange));
        }
    }

    private IEnumerator LaserLine(LineRenderer laserLine)
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.05f);
        laserLine.enabled = false;
    }

    private IEnumerator HitCrossahir()
    {
        hitCrosshair.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitCrosshair.SetActive(false);
    }
}