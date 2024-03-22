using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeView : MonoBehaviour
{
    [SerializeField] AlchemyRecipe recipe;
    [SerializeField] GameObject image1;
    [SerializeField] GameObject image2;
    [SerializeField] GameObject image3;
    [SerializeField] GameObject namePlatePfb;

    // Start is called before the first frame update
    void Start()
    {
        ViewItemIcon(recipe.material_1, image1);
        ViewItemIcon(recipe.material_2, image2);
        ViewItemIcon(recipe.product, image3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ViewItemIcon(GameObject item, GameObject image)
    {
        image.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;

        image.AddComponent<IconInRecipe>();
        image.GetComponent<IconInRecipe>().namePlatePfb = namePlatePfb;
        image.GetComponent<IconInRecipe>().itemName = item.GetComponent<Item>().GetItemName();


    }
}
