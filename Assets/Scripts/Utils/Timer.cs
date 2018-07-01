using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float lifeTime = 0f;
    float time;

    public enum EndState
    {
        destroy,
        disable
    }

    public EndState endState;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Tick())
        {
            if (endState == EndState.destroy)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public bool Tick()
    {
        if (lifeTime > 0)
        {
            time += Time.deltaTime;
            if (time >= lifeTime)
            {
                time = 0f;
                return true;
            }
        }

        return false;
    }
}
