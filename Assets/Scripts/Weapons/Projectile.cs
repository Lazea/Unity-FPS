using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 100f;

    // Use this for initialization
    protected virtual void Start () {

    }
	
	// Update is called once per frame
	protected virtual void Update () {
        float distance = speed * Time.deltaTime;
        transform.position += transform.forward * distance;

        Ray ray = new Ray(transform.position, -transform.forward);
        if (Physics.Raycast(ray, distance))
        {
            DestroyProjectile();
        }
    }

    protected virtual void Hit()
    {
        DealDamage();
        DestroyProjectile();
    }

    protected virtual void DealDamage()
    {
        // TODO: Deal damage to hit object
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
