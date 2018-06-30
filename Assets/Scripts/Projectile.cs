using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 100f;

    Vector3 previousPos;

    public float lifeTime = 5f;
    float elapsedTime = 0f;

	// Use this for initialization
	void Start () {
        previousPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float tm = Time.deltaTime;
        transform.position += transform.forward * speed * tm;

        Vector3 disp = previousPos - transform.position;
        Vector3 dir = disp.normalized;
        float distance = disp.magnitude;
        Ray ray = new Ray(transform.position, dir);
        if (Physics.Raycast(ray, distance))
        {
            Hit();
        }

        if(lifeTime > 0)
        {
            elapsedTime += tm;
            if (elapsedTime >= lifeTime)
            {
                DestroyProjectile();
            }
        }

        previousPos = transform.position;
    }

    void Hit()
    {
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        PoolObject poolObject = gameObject.GetComponent<PoolObject>();
        if (poolObject != null)
        {
            poolObject.Deactivate(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
