using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsSetManager : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots;

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
}
