using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Weapon {

    public float lifeTime = 0f;
    public Timer timer;

    bool active = false;

    // Use this for initialization
    protected override void Start() {
        base.Start();

        timer = new Timer();
        timer.lifeTime = lifeTime;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        if (active)
        {
            if (timer.Tick())
            {
                Fire();
            }
        } else
        {
            if(fire)
            {
                active = true;
            }
        }
    }

    protected override void Fire()
    {
        Explode();
    }

    void Explode()
    {
        // Spawn Explosion FX
        Destroy(gameObject);
    }
}
