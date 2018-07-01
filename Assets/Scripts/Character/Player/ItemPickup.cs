using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public float pickupRadius = 5f;
    public LayerMask layerMask;

    bool pickup;

    CharacterInventory inventory;
    CharacterHotbar hotbar;

	// Use this for initialization
	void Start () {
        inventory = gameObject.GetComponent<CharacterInventory>();
        hotbar = gameObject.GetComponent<CharacterHotbar>();
    }
	
	// Update is called once per frame
	void Update () {
        pickup = Input.GetKeyDown(KeyCode.E);

        Collider[] items = Physics.OverlapSphere(transform.position, pickupRadius, layerMask);
        if (items.Length > 0)
        {
            GameObject closestItem = null;
            float currClosestItemDistance = 0f;
            for (int i = 0; i < items.Length; i++)
            {
                if(closestItem == null)
                {
                    closestItem = items[i].gameObject;
                    currClosestItemDistance = Vector3.Distance(closestItem.transform.position, transform.position);
                } else
                {
                    float itemDistance = Vector3.Distance(items[i].transform.position, transform.position);
                    if(itemDistance < currClosestItemDistance)
                    {
                        currClosestItemDistance = itemDistance;
                        closestItem = items[i].gameObject;
                    }
                }
            }

            if (pickup && closestItem != null)
            {
                ItemType itemType = closestItem.GetComponent<Item>().type;
                inventory.AddItemToSlot(itemType, closestItem);
                hotbar.SwitchTo(inventory.TypeToIndex(itemType));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
