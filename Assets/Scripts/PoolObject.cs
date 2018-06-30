using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {

    ObjectsPool pool;

    bool active = false;
    bool deactivateGameObject = true;

    public float lifeTime = 0f;
    float time = 0f;

    Transform objectSpawnPoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Object has a limited lifetime if lifeTime != 0
        if (active && lifeTime > 0f)
        {
            time += Time.deltaTime;

            if (time >= lifeTime)
            {
                time = 0f;

                Deactivate();   // Return to the pool
            }
        }
	}

    // Sets the bool and object spawn point
    public void SetPool(ObjectsPool pool)
    {
        this.pool = pool;
        this.objectSpawnPoint = this.pool.objectSpawnPoint;
    }

    // Activates the object and moves it to the spawn position. Optionally the GameObject itself can be set active
    public void Activate(bool activateGameObject)
    {
        active = true;
        gameObject.SetActive(activateGameObject);
        deactivateGameObject = activateGameObject;

        transform.position = objectSpawnPoint.position;
        transform.rotation = objectSpawnPoint.rotation;
    }

    // Auto activate the object and its GameObject
    public void Activate()
    {
        Activate(true);
    }

    // Deactivates the object and moves it to the pool position. Optionally the GameObject itself can be deactivated
    public void Deactivate(bool activateGameObject)
    {
        active = false;
        gameObject.SetActive(activateGameObject);

        transform.position = pool.transform.position;
        transform.rotation = pool.transform.rotation;
    }

    // Auto deactivate the object and its GameObject
    public void Deactivate()
    {
        if (deactivateGameObject)
        {
            Deactivate(false);
        } else
        {
            Deactivate(true);
        }
    }
}
