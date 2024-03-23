using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;
using UnityEngine.TextCore.Text;

public class BattleAnimationManager : MonoBehaviour
{

    [SerializeField] BattleAnimationPropety battleAnimationPropety;

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

    public void PlayAttackAnimation(Character.CharacterStatus status)
    {
        CharacterAnimationInfo info = GetInfo(status);
        if (battleAnimationPropety.isAnimationPlayedOnAttack)
        {
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

    public void PlayDamagedAnimation(Character.CharacterStatus status)
    {
        CharacterAnimationInfo info = GetInfo(status);
        if (battleAnimationPropety.isAnimationPlayedOnDamaged)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(info.characterObject.transform.DOMove(info.defaultPosition, battleAnimationPropety.durationOnAttack / 10));
            sequence.Join(info.characterObject.transform.DORotate(Vector3.zero, battleAnimationPropety.durationOnAttack / 10, RotateMode.Fast));
            sequence.Join(info.characterObject.transform.DOShakePosition(battleAnimationPropety.durationOnDamaged, battleAnimationPropety.vibrationStrengthOnDamaged, battleAnimationPropety.vibrationCountOnDamaged));
        }
    }

    public void PlayHealedAnimation()
    {
        if(battleAnimationPropety.isAnimationPlayedOnHealed)
        {

        }
    }

    public void SetPlayerPropety(GameObject playerObject)
    {
        player.characterObject = playerObject;
        player.characterSide = CharacterSide.Right;
        player.defaultPosition = playerObject.transform.position;
    }

    public void SetEnemyProtery(GameObject enemyObject)
    {
        enemy.characterObject = enemyObject;
        enemy.characterSide = CharacterSide.Right;
        enemy.defaultPosition = enemyObject.transform.position;
    }

    private CharacterAnimationInfo GetInfo(Character.CharacterStatus status)
    {
        if (status.charaName == "ÉvÉåÉCÉÑÅ[")
        {
            return player;
        }
        else
        {
            return enemy;
        }
    }
}
