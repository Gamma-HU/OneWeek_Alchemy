using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    [SerializeField]
    GameObject recipeRemainText;
    [SerializeField]
    Animator anim;
    [SerializeField]
    float interval;

    [Header("CompleteDungenを呼び出してからクリア用のSEを流すまでのインターバル")]
    [SerializeField] private float interval_clearSE;
    [SerializeField] private AudioClip SE_clear;

    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ComleteDungeon()
    {
        anim.SetTrigger("Display");
        StartCoroutine(playClearSE());
    }
    public void StartDisplayRewards()
    {
        StartCoroutine(DisplayRewards());
    }
    IEnumerator DisplayRewards()
    {

        yield return new WaitForSeconds(interval);
        if (gameManager.GetUnlockedRecipe().Count != gameManager.GetAlchemyRecipes().Count)
        {
            recipeRemainText.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        FindObjectOfType<GameManager>().ReturnToAlchemyScene();
    }

    IEnumerator playClearSE()
    {
        yield return new WaitForSeconds(interval);
        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_clear);
    }
}
