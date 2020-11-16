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

    [Header("Stats")]
    public GunTypes fireType;

    [Tooltip("Rounds Per Second")]
    [Range(0.0f, 100.0f)] public float rps;

    [Tooltip("Spread (In Degrees)")]
    [Range(0.0f, 90.0f)] public float spread;



    [Header("Projectiles")]
    [Tooltip("Projectiles Per Shot")]
    [Range(1.0f, 10.0f)] public int pps;

    [Tooltip("Damage Miltiplier")]
    [Range(0.0f, 10.0f)] public float dmgMulti;

    [Tooltip("Projectile Shot Force")]
    [Range(0.0f, 10000.0f)] public float pForce;

    [Tooltip("Projectile Scale")]
    public Vector2 pScale;

    [Tooltip("Projectile Scale")]
    public List<BulletScript.BulletEffect> effects;

    public GameObject projectile;



    [Header("Parts")]
    public GameObject barrel;
    public GameObject handle;

    [HideInInspector] public bool isEquipped;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public float timeOfShot;

    void Start()
    {
        rps = 1 / rps;
        timeOfShot = Time.time - rps;
        canShoot = true;
    }

    void Update()
    {
        if (isEquipped && Time.time - timeOfShot >= rps)
        {
            if ((fireType == GunTypes.Semi && !Input.GetMouseButton(0)) || fireType == GunTypes.Auto)
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

        if (Mathf.Abs(transform.rotation.eulerAngles.z) > 90 && Mathf.Abs(transform.rotation.eulerAngles.z) < 270)
        {
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }

        if (isEquipped)
        {
            Vector2 _pos = GetComponent<GunScript>().handle.transform.localPosition;
            transform.localPosition = new Vector3(_pos.x * -transform.localScale.x, _pos.y * -transform.localScale.y);
        }
    }

    private void Shoot()
    {
        if (fireType == GunTypes.Semi)
        {
            canShoot = false;
        }

        timeOfShot = Time.time;

        for (int i = 0; i < pps; i++)
        {
            GameObject bullet = Instantiate(projectile, barrel.transform.position, transform.rotation);
            
            bullet.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-1.0f, 1.0f) * spread));
            bullet.transform.localScale = pScale;

            bullet.GetComponent<BulletScript>().damage *= dmgMulti;

            foreach (BulletScript.BulletEffect _effect in effects)
            {
                if (!bullet.GetComponent<BulletScript>().effects.Contains(_effect))
                {
                    bullet.GetComponent<BulletScript>().effects.Add(_effect);
                }
            }

            Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
            bulletBody.AddForce(bullet.transform.right * pForce);
        }
        
    }
}
