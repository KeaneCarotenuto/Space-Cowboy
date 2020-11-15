using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public enum GunTypes // your custom enumeration
    {
        Semi,
        Burst,
        Auto
    };
    public GunTypes type;

    [Tooltip("Rounds Per Second")]
    [Range(0.0f, 100.0f)] public float rps;

    public float timeOfShot;
    public bool canShoot;

    public GameObject projectile;
    
    void Start()
    {
        rps = 1 / rps;
        timeOfShot = Time.time - rps;
        canShoot = true;
    }

    void Update()
    {
        if (Time.time - timeOfShot >= rps)
        {
            if ((type == GunTypes.Semi && !Input.GetMouseButton(0)) || type == GunTypes.Auto)
            {
                canShoot = true;
            }
        }
        else
        {
            canShoot = false;
        }

        if (canShoot && Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (type == GunTypes.Semi)
        {
            canShoot = false;
        }

        timeOfShot = Time.time;
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(bullet.transform.right * 1000);
    }
}
