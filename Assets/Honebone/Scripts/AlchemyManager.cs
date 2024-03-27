using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class AlchemyManager : MonoBehaviour
{
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
    RecipeMenuContentGenerator recipeMenuContentGenerator;

    [SerializeField]
    Vector2 spawnPos;

    [SerializeField]
    Animator noteAnim;
    [SerializeField]
    Animator noteAnim_newSlot;


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
        recipeMenuContentGenerator = FindObjectOfType<RecipeMenuContentGenerator>();

        alchemyRecipes = gameManager.GetAlchemyRecipes();

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

                    //trueなら新レシピ false既出
                    if (!gameManager.GetUnlockedRecipe().Contains(recipe)) { //新レシピ
                        gameManager.UnlockRecipe(recipe);
                        noteAnim.SetTrigger("Display");
                        recipeMenuContentGenerator.GenerateContent();
                        if (recipe.unlockSlot)
                        {
                            gameManager.UnlockEquipmentsSlot();
                            noteAnim_newSlot.SetTrigger("Display");
                        }

                        UnityroomApiClient.Instance.SendScore(1, gameManager.GetUnlockedRecipe().Count, ScoreboardWriteMode.HighScoreDesc);

                        //Sound Effect
                        SEManager seManager = FindFirstObjectByType<SEManager>();
                        seManager.PlaySE(SE_find_newList);
                        seManager.PlaySE(SE_notNew);
                    }
                    else//既出レシピ
                    {
                        //Sound Effect
                        SEManager seManager = FindFirstObjectByType<SEManager>();
                        seManager.PlaySE(SE_notNew);
                    }
                }
            }
            //ここでboolがfalseなら失敗
            if (!alchemy_sucess)
            {
                //Sound Effect
                SEManager seManager = FindFirstObjectByType<SEManager>();
                seManager.PlaySE(SE_alchemyFailed);

                FindObjectOfType<MessageText>().SetMessage("何も生まれなかった...", 0);
            }
        }
        slot_L.ConsumeItem();
        slot_R.ConsumeItem();
        SetAlchemyButton();
    }
}
