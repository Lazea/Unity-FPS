using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    public float throwPower = 10f;
    bool chuckHold;
    bool chuckRelease;

    Rigidbody rb;
    Item item;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        item = gameObject.GetComponent<Item>();
        if(item == null)
        {
            item = gameObject.AddComponent<Item>();
        }

        if(item.IsInUse())
        {
            rb.isKinematic = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (item.IsInUse())
        {
            ReadInput();

            if (chuckRelease)
            {
                Throw();
            }
        }
    }

    void Throw()
    {
        item.Drop();
        transform.parent = null;

        gameObject.GetComponent<Collider>().enabled = true;

        rb.isKinematic = false;
        rb.AddForce(transform.forward * throwPower + Vector3.up * throwPower * 0.75f, ForceMode.Impulse);
    }

    private void ReadInput()
    {
        chuckHold = Input.GetMouseButton(0);
        chuckRelease = Input.GetMouseButtonUp(0);
    }
}
