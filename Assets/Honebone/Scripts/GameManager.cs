using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    DungeonData firstDangeon;//次のダンジョンはこのデータ内に格納
    [SerializeField]//test
    List<DungeonData> unlockedDungeon = new List<DungeonData>();//解放されている未クリアのダンジョン
    List<GameObject> unlockedMaterial = new List<GameObject>();
    List<AlchemyRecipe> unlockedRecipe = new List<AlchemyRecipe>();
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
    public void EnterDungeon(DungeonData enter,List<PassiveAbility> equipments)
    {

    }
    public List<DungeonData> GetUnlockedDungeon() { return unlockedDungeon; }
    public List<GameObject> GetUnlockedMaterial() { return unlockedMaterial; }
    public List<AlchemyRecipe> GEtUnlockedRecipe() { return unlockedRecipe; }
}
