using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
    [SerializeField]
    bool debug;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    EquipmentsSetManager equipmentsManager;
    [SerializeField]
    AlchemyManager alchemyManager;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach(GameObject pa in equipmentsManager.GetPassiveAbilities())
                {
                    Debug.Log(pa.GetComponent<PassiveAbility>().GetPAName());
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                alchemyManager.ToggleEquipmentSlots();
                equipmentsManager.ToggleEquipmentSlots();
            }
        }
    }
}
