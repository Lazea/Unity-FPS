using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public int damage = 10;

    public GameObject projectile;
    ObjectsPool projectilePool;
    public Transform muzzle;

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
	void Start () {
        muzzle = transform.Find("Muzzle");

        projectilePool = gameObject.AddComponent<ObjectsPool> ();
        projectilePool.poolObject = projectile;
        projectilePool.poolObjects = new List<GameObject>();
        for(int i = 0; i < (int)(ammoCapacity * 1.5f); i++)
        {
            GameObject newProjectile = Instantiate(projectile, projectilePool.transform.position, projectilePool.transform.rotation);

            PoolObject newPoolProjectile = newProjectile.GetComponent<PoolObject>();
            if(newPoolProjectile == null)
            {
                newPoolProjectile = newProjectile.AddComponent<PoolObject>();
            }
            newPoolProjectile.pool = projectilePool;
            newPoolProjectile.Deactivate();

            projectilePool.InsertObject(newProjectile);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Input
        bool fire = Input.GetMouseButtonDown(0);
        if (auto)
        {
            fire = Input.GetMouseButton(0);
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

    void Fire()
    {
        firing = true;
        reloading = false;

        GameObject newProjectile = projectilePool.PeekNextObject();
        newProjectile.transform.position = muzzle.position;
        newProjectile.transform.rotation = muzzle.rotation;

        PoolObject poolProjectileObject = newProjectile.GetComponent<PoolObject>();
        if(poolProjectileObject == null)
        {
            poolProjectileObject = newProjectile.AddComponent<PoolObject>();
        }
        poolProjectileObject.Activate();
        poolProjectileObject.SetLifeTime(5f);

        ammo -= 1;

        // TODO: Play fire animation
    }

    void Reload()
    {
        reloading = true;
        firing = false;

        // TODO: Play reload animation
    }
}
