using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//戦闘に関するスクリプト
public class BattleManager : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public Character owner;
        public Character target;
        public Sprite icon;

        public bool attack;//攻撃をする
        public int DMG;//ダメージ量
        public int heal;
        public List<StEParams> applyStE = new List<StEParams>();
        public List<GameObject> removeStE = new List<GameObject>();
    }
    [System.Serializable]
    public class StEParams
    {
        public GameObject StE;//付与/除去する状態異常
        public int amount;//付与/除去する数
    }
    [SerializeField, Header("いじるべからず")]
    Action attack;

    [SerializeField,Header("以下２つは戦闘速度に関連")]
    float actionInterval;
    [SerializeField]
    float turnInterval;
    [SerializeField, Header("敵のオブジェクトの親となる\nこの場所に敵が生成")]
    Transform enemyP;

    List<Action> actionQueue = new List<Action>();

    [SerializeField]
    GaugeManager enemyGauge;
    [SerializeField]
    PAIconManager enemyPAIcon;
    [SerializeField]
    Character player;
    Character enemy;
    bool playerTurn;

    [SerializeField]
    SEManager SEManager;
    [SerializeField] private AudioClip SE_attack;
    [SerializeField] private AudioClip SE_attacked;
    [SerializeField] private AudioClip SE_heal;
    [SerializeField] private AudioClip SE_buff;
    [SerializeField] private AudioClip SE_deBuff;

    DungeonEffect dungeonEffect;

    GameManager gameManager;
    ExpeditionManager expeditionManager;
    BattleAnimationManager battleAnimManager;
    private void Start()//test
    {
        gameManager = FindObjectOfType<GameManager>();
        expeditionManager = FindObjectOfType<ExpeditionManager>();
        battleAnimManager = FindObjectOfType<BattleAnimationManager>();
    }
    public void StartBattle(GameObject enemyObj)
    {
        var e = Instantiate(enemyObj, enemyP);
        enemy = e.GetComponent<Character>();
        enemy.Init(this, enemyGauge, enemyPAIcon);
        battleAnimManager.SetEnemyProtery(e);
        battleAnimManager.PlayEmergeAnimation(enemy.GetCharacterStatus());

        enemy.SetOpponent(player);
        player.SetOpponent(enemy);

        Debug.Log(player.GetInfo());
        Debug.Log(enemy.GetInfo());
        Debug.Log("戦闘開始");

        StartCoroutine(BattleStartDelay());
    }
    IEnumerator BattleStartDelay()
    {
        yield return new WaitForSeconds(1f);
        if (dungeonEffect != null) { dungeonEffect.OnBattleStart(); }
        player.OnBattleStart();
        enemy.OnBattleStart();
        StartResolve();
    }
    public void NextTurn()
    {
        
        playerTurn = !playerTurn;
        if (playerTurn)
        {
            Debug.Log(string.Format("{0}の攻撃", player.GetCharacterStatus().charaName));
            Enqueue(player, enemy, attack,null);
        }
        else
        {
            Debug.Log(string.Format("{0}の攻撃", enemy.GetCharacterStatus().charaName));
            Enqueue(enemy, player, attack,null);
        }
        StartResolve();
    }
    public void BattleEnd(bool victory)
    {
        playerTurn = false;
        actionQueue.Clear();
        if (victory)//敵が死亡したなら
        {
            Debug.Log("勝利");
            Destroy(enemyP.GetChild(0).gameObject,0.5f);//敵のオブジェクトを破壊
            expeditionManager.NextLayer();
        }
        else
        {
            gameManager.ReturnToAlchemyScene();
        }
    }

   
    public void Enqueue(Character owner,Character target,Action action,Sprite icon)
    {
        action.owner = owner;
        action.target = target;
        action.icon = icon;
        actionQueue.Add(action);
    }
    void StartResolve()
    {
        if (actionQueue.Count > 0) { Resolve(); }
        else { NextTurn(); }
    }
    void Resolve()
    {
        if (actionQueue.Count > 0)
        {
            Action action = actionQueue[0];
            if (!action.owner.GetCharacterStatus().dead && !action.target.GetCharacterStatus().dead)
            {
                if (action.attack) { action.owner.Attack(); }
                if (action.DMG > 0) { action.target.Damage(action.DMG, false); }
                if (action.heal > 0) { action.target.Heal(action.heal); }
                foreach (StEParams StEParams in action.applyStE)
                {
                    action.target.ApplyStE(StEParams);
                }
                foreach (GameObject removeStE in action.removeStE)
                {
                    action.target.RemoveStE(removeStE);
                }

                if (action.icon != null) { action.target.GetPAIconManager().SetIcon(action.icon); }
            }
            actionQueue.RemoveAt(0);
            StartCoroutine(ActionInterval());
        }
        else
        {
            if (!CheckBattleEnd()) { EndResolve(); }
        }
    }
    bool CheckBattleEnd()
    {
        if (player.GetCharacterStatus().dead)//敗北
        {
            BattleEnd(false);
            return true;
        }
        if (enemy.GetCharacterStatus().dead)//勝利
        {
            BattleEnd(true);
            return true;
        }
        return false;
    }
    void EndResolve()
    {
        StartCoroutine(TurnInterval());
    }

    public void SetDungeonEffect(DungeonEffect effect) { dungeonEffect = effect; }

    IEnumerator ActionInterval()
    {
        yield return new WaitForSeconds(actionInterval);
        Resolve();
    }
    IEnumerator TurnInterval()
    {
        yield return new WaitForSeconds(turnInterval);
        NextTurn();
    }

    public void PlaySE_Attack() { SEManager.PlaySE(SE_attack); }
    public void PlaySE_Attacked() { SEManager.PlaySE(SE_attacked); }
    public void PlaySE_ApplyBuff() { SEManager.PlaySE(SE_buff); }
    public void PlaySE_ApplyDebuff() { SEManager.PlaySE(SE_deBuff); }
    public void PlaySE_Heal() { SEManager.PlaySE(SE_heal); }

    public Character GetPlayer() { return player; }
    public Character GetEnemy() { return enemy; }
}
