using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionManager : MonoBehaviour
{
    [SerializeField]//test
    DungeonData currentDungeon;
    BattleManager battleManager;

    [SerializeField]
    GaugeManager playerGauge;
    [SerializeField]
    PAIconManager playerPAIcon;
    [SerializeField]
    Character player;
    [SerializeField]
    Image background;

    [SerializeField]
    float battleInterval;

    GameManager gameManager;
    DungeonEffect dungeonEffect;
    GameClearUI gameClearUI;
    RewardUI rewardUI;

    int layer;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();
        gameClearUI = FindObjectOfType<GameClearUI>();
        rewardUI = FindObjectOfType<RewardUI>();

        player.Init(battleManager,playerGauge, playerPAIcon);
        player.Equip(gameManager.GetEquipments());
        FindObjectOfType<BattleAnimationManager>().SetPlayerPropety(player.gameObject);

        StartExpedition(gameManager.GetSelectedDugeon());
        Debug.Log("探索開始");
    }

    public void StartExpedition(DungeonData dungeon)
    {
        currentDungeon = dungeon;
        background.sprite = currentDungeon.background;
        if (currentDungeon.dungeonEffect != null)
        {
            var d = Instantiate(dungeon.dungeonEffect);
            dungeonEffect = d.GetComponent<DungeonEffect>();
            dungeonEffect.Init(battleManager);
            battleManager.SetDungeonEffect(dungeonEffect);
        }
        battleManager.StartBattle(currentDungeon.enemies[0]);
    }
    public void NextLayer()
    {
        layer++;
        if (currentDungeon.enemies.Count == layer)
        {
            Debug.Log("ダンジョンクリア");
            CompleteExpedition();
        }
        else { StartCoroutine(BattleInterval()); }
    }
    void CompleteExpedition()
    {
        gameManager.ClearDungeon(currentDungeon);
        foreach(GameObject reward in currentDungeon.rewardItems)
        {
            Debug.Log(string.Format("新たな錬金素材「{0}」をアンロック", reward.GetComponent<Item>().GetItemName()));
            gameManager.UnlockMaterial(reward);
        }
        foreach(DungeonData dungeon in currentDungeon.nextDungeons)
        {
            Debug.Log(string.Format("新たなダンジョン「{0}」をアンロック", dungeon.dungeonName));
            gameManager.UnlockDungeon(dungeon);
        }

        if (gameManager.GetCleardDungeon().Count == gameManager.GetDungeonDatabase().Count)
        {
            gameClearUI.ComleteDungeon();
        }
        else
        {
            rewardUI.ComleteDungeon();
        }
    }
  
    IEnumerator BattleInterval()
    {
        yield return new WaitForSeconds(battleInterval);
        battleManager.StartBattle(currentDungeon.enemies[layer]);
    }

    public DungeonData GetCurrentDungeon() { return currentDungeon; }
}
