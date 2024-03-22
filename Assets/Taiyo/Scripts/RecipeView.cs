using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeView : MonoBehaviour
{
    [SerializeField] AlchemyRecipe recipe;
    [SerializeField] GameObject flame1;
    [SerializeField] GameObject flame2;
    [SerializeField] GameObject flame3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(recipe.material_1, flame1.transform);
        Instantiate(recipe.material_2, flame2.transform);
        Instantiate(recipe.product, flame3.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateItemIcon(GameObject item, Transform flameTransform)
    {
        GameObject icon = Instantiate(item, flameTransform);
    }
}
