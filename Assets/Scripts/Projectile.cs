using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 100f;

    Vector3 previousPos;

	// Use this for initialization
	void Start () {
        previousPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 disp = previousPos - transform.position;
        Vector3 dir = disp.normalized;
        float distance = disp.magnitude;
        Ray ray = new Ray(transform.position, dir);
        if (Physics.Raycast(ray, distance))
        {
            Hit();
        }

        previousPos = transform.position;
    }

    void Hit()
    {
        // TODO: Deal damage to hit object
    }
}
