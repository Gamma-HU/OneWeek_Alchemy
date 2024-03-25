using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentsSetManager : MonoBehaviour
{
    [SerializeField]
    List<EquipmentSlot> equipmentSlots;
    [SerializeField]
    GameObject slotsP;

    [SerializeField]
    List<AudioClip> SE_equip;
    [SerializeField]
    GameObject embarkButton;
    [SerializeField]
    Image backpack;
    [SerializeField]
    Sprite[] bakpacks;

    GameManager gameManager;
    MessageText message;
    SEManager SEManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        message = FindObjectOfType<MessageText>();
        SEManager = FindObjectOfType<SEManager>();
    }
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
                    message.SetMessage("武器は一つまで", 0f);
                    return;
                }
                if (slot.GetItem().GetItemData().armor && itemData.armor)
                {
                    slot.SnapItem();
                    message.SetMessage("防具は一つまで", 0f);
                    return;
                }
                if (slot.GetItem().GetItemName() == itemData.itemName)
                {
                    slot.SnapItem();
                    message.SetMessage("同じ装備は一つまで", 0f);
                    return;
                }
            }
        }
    }

    public void Embark()
    {
        gameManager.EnterDungeon(GetPassiveAbilities());
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
        else
        {
            message.SetMessage("装備するアイテムを選択", 0.25f);
        }
        embarkButton.SetActive(!slotsP.activeSelf);
        int slots = gameManager.GetUnlockedEquipmentSlot();
        if (slots == 4) { backpack.sprite = bakpacks[0]; }
        if (slots == 5) { backpack.sprite = bakpacks[1]; }
        for(int i = 0; i < equipmentSlots.Count; i++)
        {
            if (i < slots) { equipmentSlots[i].gameObject.SetActive(true); }
            else { equipmentSlots[i].gameObject.SetActive(false); }
        }
        slotsP.SetActive(!slotsP.activeSelf);
    }
    public void PlayEquipSE()
    {
        SEManager.PlaySE(SE_equip.Choice());
    }
}
