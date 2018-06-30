using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public int damage = 10;

    public Projectile projectile;

    public int ammoCapacity = 5;
    int ammo = 0;

    public bool auto;
    public float fireRate = 0.1f;
    float fireTime = 0f;
    bool firing = false;

    public float reloadRate = 3f;
    float reloadTime = 0f;
    bool reloading = false;

	// Use this for initialization
	void Start () {
		
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
