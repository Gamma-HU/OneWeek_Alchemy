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
    Character player;
    [SerializeField]
    Image background;

    [SerializeField]
    float battleInterval;

    GameManager gameManager;
    DungeonEffect dungeonEffect;

    int layer;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();

        player.Init(battleManager,playerGauge);
        player.Equip(gameManager.GetEquipments());
        FindObjectOfType<BattleAnimationManager>().SetPlayerPropety(player.gameObject);

        StartExpedition(gameManager.GetSelectedDugeon());
        Debug.Log("�T���J�n");
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
            Debug.Log("�_���W�����N���A");
            CompleteExpedition();
        }
        else { StartCoroutine(BattleInterval()); }
    }
    void CompleteExpedition()
    {
        gameManager.ClearDungeon(currentDungeon);
        foreach(GameObject reward in currentDungeon.rewardItems)
        {
            Debug.Log(string.Format("�V���ȘB���f�ށu{0}�v���A�����b�N", reward.GetComponent<Item>().GetItemName()));
            gameManager.UnlockMaterial(reward);
        }
        foreach(DungeonData dungeon in currentDungeon.nextDungeons)
        {
            Debug.Log(string.Format("�V���ȃ_���W�����u{0}�v���A�����b�N", dungeon.dungeonName));
            gameManager.UnlockDungeon(dungeon);
        }

        gameManager.ReturnToAlchemyScene();
    }
  
    IEnumerator BattleInterval()
    {
        yield return new WaitForSeconds(battleInterval);
        battleManager.StartBattle(currentDungeon.enemies[layer]);
    }

    public DungeonData GetCurrentDungeon() { return currentDungeon; }
}
