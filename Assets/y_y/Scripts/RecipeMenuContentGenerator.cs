using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeMenuContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject RecipePfb;

    [Header("���̃��X�g�ɉ���ς݂̃��V�s��n���Ă�������.\n����͎蓮�ŗv�f���w�肵�Ă���")]
    public List<AlchemyRecipe> unlockedRecipe = new List<AlchemyRecipe>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateContent();
    }

    private void GenerateContent()
    {
        for (int i = 0; i < unlockedRecipe.Count; i++)
        {
            GameObject recipe_obj = Instantiate(RecipePfb, transform.position, Quaternion.identity);
            recipe_obj.transform.SetParent(this.transform, false);
            recipe_obj.GetComponent<RecipeView>().SetRecipe(unlockedRecipe[i]);
        }
    }
}
