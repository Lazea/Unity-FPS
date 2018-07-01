using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour {

    public GameObject poolObject;
    public int poolSize = 0;
    int count = 0;
    int currIndex = 0;

    public List<GameObject> poolObjects;

	// Use this for initialization
	void Start () {
        // Initial pool object construction
        if (poolObjects == null)
        {
            poolObjects = new List<GameObject>(poolSize);
            if (poolObject != null)
            {
                for (int i = 0; i < poolSize; i++)
                {
                    InsertObject();
                }

                currIndex = poolObjects.Count - 1;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InsertObjectAt(int index, GameObject obj)
    {
        if(index >= 0 && index <= count)
        {
            if(index < count)
            {
                poolObjects.Insert(index, obj);
            } else
            {
                poolObjects.Add(obj);
            }
        } else
        {
            poolObjects.Add(obj);
        }

        count += 1;
    }

    public void InsertObjectAt(int index)
    {
        GameObject newObject = Instantiate(poolObject, transform.position, transform.rotation);

        InsertObjectAt(index, newObject);
    }

    public void InsertObject(GameObject obj)
    {
        InsertObjectAt(poolObjects.Count, obj);
    }

    public void InsertObject()
    {
        InsertObjectAt(poolObjects.Count);
    }

    public GameObject PeekObject(int index)
    {
        if (index >= 0 && index < poolObjects.Count)
        {
            GameObject obj = poolObjects[index];
            return obj;
        } if (poolObjects.Count > 0)
        {
            GameObject obj = poolObjects[poolObjects.Count - 1];
            return obj;
        } else
        {
            return null;
        }
    }

    public GameObject PeekNextObject()
    {
        GameObject obj = PeekObject(currIndex);

        if (obj != null)
        {
            currIndex -= 1;
            if (currIndex < 0)
            {
                currIndex = poolObjects.Count - 1;
            }
        }

        return obj;
    }

    public GameObject GetObject(int index)
    {
        GameObject obj = PeekObject(index);
        if(obj != null)
        {
            poolObjects.RemoveAt(index);

            count -= 1;
        }

        return obj;
    }

    public GameObject GetNextObject()
    {
        return GetObject(poolObjects.Count - 1);
    }
}
