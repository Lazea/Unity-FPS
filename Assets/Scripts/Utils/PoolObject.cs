using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {

    public ObjectsPool pool;

    bool active = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Activate()
    {
        active = true;
        gameObject.SetActive(true);
    }

    public void Deactivate(bool returnToPool)
    {
        if(returnToPool)
        {
            pool.InsertObject(this.gameObject);
        }

        active = false;
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
}
