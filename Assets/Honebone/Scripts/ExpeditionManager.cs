using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    [SerializeField]//test
    DungeonData currentDungeon;
    BattleManager battleManager;

    [SerializeField]
    GaugeManager playerGauge;
    [SerializeField]
    Character player;

    [SerializeField]
    float battleInterval;

    int layer;
    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();

        player.Init(battleManager,playerGauge);
        player.Equip(FindObjectOfType<GameManager>().GetEquipments());

        StartExpedition(FindObjectOfType<GameManager>().GetSelectedDugeon());
        Debug.Log("探索開始");
    }

    public void StartExpedition(DungeonData dungeon)
    {
        currentDungeon = dungeon;
        battleManager.StartBattle(currentDungeon.enemies[0]);
    }
    public void NextLayer()
    {
        layer++;
        if (currentDungeon.enemies.Count == layer)
        {
            Debug.Log("ダンジョンクリア");
        }
        else { StartCoroutine(BattleInterval()); }
    }
    public void EndExpedition()
    {
        
    }
    IEnumerator BattleInterval()
    {
        yield return new WaitForSeconds(battleInterval);
        battleManager.StartBattle(currentDungeon.enemies[layer]);
    }

    public DungeonData GetCurrentDungeon() { return currentDungeon; }
}
