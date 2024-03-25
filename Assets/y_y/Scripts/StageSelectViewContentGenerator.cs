using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectViewContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject StageContent;
    [SerializeField] private GameObject GameManager;
    [SerializeField] private GameObject alchemySceneManager;

    GameManager gameManager;

    private List<DungeonData> clearedDungeon = new List<DungeonData>();//�������Ă��関�N���A�̃_���W����
    private List<DungeonData> unlockedDungeon = new List<DungeonData>();//�������Ă��関�N���A�̃_���W����

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
            GenerateContent(unlockedDungeon[i], i);
        }

        
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
        content.GetComponent<SetContentElement>().setElement(background, dungeonName, difficulty, dungeonInfo,gameManager.GetCleardDungeon().Contains(unlockedDungeon[i]));
    }

    public void SelectDungeon(int i)
    {
        alchemySceneManager.GetComponent<AlchemySceneManager>().ToggleSlots();
        gameManager.SelectDungeon(unlockedDungeon[i]);

    }
}
