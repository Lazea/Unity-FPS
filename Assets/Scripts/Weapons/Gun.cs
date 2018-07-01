﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {

    public GameObject projectile;
    ObjectsPool projectilePool;
    public Transform muzzle;
    Transform muzzleFlash;

    public int ammoCapacity = 6;
    public int ammo = 0;

    public bool auto;
    public float fireRate = 0.5f;
    float fireTime = 0f;
    bool firing = false;

    public float reloadRate = 3f;
    float reloadTime = 0f;
    bool reloading = false;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        muzzle = transform.Find("Muzzle");
        muzzleFlash = muzzle.Find("Muzzle Flash");
        SetActiveMuzzleFlash(false);

        projectilePool = gameObject.AddComponent<ObjectsPool> ();
        projectilePool.poolObject = projectile;
        projectilePool.poolObjects = new List<GameObject>();
        for(int i = 0; i < (int)(ammoCapacity * 1.5f); i++)
        {
            GameObject newProjectile = Instantiate(projectile, projectilePool.transform.position, projectilePool.transform.rotation);

            PoolProjectile newPoolProjectile = newProjectile.GetComponent<PoolProjectile>();
            newPoolProjectile.poolObject = newProjectile.GetComponent<PoolObject>();
            if(newPoolProjectile.poolObject == null)
            {
                newPoolProjectile.poolObject = newProjectile.AddComponent<PoolObject>();
            }

            newPoolProjectile.poolObject.pool = projectilePool;
            newPoolProjectile.poolObject.Deactivate();

            projectilePool.InsertObject(newProjectile);
        }
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();

        if(auto)
        {
            fire = fireHold;
        }
        bool reload = Input.GetKeyDown(KeyCode.R);

        // Reload and fire logic
        if (reloading)
        {
            firing = false;

            reloadTime += Time.deltaTime;
            if(reloadTime >= reloadRate)
            {
                reloadTime = 0f;
                reloading = false;

                ammo = ammoCapacity;
            }
        } else if (firing)
        {
            reloading = false;

            fireTime += Time.deltaTime;
            if(fireTime >= fireRate)
            {
                fireTime = 0f;
                firing = false;
            }
        } else
        {
            if (reload)
            {
                Reload();
            }

            if (fire)
            {
                if (ammo > 0)
                {
                    Fire();
                }
                else
                {
                    Reload();
                }
            }
        }
	}

    protected override void Fire()
    {
        firing = true;
        reloading = false;

        SetActiveMuzzleFlash(true);

        GameObject newProjectile = projectilePool.GetNextObject();
        newProjectile.transform.position = muzzle.position;
        newProjectile.transform.rotation = muzzle.rotation;

        PoolProjectile poolProjectile = newProjectile.GetComponent<PoolProjectile>();
        poolProjectile.poolObject.Activate();

        ammo -= 1;

        // TODO: Play fire animation
    }

    void Reload()
    {
        reloading = true;
        firing = false;

        // TODO: Play reload animation
    }

    void SetActiveMuzzleFlash(bool active)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.gameObject.SetActive(active);
        }
    }
}
