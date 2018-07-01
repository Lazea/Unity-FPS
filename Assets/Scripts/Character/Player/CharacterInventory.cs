using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemSlot
{
    public int index;
    public string slotName;
    public string itemName;
    public GameObject item;

    public ItemSlot(int index)
    {
        this.index = index;
        slotName = "";
        itemName = "";
        item = null;
    }

    public void AddItem(string name, GameObject item)
    {
        itemName = name;
        this.item = item;
    }

    public void RemoveItem()
    {
        item = null;
        itemName = "";
    }
}

public class CharacterInventory : MonoBehaviour {

    int itemSlotCount;
    int genericItemSlotIndexOffset;
    ItemSlot[] itemSlots;

    int itemCount;
    bool inventoryFull = false;

    ItemSlot primaryWeaponSlot;
    const string primaryWeaponSlotName = "Primary";
    ItemSlot secondaryWeaponSlot;
    const string secondaryWeaponSlotName = "Secondary";
    ItemSlot sidearmWeaponSlot;
    const string sidearmWeaponSlotName = "Sidearm";
    ItemSlot meleeWeaponSlot;
    const string meleeWeaponSlotName = "Melee Weapon";
    ItemSlot throwableSlot;
    const string throwableSlotName = "Throwable Item";

    ItemSlot currItemSlot;

    // Use this for initialization
    void Start () {
        genericItemSlotIndexOffset = 5;
        itemSlots = new ItemSlot[genericItemSlotIndexOffset + itemSlotCount];

        primaryWeaponSlot = new ItemSlot(0);
        primaryWeaponSlot.slotName = primaryWeaponSlotName;
        itemSlots[0] = primaryWeaponSlot;

        secondaryWeaponSlot = new ItemSlot(1);
        secondaryWeaponSlot.slotName = secondaryWeaponSlotName;
        itemSlots[1] = secondaryWeaponSlot;

        sidearmWeaponSlot = new ItemSlot(2);
        sidearmWeaponSlot.slotName = sidearmWeaponSlotName;
        itemSlots[2] = sidearmWeaponSlot;

        meleeWeaponSlot = new ItemSlot(3);
        meleeWeaponSlot.slotName = meleeWeaponSlotName;
        itemSlots[3] = meleeWeaponSlot;

        throwableSlot = new ItemSlot(4);
        throwableSlot.slotName = throwableSlotName;
        itemSlots[4] = throwableSlot;

        for (int i = genericItemSlotIndexOffset; i < (genericItemSlotIndexOffset + itemSlotCount); i++)
        {
            ItemSlot newItemSlot = new ItemSlot(i);
            newItemSlot.slotName = string.Format("Item {0}", i);
            itemSlots[i] = newItemSlot;
        }

        itemCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItemToSlot(int index, GameObject item)
    {
        if (!inventoryFull)
        {
            itemSlots[index].AddItem("", item);

            itemCount += 1;
            if (itemCount >= itemSlotCount)
            {
                inventoryFull = true;
            }
        }
    }

    public void AddItemToNamedSlot(string slotName, GameObject item)
    {
        int index = NameToIndex(slotName);
        AddItemToSlot(index, item);
    }

    public void RemoveItemFromSlot(int index)
    {
        itemSlots[index].RemoveItem();
    }

    public void RemoveItemFromNamedSlot(string slotName)
    {
        int index = NameToIndex(slotName);
    }

    public ItemSlot GetItemSlot(int index)
    {
        return itemSlots[index];
    }

    public GameObject GetItem(int index)
    {
        return GetItemSlot(index).item;
    }

    public GameObject GetItem(string slotName)
    {
        int index = NameToIndex(slotName);
        return GetItem(index);
    }

    int NameToIndex(string slotName)
    {
        switch (slotName)
        {
            case primaryWeaponSlotName:
                return 0;
            case secondaryWeaponSlotName:
                return 1;
            case sidearmWeaponSlotName:
                return 2;
            case meleeWeaponSlotName:
                return 3;
            case throwableSlotName:
                return 4;
            default:
                for (int i = genericItemSlotIndexOffset; i < (genericItemSlotIndexOffset + itemSlotCount); i++)
                {
                    if(GetItemSlot(i).slotName == slotName)
                    {
                        return i;
                    }
                }

                return -1;
        }
    }
}
