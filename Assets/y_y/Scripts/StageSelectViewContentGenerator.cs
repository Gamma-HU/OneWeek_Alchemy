using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectViewContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject StageContent;
    [SerializeField] private GameObject GameManager;
    [SerializeField] private GameObject alchemySceneManager;

    GameManager gameManager;

    private List<DungeonData> clearedDungeon = new List<DungeonData>();
    private List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されている未クリアのダンジョン

    List<DungeonData> dungeonList = new List<DungeonData>();

    private Sprite background;
    private string dungeonName;
    private int difficulty;
    private string dungeonInfo;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        clearedDungeon = gameManager.GetCleardDungeon();
        unlockedDungeon = gameManager.GetUnlockedDungeon();


        for (int i = 0; i < unlockedDungeon.Count; i++)
        {
            if (!clearedDungeon.Contains(unlockedDungeon[i]))
            {
                dungeonList.Add(unlockedDungeon[i]);
                //GenerateContent(unlockedDungeon[i], i);
            }
        }
        if (clearedDungeon.Count > 0) { dungeonList.AddRange(clearedDungeon); }

        for(int i = 0; i < dungeonList.Count; i++)
        {
            GenerateContent(dungeonList[i], i);
        }
        //for (int i = unlockedDungeon.Count; i < clearedDungeon.Count + unlockedDungeon.Count; i++)
        //{
        //    GenerateContent(clearedDungeon[i], i + unlockedDungeon.Count);
        //}
    }

    private void GenerateContent(DungeonData dungeonData, int i)
    {
        GameObject content = Instantiate(StageContent, transform.position, Quaternion.identity);
        content.transform.SetParent(this.transform, false);

        background = dungeonData.background;
        dungeonName = dungeonData.dungeonName;
        difficulty = dungeonData.difficulty;
        dungeonInfo = dungeonData.dungeonInfo;

        content.GetComponent<SetContentElement>().dungeonNum = i;
        //print((background, dungeonName, difficulty, dungeonInfo));
        content.GetComponent<SetContentElement>().setElement(background, dungeonName, difficulty, dungeonInfo,gameManager.GetCleardDungeon().Contains(dungeonList[i]));
    }

    public void SelectDungeon(int i)
    {
        alchemySceneManager.GetComponent<AlchemySceneManager>().ToggleSlots();
        gameManager.SelectDungeon(dungeonList[i]);

    }
}
