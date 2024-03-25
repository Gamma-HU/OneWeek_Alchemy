using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    bool debugMode;
    [SerializeField]    List<DungeonData> clearedDungeon_debug = new List<DungeonData>();
    [SerializeField] List<DungeonData> unlockedDungeon_debug = new List<DungeonData>();//解放されているダンジョン
    [SerializeField] List<GameObject> unlockedMaterial_debug = new List<GameObject>();
    [SerializeField] List<AlchemyRecipe> unlockedRecipe_debug = new List<AlchemyRecipe>();

    List<DungeonData> clearedDungeon = new List<DungeonData>();
    List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されているダンジョン
    List<GameObject> unlockedMaterial = new List<GameObject>();
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
        //if (debugMode)
        //{
        //    clearedDungeon = new List<DungeonData>(clearedDungeon_debug);
        //    unlockedDungeon = new List<DungeonData>(unlockedDungeon_debug);
        //    unlockedMaterial = new List<GameObject>(unlockedMaterial_debug);
        //    unlockedRecipe = new List<AlchemyRecipe>(unlockedRecipe_debug);
        //}
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

    public List<DungeonData> GetCleardDungeon()
    {
        if (debugMode) { return clearedDungeon_debug; }
        else { return clearedDungeon; }
    }
    public List<DungeonData> GetUnlockedDungeon()
    {
        if (debugMode) { return unlockedDungeon_debug; }
        else { return unlockedDungeon; }
    }
    public List<GameObject> GetUnlockedMaterial()
    {
        if (debugMode) { return unlockedMaterial_debug; }
        else { return unlockedMaterial; }
    }
    public List<AlchemyRecipe> GetUnlockedRecipe()
    {
        if (debugMode) { return unlockedRecipe_debug; }
        else { return unlockedRecipe; }
    }

    public List<GameObject> GetEquipments() { return equipments; }
    public DungeonData GetSelectedDugeon() { return selectedDungeon; }
}
