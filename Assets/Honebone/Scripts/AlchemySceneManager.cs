using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemySceneManager : MonoBehaviour
{

    GameManager gameManager;
    AlchemyManager alchemyManager;
    EquipmentsSetManager equipmentsManager;
    [SerializeField]
    Transform itemsP;
    [SerializeField]
    Text draggingItemText;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        alchemyManager = FindObjectOfType<AlchemyManager>();
        equipmentsManager = FindObjectOfType<EquipmentsSetManager>();
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
    }
}
