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
    AlchemySceneManager alchemySceneManager;
    [SerializeField]
    EquipmentsSetManager equipmentsManager;
    [SerializeField]
    AlchemyManager alchemyManager;

    [SerializeField]
    DungeonData forest;
    
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
                alchemySceneManager.ToggleSlots();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
               // gameManager.SelectDungeon(forest);
                gameManager.EnterDungeon(equipmentsManager.GetPassiveAbilities());
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                FindObjectOfType<MessageText>().SetMessage("HELLO", 0);
            }
        }
    }
}
