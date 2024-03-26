using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> firstItems;
    [SerializeField] DungeonData firstDungeon;
    [SerializeField]
    bool debugMode;
    [SerializeField] bool deleteSave;

    [SerializeField] List<DungeonData> dungeonDataBase;
    [SerializeField] List<GameObject> itemDataBase;
    [SerializeField] List<AlchemyRecipe> recipeDataBase;
    
    [SerializeField] List<DungeonData> clearedDungeon_debug = new List<DungeonData>();
    [SerializeField] List<DungeonData> unlockedDungeon_debug = new List<DungeonData>();
    [SerializeField] List<GameObject> unlockedMaterial_debug = new List<GameObject>();
    [SerializeField] List<AlchemyRecipe> unlockedRecipe_debug = new List<AlchemyRecipe>();

    List<DungeonData> clearedDungeon = new List<DungeonData>();//save
    List<DungeonData> unlockedDungeon = new List<DungeonData>();//‰ð•ú‚³‚ê‚Ä‚¢‚éƒ_ƒ“ƒWƒ‡ƒ“  //save
    List<GameObject> unlockedMaterial = new List<GameObject>();//save
    List<AlchemyRecipe> unlockedRecipe = new List<AlchemyRecipe>();//save

    [SerializeField]//test
    DungeonData selectedDungeon;
    [SerializeField]//test
    List<GameObject> equipments;

    List<string> generatedItems = new List<string>();//save

    int equipmentSlots = 3;//save

    Dictionary<string, int> itemNameDic = new Dictionary<string, int>(); 

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //for (int i = 0; i < dungeonDataBase.Count; i++)
        //{
        //    dungeonDataBase[i].SetID(i);
        //}

        //for (int i = 0; i < recipeDataBase.Count; i++)
        //{
        //    recipeDataBase[i].SetID(i);
        //}
        Dictionary<string, int> test = new Dictionary<string, int>();
        foreach(AlchemyRecipe recipe in recipeDataBase)
        {
            string mName = recipe.material_1.GetComponent<Item>().GetItemName();

        }

    }

    public static GameManager instance;

    void Awake()
    {
        CheckInstance();

        if (deleteSave) { PlayerPrefs.DeleteAll(); }
       
        LoadData();
       
        if (unlockedMaterial.Count == 0) { unlockedMaterial.AddRange(firstItems); }
        if (unlockedDungeon.Count == 0) { unlockedDungeon.Add(firstDungeon); }

        for (int i = 0; i < itemDataBase.Count; i++)
        {
            itemNameDic.Add(itemDataBase[i].GetComponent<Item>().GetItemName(), i);
        }
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
        FindObjectOfType<Blackout>().StartBlackout();
        StartCoroutine(LoadSceneDelay("Battle"));
    }
    public void ReturnToAlchemyScene()
    {
        FindObjectOfType<Blackout>().StartBlackout();
        equipments = new List<GameObject>();
        StartCoroutine(LoadSceneDelay("Alchemy"));
    }
    IEnumerator LoadSceneDelay(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void SaveGeneratedItems(List<string> items)
    {
        generatedItems = new List<string>(items);
    }
    public List<string> GetGeneratedItemsName() { return generatedItems; }

    public void UnlockEquipmentsSlot()
    {
        equipmentSlots++;
        SaveData();
    }
    public void UnlockRecipe(AlchemyRecipe recipe)
    {
        if (!unlockedRecipe.Contains(recipe)) { unlockedRecipe.Add(recipe); }
        SaveData();
    }
    public void UnlockMaterial(GameObject material)
    {
        if (!unlockedMaterial.Contains(material)) { unlockedMaterial.Add(material); }
        SaveData();
    }
    public void ClearDungeon(DungeonData dungeon)
    {
        if (!clearedDungeon.Contains(dungeon)) { clearedDungeon.Add(dungeon); }
        SaveData();
    }
    public void UnlockDungeon(DungeonData dungeon)
    {
        if (!unlockedDungeon.Contains(dungeon)) { unlockedDungeon.Add(dungeon); }
        SaveData();
    }

    public int GetUnlockedEquipmentSlot() { return equipmentSlots; }
    public List<GameObject> GetFirstItems() { return firstItems; }
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

    public void SaveData()
    {
        foreach(GameObject data in itemDataBase)
        {
            int i = 0;
            if (unlockedMaterial.Contains(data)) { i = 1; }
            PlayerPrefs.SetInt(data.GetComponent<Item>().GetItemName(), i);
        }
        foreach(DungeonData data in dungeonDataBase)
        {
            int clear = 0;
            int unlocked = 0;
            if (clearedDungeon.Contains(data)) { clear = 1; }
            if (unlockedDungeon.Contains(data)) { unlocked = 1; }

            PlayerPrefs.SetInt(data.dungeonName + "_cleared", clear);
            PlayerPrefs.SetInt(data.dungeonName + "_unlocked", unlocked);
        }
        foreach (AlchemyRecipe data in recipeDataBase)
        {
            int i = 0;
            string key = string.Format("recipe_{0}", data.product.GetComponent<Item>().GetItemName());
            if (unlockedRecipe.Contains(data)) { i = 1; }
            PlayerPrefs.SetInt(key, i);
        }
        PlayerPrefs.SetInt("equipmentSlots", equipmentSlots);
    }
    public void LoadData()
    {
        foreach (GameObject data in itemDataBase)
        {
            if (PlayerPrefs.GetInt(data.GetComponent<Item>().GetItemName()) == 1)
            {
                unlockedMaterial.Add(data);
            }
        }
        foreach (DungeonData data in dungeonDataBase)
        {
            if (PlayerPrefs.GetInt(data.dungeonName + "_cleared") == 1)
            {
                clearedDungeon.Add(data);
            }
            if (PlayerPrefs.GetInt(data.dungeonName + "_unlocked") == 1)
            {
                unlockedDungeon.Add(data);
            }
        }
        foreach (AlchemyRecipe data in recipeDataBase)
        {
            string key = string.Format("recipe_{0}", data.product.GetComponent<Item>().GetItemName());
            if (PlayerPrefs.GetInt(key) == 1)
            {
                unlockedRecipe.Add(data);
            }
        }
        if (PlayerPrefs.HasKey("equipmentSlots")) { equipmentSlots = PlayerPrefs.GetInt("equipmentSlots"); }
    }

    public GameObject GetItemFromDataBase(string itemName) { return itemDataBase[itemNameDic[itemName]]; }
    public List<AlchemyRecipe> GetAlchemyRecipes() { return recipeDataBase; }
    public List<DungeonData> GetDungeonDatabase() { return dungeonDataBase; }
    public List<GameObject> GetEquipments() { return equipments; }
    public DungeonData GetSelectedDugeon() { return selectedDungeon; }
}
