using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemySceneManager : MonoBehaviour
{

    GameManager gameManager;
    AlchemyManager alchemyManager;
    EquipmentsSetManager equipmentsManager;
    [SerializeField]
    Transform itemsP;
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
