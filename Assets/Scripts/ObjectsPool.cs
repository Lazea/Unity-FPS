using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour {

    public GameObject poolObject;
    public int poolSize = 0;
    int count = 0;

    public List<GameObject> poolObjects;

    public Transform objectSpawnPoint;

    int currIndex;

	// Use this for initialization
	void Start () {
        // Initial pool object construction
        poolObjects = new List<GameObject>(poolSize);
        for(int i = 0; i < poolSize; i++)
        {
            AddObject(poolObject);
        }

        currIndex = poolObjects.Count - 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Inserts a new specific object at a specific location in the pool list. Also returns the new object.
    public GameObject AddObject(int index, GameObject obj)
    {
        GameObject newObject = Instantiate(obj, transform.position, transform.rotation);
        PoolObject newPoolObject = newObject.GetComponent<PoolObject>();
        if (newPoolObject != null)
        {
            newPoolObject.SetPool(this);
        }

        if (index >= poolObjects.Count)
        {
            poolObjects.Add(newObject);
        }
        else
        {
            poolObjects.Insert(index, newObject);
        }

        return newObject;
    }

    // Inserts a new specific object at the end of the pool list. Also returns the new object.
    public GameObject AddObject(GameObject obj)
    {
        return AddObject(poolObjects.Count, obj);
    }

    // Returns the pool object at the specific index.
    public GameObject PeekNextObject()
    {
        if (currIndex >= 0 && currIndex < poolObjects.Count)
        {
            GameObject obj = PeekObject(currIndex);
            currIndex -= 1;

            return obj;
        }

        currIndex = poolObjects.Count;

        return null;
    }

    // Returns the pool object at the specific index.
    public GameObject PeekObject(int index)
    {
        if (index < poolObjects.Count)
        {
            return poolObjects[index];
        }

        return null;
    }

    // Returns the pool object at the end of the pool list.
    public GameObject PeekObject()
    {
        return PeekObject();
    }

    // Retrieves the pool object at the specific index and removes it from the pool list.
    public GameObject GetObject(int index)
    {
        GameObject obj = PeekObject(index);
        RemoveObject(index);
        return obj;
    }

    // Retrieves the pool object at the end of the pool list and removes it from the pool list.
    public GameObject GetObject()
    {
        return GetObject(poolObjects.Count - 1);
    }

    // Removes the pool object at the specific index of the pool list.
    public void RemoveObject(int index)
    {
        if(index < poolObjects.Count)
        {
            poolObjects.RemoveAt(index);
        }
    }

    // Removes the pool object at the end of the pool list.
    public void RemoveObject()
    {
        RemoveObject(poolObjects.Count - 1);
    }

    // Removes and destroys the pool object at the specific index of the pool list.
    public void DestroyObject(int index)
    {
        if (index < poolObjects.Count)
        {
            GameObject obj = GetObject(index);
            Destroy(obj);
        }
    }

    // Removes and destroys the pool object at the end of the pool list.
    public void DestroyObject()
    {
        DestroyObject(poolObjects.Count - 1);
    }
}
