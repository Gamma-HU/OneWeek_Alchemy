using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyManager : MonoBehaviour
{
    [SerializeField]
    List<AlchemyRecipe> alchemyRecipes = new List<AlchemyRecipe>();
    [SerializeField]
    GameObject slotsP;
    [SerializeField]
    AlchemySlot slot_L;
    [SerializeField]
    AlchemySlot slot_R;
    [SerializeField]
    GameObject alchemyButton;
    [SerializeField]
    Text productNameText;
   

    GameManager gameManager;
    AlchemySceneManager alchemySceneManager;

    [SerializeField]
    Vector2 spawnPos;

    private bool alchemy_sucess = false;
    [SerializeField]
    private AudioClip SE_find_newList;
    [SerializeField]
    private AudioClip SE_notNew;
    [SerializeField]
    private AudioClip SE_alchemyFailed;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        alchemySceneManager = FindObjectOfType<AlchemySceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void ToggleEquipmentSlots()
    {
        if (slotsP.activeSelf)
        {
            slot_L.SnapItem();
            slot_R.SnapItem();
        }
        slotsP.SetActive(!slotsP.activeSelf);
    }
    public void SetAlchemyButton()
    {
        if (slot_L.GetItem() != null && slot_R.GetItem() != null)
        {
            alchemyButton.SetActive(true);
            string name1 = slot_L.GetItem().GetItemName();
            string name2 = slot_R.GetItem().GetItemName();
            foreach (AlchemyRecipe recipe in alchemyRecipes)
            {
                if (recipe.CheckMaterial(name1, name2))
                {
                    if (gameManager.GetUnlockedRecipe().Contains(recipe)) { productNameText.text = recipe.product.GetComponent<Item>().GetItemName(); }
                    else { productNameText.text = "???"; }
                    return;
                }
            }
            productNameText.text = "???";
        }
        else
        {
            alchemyButton.SetActive(false);
        }
    }
   
    public void Alchemy()
    {
        if (slot_L.GetItem() != null && slot_R.GetItem() != null)
        {
            alchemy_sucess = false;
            string name1 = slot_L.GetItem().GetItemName();
            string name2 = slot_R.GetItem().GetItemName();
            foreach (AlchemyRecipe recipe in alchemyRecipes)
            {
                if (recipe.CheckMaterial(name1, name2))
                {
                    //錬金成功
                    alchemy_sucess = true;
                    alchemySceneManager.SpawnItem(recipe.product, spawnPos);

                    slot_L.ConsumeItem();
                    slot_R.ConsumeItem();
                    //trueなら新レシピ false既出
                    if (!gameManager.GetUnlockedRecipe().Contains(recipe)) { 
                        gameManager.UnlockRecipe(recipe);
                        //Sound Effect
                        SEManager seManager = FindFirstObjectByType<SEManager>();
                        seManager.PlaySE(SE_find_newList);
                    }
                    else
                    {
                        //Sound Effect
                        SEManager seManager = FindFirstObjectByType<SEManager>();
                        seManager.PlaySE(SE_notNew);
                    }
                    SetAlchemyButton();
                }
            }
            //ここでboolがfalseなら失敗
            if (!alchemy_sucess)
            {
                //Sound Effect
                SEManager seManager = FindFirstObjectByType<SEManager>();
                seManager.PlaySE(SE_alchemyFailed);
            }
        }
    }
}
