using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    //[SerializeField]
    //Text itemNameText;
    [SerializeField]
    EquipmentsSetManager equipmentManager;
    Item setItem;

    private void Start()
    {
        equipmentManager = FindObjectOfType<EquipmentsSetManager>();
    }
    public void SetItem(Item item)
    {
        if (setItem != null)
        {
            setItem.ResetSlot();
        }
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
