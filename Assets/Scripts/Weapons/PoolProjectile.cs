using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolProjectile : Projectile {

    public float lifeTime = 0f;
    float elapsedTime = 0f;

    public PoolObject poolObject;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        elapsedTime = 0f;
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();

        // Object has a limited lifetime if lifeTime != 0
        if (poolObject.IsActive() && lifeTime > 0f)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= lifeTime)
            {
                elapsedTime = 0f;

                DestroyProjectile();   // Deactivate and return to the pool
            }
        }
    }

    protected override void DestroyProjectile()
    {
        elapsedTime = 0f;
        poolObject.Deactivate(true);
    }
}
