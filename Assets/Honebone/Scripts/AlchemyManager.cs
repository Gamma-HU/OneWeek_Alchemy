using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyManager : MonoBehaviour
{
    [SerializeField]
    List<AlchemyRecipe> alchemyRecipes = new List<AlchemyRecipe>();
    [SerializeField]
    AlchemySlot slot_L;
    [SerializeField]
    AlchemySlot slot_R;

    [SerializeField]
    Vector2 spawnPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Alchemy()
    {
        if (slot_L.GetItem() != null && slot_R.GetItem() != null)
        {
            string name1 = slot_L.GetItem().GetItemName();
            string name2 = slot_R.GetItem().GetItemName();
            foreach (AlchemyRecipe recipe in alchemyRecipes)
            {
                if (recipe.CheckMaterial(name1, name2))
                {
                    var p = Instantiate(recipe.product, spawnPos, Quaternion.identity);
                    p.GetComponent<Item>().Init();
                    p.GetComponent<Item>().Snap();

                    slot_L.ConsumeItem();
                    slot_R.ConsumeItem();
                }
            }
        }
    }
}
