using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    DungeonData firstDangeon;//次のダンジョンはこのデータ内に格納
    List<DungeonData> clearedDungeon = new List<DungeonData>();
    [SerializeField]//test
    List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されているダンジョン
    [SerializeField]//test
    List<GameObject> unlockedMaterial = new List<GameObject>();
    [SerializeField]//test
    List<AlchemyRecipe> unlockedRecipe = new List<AlchemyRecipe>();

    [SerializeField]//test
    DungeonData selectedDungeon;
    [SerializeField]//test
    List<GameObject> equipments;

    List<GameObject> generatedItems=new List<GameObject> ();

    int equipmentSlots = 3;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager instance;

    void Awake()
    {
        CheckInstance();
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectDungeon(DungeonData select)
    {
        selectedDungeon = select;
    }
    public void EnterDungeon(List<GameObject> eq)
    {
        equipments = eq;
        SaveGeneratedItems(FindObjectOfType<AlchemySceneManager>().GetGeneratedItems());
        SceneManager.LoadScene("Battle");
    }
    public void ReturnToAlchemyScene()
    {
        equipments = new List<GameObject>();
        SceneManager.LoadScene("Alchemy");
    }

    public void SaveGeneratedItems(List<GameObject> items)
    {
        generatedItems = new List<GameObject>(items);
    }
    public List<GameObject> GetGeneratedItems() { return generatedItems; }

    public void UnlockEquipmentsSlot() { equipmentSlots++; }
    public void UnlockRecipe(AlchemyRecipe recipe)
    {
        if (!unlockedRecipe.Contains(recipe)) { unlockedRecipe.Add(recipe); }
    }
    public void UnlockMaterial(GameObject material)
    {
        if (!unlockedMaterial.Contains(material)) { unlockedMaterial.Add(material); }
    }
    public void ClearDungeon(DungeonData dungeon)
    {
        if (!clearedDungeon.Contains(dungeon)) { clearedDungeon.Add(dungeon); }
    }
    public void UnlockDungeon(DungeonData dungeon)
    {
        if (!unlockedDungeon.Contains(dungeon)) { unlockedDungeon.Add(dungeon); }
    }

    public List<DungeonData> GetCleardDungeon() { return clearedDungeon; }
    public List<DungeonData> GetUnlockedDungeon() { return unlockedDungeon; }
    public List<GameObject> GetUnlockedMaterial() { return unlockedMaterial; }
    public List<AlchemyRecipe> GetUnlockedRecipe() { return unlockedRecipe; }

    public List<GameObject> GetEquipments() { return equipments; }
    public DungeonData GetSelectedDugeon() { return selectedDungeon; }
}
