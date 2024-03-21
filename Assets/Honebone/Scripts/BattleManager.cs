using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public Character owner;
        public Character target;

        public bool attack;//UŒ‚‚ğ‚·‚é
        public int DMG;//ƒ_ƒ[ƒW—Ê
        public int heal;
        public List<StEParams> applyStE;
        public List<StEParams> removeStE;
    }
    [System.Serializable]
    public class StEParams
    {
        public GameObject StE;//•t—^/œ‹‚·‚éó‘ÔˆÙí
        public int amount;//•t—^/œ‹‚·‚é”
    }
    [SerializeField]
    Action attack;
    [SerializeField]
    float actionInterval;
    [SerializeField]
    float turnInterval;

    List<Action> actionQueue = new List<Action>();

    [SerializeField]//test
    Character player;
    [SerializeField]//test
    Character enemy;
    bool playerTurn;
    private void Start()//test
    {
        player.Init();
        player.SetOpponent(enemy);
        enemy.Init();
        enemy.SetOpponent(player);
        BattleStart();
    }
    public void BattleStart()
    {
        NextTurn();//test –{—ˆ‚Íí“¬ŠJn—U”­
    }
    public void NextTurn()
    {
        
        playerTurn = !playerTurn;
        if (playerTurn)
        {
            Debug.Log(string.Format("{0}‚ÌUŒ‚", player.GetCharacterStatus().charaName));
            Enqueue(player, enemy, attack);
        }
        else
        {
            Debug.Log(string.Format("{0}‚ÌUŒ‚", enemy.GetCharacterStatus().charaName));
            Enqueue(enemy, player, attack);
        }
        StartResolve();
    }
    public void BattleEnd()
    {
        playerTurn = false;
        actionQueue.Clear();
    }

   
    public void Enqueue(Character owner,Character target,Action action)
    {
        action.owner = owner;
        action.target = target;
        actionQueue.Add(action);
    }
    void StartResolve()
    {
        Resolve();
    }
    void Resolve()
    {
        if(actionQueue.Count > 0)
        {
            Action action = actionQueue[0];
            if (action.attack) { action.owner.Attack(); }

            if (action.heal > 0) { action.target.Heal(action.heal); }

            actionQueue.RemoveAt(0);
            CheckBattleEnd();
        }
        else
        {
            EndResolve();
        }
    }
    void CheckBattleEnd()
    {
        //Šm”F
        StartCoroutine(ActionInterval());//test
    }
    void EndResolve()
    {
        StartCoroutine(TurnInterval());
    }

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
}
