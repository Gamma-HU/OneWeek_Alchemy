using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    //[SerializeField]
    //Text itemNameText;
    EquipmentsSetManager equipmentManager;
    SEManager SEManager;
    Item setItem;

    private void Start()
    {
        equipmentManager = FindObjectOfType<EquipmentsSetManager>();
        SEManager = FindObjectOfType<SEManager>();
    }
    public void SetItem(Item item)
    {
        if (setItem != null)
        {
            setItem.ResetSlot();
        }
        equipmentManager.PlayEquipSE();
        setItem = item;
        equipmentManager.CheckEquipments(this, setItem.GetItemData());
        //itemNameText.text = item.GetItemName();
    }
    public void ResetItem()
    {
        setItem = null;
        //itemNameText.text = string.Empty;
    }

    public void SnapItem()
    {
        if (setItem != null)
        {
            setItem.ResetSlot();
            setItem = null;
        }
    }

    public Vector3 GetPos() { return transform.position; }
    public Item GetItem() { return setItem; }
}
