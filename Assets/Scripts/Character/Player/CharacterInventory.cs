using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemSlot
{
    public int index;
    public ItemType slotType;
    public string itemName;
    public GameObject item;

    public ItemSlot(int index, ItemType slotType)
    {
        this.index = index;
        this.slotType = slotType;
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
    public ItemSlot[] itemSlots;

    int itemCount;
    bool inventoryFull = false;

    public GameObject primaryWeapon;
    ItemSlot primaryWeaponSlot;
    const ItemType primaryWeaponSlotType = ItemType.primaryWeapon;
    public GameObject secondaryWeapon;
    ItemSlot secondaryWeaponSlot;
    const ItemType secondaryWeaponSlotType = ItemType.secondaryWeapon;
    public GameObject sidearmWeapon;
    ItemSlot sidearmWeaponSlot;
    const ItemType sidearmWeaponSlotType = ItemType.sidearmWeapon;
    public GameObject meleeWeapon;
    ItemSlot meleeWeaponSlot;
    const ItemType meleeWeaponSlotType = ItemType.meleeWeapon;
    public GameObject throwable;
    ItemSlot throwableSlot;
    const ItemType throwableSlotType = ItemType.throwable;

    const ItemType itemSlotType = ItemType.genericItem;

    CharacterHotbar hotbar;

    // Use this for initialization
    void Awake () {
        itemSlotCount = 1;
        equiptableItemSlotCount = 5;
        itemSlots = new ItemSlot[equiptableItemSlotCount + itemSlotCount];

        itemCount = 0;

        primaryWeaponSlot = new ItemSlot(0, primaryWeaponSlotType);
        if(primaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon, transform.position, transform.rotation, transform);
            primaryWeapon.SetActive(false);
            primaryWeaponSlot.AddItem(primaryWeapon.name, primaryWeapon);
            itemCount += 1;
        }
        itemSlots[0] = primaryWeaponSlot;

        secondaryWeaponSlot = new ItemSlot(1, secondaryWeaponSlotType);
        if (secondaryWeapon != null)
        {
            secondaryWeapon = Instantiate(secondaryWeapon, transform.position, transform.rotation, transform);
            secondaryWeapon.SetActive(false);
            secondaryWeaponSlot.AddItem(secondaryWeapon.name, secondaryWeapon);
            itemCount += 1;
        }
        itemSlots[1] = secondaryWeaponSlot;

        sidearmWeaponSlot = new ItemSlot(2, sidearmWeaponSlotType);
        if (sidearmWeapon != null)
        {
            sidearmWeapon = Instantiate(sidearmWeapon, transform.position, transform.rotation, transform);
            sidearmWeapon.SetActive(false);
            sidearmWeaponSlot.AddItem(sidearmWeapon.name, sidearmWeapon);
            itemCount += 1;
        }
        itemSlots[2] = sidearmWeaponSlot;

        meleeWeaponSlot = new ItemSlot(3, meleeWeaponSlotType);
        if (meleeWeapon != null)
        {
            meleeWeapon = Instantiate(meleeWeapon, transform.position, transform.rotation, transform);
            meleeWeapon.SetActive(false);
            meleeWeaponSlot.AddItem(meleeWeapon.name, meleeWeapon);
            itemCount += 1;
        }
        itemSlots[3] = meleeWeaponSlot;

        throwableSlot = new ItemSlot(4, throwableSlotType);
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
            ItemSlot newItemSlot = new ItemSlot(i, itemSlotType);
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
            if (itemCount >= equiptableItemSlotCount)
            {
                inventoryFull = true;
            }
        }
    }

    public void AddItemToSlot(ItemType slotType, GameObject item)
    {
        int index = TypeToIndex(slotType);
        AddItemToSlot(index, item);
    }

    public void RemoveItemFromSlot(int index)
    {
        itemSlots[index].RemoveItem();
        itemCount -= 1;
    }

    public void RemoveItemFromSlot(ItemType slotType)
    {
        int index = TypeToIndex(slotType);
        RemoveItemFromSlot(index);
    }

    public ItemSlot GetItemSlot(int index)
    {
        return itemSlots[index];
    }

    public GameObject GetItem(int index)
    {
        return GetItemSlot(index).item;
    }

    public GameObject GetItem(ItemType slotType)
    {
        int index = TypeToIndex(slotType);
        return GetItem(index);
    }

    public int TypeToIndex(ItemType slotType)
    {
        switch (slotType)
        {
            case ItemType.primaryWeapon:
                return 0;
            case ItemType.secondaryWeapon:
                return 1;
            case ItemType.sidearmWeapon:
                return 2;
            case ItemType.meleeWeapon:
                return 3;
            case ItemType.throwable:
                return 4;
            default:
                for (int i = equiptableItemSlotCount; i < (equiptableItemSlotCount + itemSlotCount); i++)
                {
                    if(GetItemSlot(i).slotType == slotType)
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
