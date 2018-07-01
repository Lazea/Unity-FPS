using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage = 10;

    protected bool fire;
    protected bool fireHold;

    Item item;

    // Use this for initialization
    protected virtual void Start () {
        item = gameObject.GetComponent<Item>();
    }

    // Update is called once per frame
    protected virtual void Update () {
        if (item != null)
        {
            if (item.IsInUse())
            {
                ReadInput();
            }
        } else
        {
            ReadInput();
        }
    }

    protected virtual void Fire()
    {
        throw new System.NotImplementedException("Fire() method needs to be implemented.");
    }

    private void ReadInput()
    {
        fire = Input.GetMouseButtonDown(0);
        fireHold = Input.GetMouseButton(0);
    }
}
