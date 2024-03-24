using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsSetManager : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots;
    [SerializeField]
    GameObject slotsP;

    public List<GameObject> GetPassiveAbilities()
    {
        List<GameObject> PA = new List<GameObject>();
        foreach(EquipmentSlot slot in equipmentSlots)
        {
            if (slot.GetItem() != null)
            {
                PA.Add(slot.GetItem().GetItemData().passiveAbility);
            }
        }
        return PA;
    }
    public void CheckEquipments(EquipmentSlot set, Item.ItemData itemData)
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (slot == set) { continue; }
            if (slot.GetItem() != null)
            {
                if (slot.GetItem().GetItemData().weapon && itemData.weapon)
                {
                    slot.SnapItem();
                    return;
                }
                if (slot.GetItem().GetItemData().armor && itemData.armor)
                {
                    slot.SnapItem();
                    return;
                }
                if (slot.GetItem().GetItemName() == itemData.itemName)
                {
                    slot.SnapItem();
                    return;
                }
            }
        }
    }
    public void ToggleEquipmentSlots()
    {
        if (slotsP.activeSelf)
        {
            foreach(EquipmentSlot slot in equipmentSlots)
            {
                slot.SnapItem();
            }
        }
        slotsP.SetActive(!slotsP.activeSelf);
    }
}
