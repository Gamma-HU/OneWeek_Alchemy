using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemySceneManager : MonoBehaviour
{

    GameManager gameManager;
    AlchemyManager alchemyManager;
    EquipmentsSetManager equipmentsManager;
    SEManager SEManager;
    [SerializeField]
    Transform itemsP;
    [SerializeField]
    Text draggingItemText;
    [SerializeField]
    AudioClip SE_HOLD;
    [SerializeField]
    AudioClip SE_Snap;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        alchemyManager = FindObjectOfType<AlchemyManager>();
        equipmentsManager = FindObjectOfType<EquipmentsSetManager>();
        SEManager = FindObjectOfType<SEManager>();
    }
    public void ToggleSlots()
    {
        alchemyManager.ToggleEquipmentSlots();
        equipmentsManager.ToggleEquipmentSlots();
    }
    public void SetDraggingItemText(string s)
    {
        draggingItemText.text = s;
    }
    public void PlaySE_Snap()
    {
        SEManager.PlaySE(SE_Snap);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="spawnPos">Šî–{“I‚ÉŒ´“_‚ÅOK</param>
    public void SpawnItem(GameObject item,Vector2 spawnPos)
    {
        var i = Instantiate(item, spawnPos, Quaternion.identity, itemsP);
        i.GetComponent<Item>().Init();
        i.GetComponent<Item>().Snap();
        i.GetComponent<Item>().SE_hold = SE_HOLD;
    }
}
