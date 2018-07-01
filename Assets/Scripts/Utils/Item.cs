using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    primaryWeapon,
    secondaryWeapon,
    sidearmWeapon,
    meleeWeapon,
    throwable,
    genericItem
}

public class Item : MonoBehaviour {

    bool inUse = false;

    Rigidbody rb;
    Collider collider;

    public ItemType type;

    CharacterHotbar hotbar;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        collider = gameObject.GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
		if(inUse)
        {
            rb.isKinematic = true;

            if(collider != null)
            {
                collider.enabled = false;
            }
        } else
        {
            rb.isKinematic = false;

            if (collider != null)
            {
                collider.enabled = true;
            }
        }
	}

    public void Pickup(CharacterHotbar hotbar)
    {
        this.hotbar = hotbar;
        inUse = true;
    }

    public void Drop()
    {
        if (hotbar != null)
        {
            hotbar.DropItem(this.gameObject);
            hotbar = null;
            inUse = false;
        }
    }

    public bool IsInUse()
    {
        return inUse;
    }
}
