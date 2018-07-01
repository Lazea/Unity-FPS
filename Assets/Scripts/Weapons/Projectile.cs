using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 100f;

    Vector3 previousPos;

    // Use this for initialization
    protected virtual void Start () {
        previousPos = transform.position;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 disp = previousPos - transform.position;
        Vector3 dir = disp.normalized;
        float distance = disp.magnitude;
        Ray ray = new Ray(transform.position, dir);
        if (Physics.Raycast(ray, distance))
        {
            DestroyProjectile();
        }

        previousPos = transform.position;
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
