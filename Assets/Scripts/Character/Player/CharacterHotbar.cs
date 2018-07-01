using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHotbar : MonoBehaviour {

    CharacterInventory inventory;

    public int currSlotIndex = 0;
    public int prevSlotIndex = 1;
    GameObject currItem;

    bool quickSwitchSlot;
    bool nextSwitchSlot;
    bool prevSwitchSlot;
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
    };

    public Transform rightHand;
    public Transform leftHand;

    // Use this for initialization
    void Start () {
        inventory = GetComponent<CharacterInventory>();

        currSlotIndex = 0;
        prevSlotIndex = 1;

        currItem = inventory.GetItem(currSlotIndex);
        if(currItem != null)
        {
            EquiptItem(currItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        quickSwitchSlot = Input.GetKeyDown(KeyCode.Q);
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        nextSwitchSlot = false;
        prevSwitchSlot = false;
        if (scroll > 0)
        {
            nextSwitchSlot = true;
        }
        else if (scroll < 0)
        {
            prevSwitchSlot = true;
        }

        if (quickSwitchSlot)
        {
            QuickSwitchTo();
        }
        else if (nextSwitchSlot)
        {
            SwitchToNext();
        }
        else if (prevSwitchSlot)
        {
            SwitchToPrev();
        }
        else
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    SwitchTo(i);
                    break;
                }
            }
        }
    }

    public GameObject SwitchTo(int index)
    {
        if (index != currSlotIndex)
        {
            GameObject item = GetItem(index);
            if (item != null)
            {
                UnequiptItem(currItem);
                EquiptItem(item);

                currItem = item;

                prevSlotIndex = currSlotIndex;
                currSlotIndex = index;
            }

            return item;
        }

        return null;
    }

    public GameObject SwitchToNext()
    {
        if (inventory.GetItemCount() > 0)
        {
            int index = currSlotIndex + 1;
            if (index >= inventory.GetEquiptableItemSlotCount())
            {
                index = 0;
            }

            GameObject item = GetItem(index);
            while (item == null)
            {
                index += 1;
                if (index >= inventory.GetEquiptableItemSlotCount())
                {
                    index = 0;
                }

                item = GetItem(index);
            }

            return SwitchTo(index);
        }

        return null;
    }

    public GameObject SwitchToPrev()
    {
        if (inventory.GetItemCount() > 0)
        {
            int index = currSlotIndex - 1;
            if (index < 0)
            {
                index = inventory.GetEquiptableItemSlotCount() - 1;
            }

            GameObject item = GetItem(index);
            while (item == null)
            {
                index -= 1;
                if (index < 0)
                {
                    index = inventory.GetEquiptableItemSlotCount() - 1;
                }

                item = GetItem(index);
            }

            return SwitchTo(index);
        }

        return null;
    }

    public GameObject QuickSwitchTo()
    {
        return SwitchTo(prevSlotIndex);
    }

    GameObject GetItem(int index)
    {
        return inventory.GetItem(index);
    }

    void EquiptItem(GameObject item)
    {
        item.SetActive(true);
        item.transform.parent = rightHand;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    void UnequiptItem(GameObject item)
    {
        item.SetActive(false);
    }

    void DropItem(GameObject item)
    {

    }
}
