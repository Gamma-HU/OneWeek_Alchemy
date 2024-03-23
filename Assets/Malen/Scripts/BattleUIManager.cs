using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    [SerializeField] [Header("プレイヤーオブジェクト")] private GameObject player;
    [SerializeField] [Header("プレイヤーHPバー")] private GaugeManager playerHPBar;
    [SerializeField] [Header("敵オブジェクト(Parent)")] private GaugeManager enemyParent;
    [SerializeField] [Header("敵HPバー")] private GameObject enemyHPBar;
    

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI enemyText;

    private Character characterPlayer;
    private GameObject enemy;
    private Character characterEnemy;

    void Start()
    {
        characterPlayer = player.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            try
            {
                enemy = enemyParent.transform.GetChild(0).gameObject;
            }
            catch {
                Debug.Log("モンスターいないよ");
            }
        }
        characterPlayer = player.GetComponent<Character>();
        Character.CharacterStatus playerStatus = characterPlayer.GetCharacterStatus();
        playerText.text = playerStatus.HP + "";

    }

    
}
