using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// 行動のアニメーションとダメージと状態異常の表示を管理
/// </summary>
public class BattleAnimationManager : MonoBehaviour
{

    [SerializeField] BattleAnimationPropety battleAnimationPropety;
    [SerializeField] Canvas indicatorCanvas;

    private CharacterAnimationInfo player;
    private CharacterAnimationInfo enemy;

    private enum CharacterSide { Right, Left }
    private enum CharacterType { Player, Enemy }

    private void Start()
    {
        player = new CharacterAnimationInfo();
        enemy = new CharacterAnimationInfo();
    }

    private class CharacterAnimationInfo
    {
        public GameObject characterObject;
        public CharacterSide characterSide;
        public Vector3 defaultPosition;
    }

    /// <summary>
    /// 攻撃アニメーションを再生
    /// </summary>
    /// <param name="status"></param>
    public void PlayAttackAnimation(Character.CharacterStatus status)
    {
        if (battleAnimationPropety.isAnimationPlayedOnAttack)
        {
            CharacterAnimationInfo info = GetInfo(status);
            Sequence sequence = DOTween.Sequence();
            if (info.characterSide == CharacterSide.Right)
            {
                sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition - new Vector3(battleAnimationPropety.forwardDistanceOnAttack, 0, 0), battleAnimationPropety.durationOnAttack / 2).SetEase(Ease.InQuad));
                sequence.Join(info.characterObject.transform.DORotate(new Vector3(0, 0, battleAnimationPropety.angleOnAttack), battleAnimationPropety.durationOnAttack / 2, RotateMode.Fast).SetEase(Ease.InQuad));
            }
            else
            {
                sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition + new Vector3(battleAnimationPropety.forwardDistanceOnAttack, 0, 0), battleAnimationPropety.durationOnAttack / 2));
                sequence.Join(info.characterObject.transform.DORotate(new Vector3(0, 0, -battleAnimationPropety.angleOnAttack), battleAnimationPropety.durationOnAttack / 2, RotateMode.Fast));
            }
            sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition, battleAnimationPropety.durationOnAttack / 2));
            sequence.Join(info.characterObject.transform.DORotate(Vector3.zero, battleAnimationPropety.durationOnAttack / 2, RotateMode.Fast));
        }
    }

    /// <summary>
    /// 被ダメージアニメーションを再生
    /// </summary>
    /// <param name="status"></param>
    public void PlayDamagedAnimation(Character.CharacterStatus status)
    {
        if (battleAnimationPropety.isAnimationPlayedOnDamaged)
        {
            CharacterAnimationInfo info = GetInfo(status);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition, battleAnimationPropety.durationOnDamaged / 10));
            sequence.Join(info.characterObject.transform.DORotate(Vector3.zero, battleAnimationPropety.durationOnDamaged / 10, RotateMode.Fast));
            sequence.Join(info.characterObject.transform.DOShakePosition(battleAnimationPropety.durationOnDamaged, battleAnimationPropety.vibrationStrengthOnDamaged, battleAnimationPropety.vibrationCountOnDamaged));
        }
    }

    /// <summary>
    /// 回復アニメーションを再生
    /// </summary>
    /// <param name="status"></param>
    public void PlayHealedAnimation(Character.CharacterStatus status)
    {
        if(battleAnimationPropety.isAnimationPlayedOnHealed)
        {
            CharacterAnimationInfo info = GetInfo(status);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition, battleAnimationPropety.durationOnHealed / 10));
            sequence.Join(info.characterObject.transform.DORotate(Vector3.zero, battleAnimationPropety.durationOnHealed / 10, RotateMode.Fast));
            sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition + new Vector3(0, battleAnimationPropety.jumpDistanceOnHealed, 0), battleAnimationPropety.durationOnHealed / 2));
            sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition, battleAnimationPropety.durationOnHealed / 2));
        }
    }

    /// <summary>
    /// ダメージ表示 (被ダメージはマイナス値、与ダメージはプラス値で指定)
    /// </summary>
    /// <param name="targetCharacter"></param>
    /// <param name="amount"></param>
    public void ShowDamageIndicator(GameObject targetCharacter, float amount)
    {
        GameObject indicator = Instantiate(battleAnimationPropety.damageIndicator);
        float scattering = battleAnimationPropety.damageScattering;
        indicator.transform.SetParent(indicatorCanvas.transform);
        indicator.transform.position = targetCharacter.transform.position + new Vector3(Random.Range(-scattering, scattering), Random.Range(-scattering, scattering), 0);
        indicator.transform.localScale = Vector3.one;
        Text text = indicator.GetComponent<Text>();
        Outline outline = text.GetComponent<Outline>();
        if (amount < 0)
        {
            text.color = battleAnimationPropety.damageColor;
            outline.effectColor = battleAnimationPropety.damageColorOutline;
            text.text = "" + amount;
        }
        else
        {
            text.color = battleAnimationPropety.healColor;
            outline.effectColor = battleAnimationPropety.healColorOutline;
            text.text = "+" + amount;
        }
    }

    /// <summary>
    /// アビリティ付与表示
    /// </summary>
    /// <param name="targetCharacter"></param>
    /// <param name="stEParams"></param>
    public void ShowAbilityApplyed(GameObject targetCharacter, BattleManager.StEParams stEParams)
    {
        string StEName = stEParams.StE.GetComponent<PassiveAbility>().GetPAName();
        GameObject indicator = Instantiate(battleAnimationPropety.paramsIndicator);
        float scattering = battleAnimationPropety.paramsScattering;
        indicator.transform.SetParent(indicatorCanvas.transform);
        indicator.transform.position = targetCharacter.transform.position + new Vector3(Random.Range(-scattering, scattering), Random.Range(-scattering, scattering), 0);
        indicator.transform.localScale = Vector3.one;
        Text text = indicator.GetComponent<Text>();
        Outline outline = text.GetComponent<Outline>();
        Character.CharacterStatus status = targetCharacter.GetComponent<Character>().GetCharacterStatus();
        text.text = string.Format("+{0} {1}", StEName, stEParams.amount);

        // バフ・デバフ判別用のフィールドを用意したらコメントアウトを解除してください
        /*if (stEParams.isBuff)
        {
            text.color = battleAnimationPropety.paramsColorBuff;
            outline.effectColor = battleAnimationPropety.paramsColorBuffOutline;
        }
        else
        {
            text.color = battleAnimationPropety.paramsColorDebuff;
            outline.effectColor = battleAnimationPropety.paramsColorDebuffOutline;
        }*/
    }

    /// <summary>
    /// アビリティ剥奪表示
    /// </summary>
    /// <param name="targetCharacter"></param>
    /// <param name="stEParams"></param>
    public void ShowAbilityRemoved(GameObject targetCharacter, BattleManager.StEParams stEParams)
    {
        string StEName = stEParams.StE.GetComponent<PassiveAbility>().GetPAName();
        GameObject indicator = Instantiate(battleAnimationPropety.paramsIndicator);
        float scattering = battleAnimationPropety.paramsScattering;
        indicator.transform.SetParent(indicatorCanvas.transform);
        indicator.transform.position = targetCharacter.transform.position + new Vector3(Random.Range(-scattering, scattering), Random.Range(-scattering, scattering), 0);
        indicator.transform.localScale = Vector3.one;
        Text text = indicator.GetComponent<Text>();
        Character.CharacterStatus status = targetCharacter.GetComponent<Character>().GetCharacterStatus();
        text.text = string.Format("-{0}", StEName);

        // バフ・デバフ判別用のフィールドを用意したらコメントアウトを解除してください
        /*if (stEParams.isBuff)
        {
            text.color = battleAnimationPropety.paramsColorBuff;
            outline.effectColor = battleAnimationPropety.paramsColorBuffOutline;
        }
        else
        {
            text.color = battleAnimationPropety.paramsColorDebuff;
            outline.effectColor = battleAnimationPropety.paramsColorDebuffOutline;
        }*/
    }

    /// <summary>
    /// プレイヤーの行動アニメーション用のプロパティを設定
    /// </summary>
    /// <param name="playerObject"></param>
    public void SetPlayerPropety(GameObject playerObject)
    {
        player.characterObject = playerObject;
        player.characterSide = CharacterSide.Right;
        player.defaultPosition = playerObject.transform.position;
    }

    /// <summary>
    /// 敵の行動アニメーション用のプロパティを設定
    /// </summary>
    /// <param name="enemyObject"></param>
    public void SetEnemyProtery(GameObject enemyObject)
    {
        enemy.characterObject = enemyObject;
        enemy.characterSide = CharacterSide.Left;
        enemy.defaultPosition = enemyObject.transform.position;
    }

    private CharacterAnimationInfo GetInfo(Character.CharacterStatus status)
    {
        if (status.player)
        {
            return player;
        }
        else
        {
            return enemy;
        }
    }
}
