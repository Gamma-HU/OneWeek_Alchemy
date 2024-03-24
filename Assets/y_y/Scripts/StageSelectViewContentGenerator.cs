using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectViewContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject StageContent;
    [SerializeField] private GameObject GameManager;
    [SerializeField] private GameObject alchemySceneManager;

    private List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されている未クリアのダンジョン

    private Sprite background;
    private string dungeonName;
    private int difficulty;
    private string dungeonInfo;

    // Start is called before the first frame update
    void Start()
    {
        unlockedDungeon = GameManager.GetComponent<GameManager>().GetUnlockedDungeon();
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
        content.GetComponent<SetContentElement>().setElement(background, dungeonName, difficulty, dungeonInfo);
    }

    public void SelectDungeon(int i)
    {
        alchemySceneManager.GetComponent<AlchemySceneManager>().ToggleSlots();
        GameManager.GetComponent<GameManager>().SelectDungeon(unlockedDungeon[i]);

    }
}
