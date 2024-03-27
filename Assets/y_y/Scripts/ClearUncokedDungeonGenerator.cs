using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUncokedDungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject UnlockedDungeonPfb_clearUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip SE_showUnlockedDungeon;

    [Header("ダンジョンの表示の間隔")]
    [SerializeField] private float interval;

    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            StartCoroutine(ActivateClearUI());
            test = false;
        }
    }

    public IEnumerator ActivateClearUI()
    {
        DungeonData currentDungeon = gameManager.GetComponent<GameManager>().GetSelectedDugeon();
        List<DungeonData> unlockedDungeon = currentDungeon.nextDungeons;

        for (int i = 0; i < unlockedDungeon.Count; i++)
        {
            GenerateRewardItems(unlockedDungeon[i]);
            yield return new WaitForSeconds(interval);
        }
    }

    private void GenerateRewardItems(DungeonData dungeonData)
    {
        GameObject unlockedDungeonPfb = Instantiate(UnlockedDungeonPfb_clearUI, transform.position, Quaternion.identity);
        unlockedDungeonPfb.transform.SetParent(this.transform, false);

        //画像とテキストを設定
        unlockedDungeonPfb.gameObject.GetComponent<Image>().sprite = dungeonData.background;
        unlockedDungeonPfb.transform.GetChild(0).gameObject.GetComponent<Text>().text = dungeonData.dungeonName;


        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_showUnlockedDungeon);
    }
}
