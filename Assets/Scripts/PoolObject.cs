using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {

    public ObjectsPool pool;

    bool active = false;

    float lifeTime = 0f;
    float elapsedTime = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Object has a limited lifetime if lifeTime != 0
        if (active && lifeTime > 0f)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= lifeTime)
            {
                elapsedTime = 0f;

                Deactivate(true);   // Deactivate and return to the pool
            }
        }
	}

    public void Activate()
    {
        active = true;
        elapsedTime = 0f;
        gameObject.SetActive(true);
    }

    public void Deactivate(bool returnToPool)
    {
        if(returnToPool)
        {
            pool.InsertObject(this.gameObject);
        }

        active = false;
        elapsedTime = 0f;
        gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        Deactivate(false);
    }

    public bool IsActive()
    {
        return active;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void SetLifeTime(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }
}
