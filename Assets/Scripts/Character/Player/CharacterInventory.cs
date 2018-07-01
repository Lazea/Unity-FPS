using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    int equiptableItemSlotCount;
    ItemSlot[] itemSlots;

    int itemCount;
    bool inventoryFull = false;

    public GameObject primaryWeapon;
    ItemSlot primaryWeaponSlot;
    const string primaryWeaponSlotName = "Primary";
    public GameObject secondaryWeapon;
    ItemSlot secondaryWeaponSlot;
    const string secondaryWeaponSlotName = "Secondary";
    public GameObject sidearmWeapon;
    ItemSlot sidearmWeaponSlot;
    const string sidearmWeaponSlotName = "Sidearm";
    public GameObject meleeWeapon;
    ItemSlot meleeWeaponSlot;
    const string meleeWeaponSlotName = "Melee Weapon";
    public GameObject throwable;
    ItemSlot throwableSlot;
    const string throwableSlotName = "Throwable Item";

    // Use this for initialization
    void Awake () {
        itemSlotCount = 1;
        equiptableItemSlotCount = 5;
        itemSlots = new ItemSlot[equiptableItemSlotCount + itemSlotCount];

        itemCount = 0;

        primaryWeaponSlot = new ItemSlot(0);
        primaryWeaponSlot.slotName = primaryWeaponSlotName;
        if(primaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon, transform.position, transform.rotation, transform);
            primaryWeapon.SetActive(false);
            primaryWeaponSlot.AddItem(primaryWeapon.name, primaryWeapon);
            itemCount += 1;
        }
        itemSlots[0] = primaryWeaponSlot;

        secondaryWeaponSlot = new ItemSlot(1);
        secondaryWeaponSlot.slotName = secondaryWeaponSlotName;
        if (secondaryWeapon != null)
        {
            secondaryWeapon = Instantiate(secondaryWeapon, transform.position, transform.rotation, transform);
            secondaryWeapon.SetActive(false);
            secondaryWeaponSlot.AddItem(secondaryWeapon.name, secondaryWeapon);
            itemCount += 1;
        }
        itemSlots[1] = secondaryWeaponSlot;

        sidearmWeaponSlot = new ItemSlot(2);
        sidearmWeaponSlot.slotName = sidearmWeaponSlotName;
        if (sidearmWeapon != null)
        {
            sidearmWeapon = Instantiate(sidearmWeapon, transform.position, transform.rotation, transform);
            sidearmWeapon.SetActive(false);
            sidearmWeaponSlot.AddItem(sidearmWeapon.name, sidearmWeapon);
            itemCount += 1;
        }
        itemSlots[2] = sidearmWeaponSlot;

        meleeWeaponSlot = new ItemSlot(3);
        meleeWeaponSlot.slotName = meleeWeaponSlotName;
        if (meleeWeapon != null)
        {
            meleeWeapon = Instantiate(meleeWeapon, transform.position, transform.rotation, transform);
            meleeWeapon.SetActive(false);
            meleeWeaponSlot.AddItem(meleeWeapon.name, meleeWeapon);
            itemCount += 1;
        }
        itemSlots[3] = meleeWeaponSlot;

        throwableSlot = new ItemSlot(4);
        throwableSlot.slotName = throwableSlotName;
        if (throwable != null)
        {
            throwable = Instantiate(throwable, transform.position, transform.rotation, transform);
            throwable.SetActive(false);
            throwableSlot.AddItem(throwable.name, throwable);
            itemCount += 1;
        }
        itemSlots[4] = throwableSlot;

        for (int i = equiptableItemSlotCount; i < (equiptableItemSlotCount + itemSlotCount); i++)
        {
            ItemSlot newItemSlot = new ItemSlot(i);
            newItemSlot.slotName = string.Format("Item {0}", i);
            itemSlots[i] = newItemSlot;
        }
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
                for (int i = equiptableItemSlotCount; i < (equiptableItemSlotCount + itemSlotCount); i++)
                {
                    if(GetItemSlot(i).slotName == slotName)
                    {
                        return i;
                    }
                }

                return -1;
        }
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public int GetItemSlotCount()
    {
        return itemSlotCount;
    }

    public int GetEquiptableItemSlotCount()
    {
        return equiptableItemSlotCount;
    }
}
