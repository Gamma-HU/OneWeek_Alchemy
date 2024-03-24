using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// �s���̃A�j���[�V�����ƃ_���[�W�Ə�Ԉُ�̕\�����Ǘ�
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
    /// �U���A�j���[�V�������Đ�
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
    /// ��_���[�W�A�j���[�V�������Đ�
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
    /// �񕜃A�j���[�V�������Đ�
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
    /// �_���[�W�\�� (��_���[�W�̓}�C�i�X�l�A�^�_���[�W�̓v���X�l�Ŏw��)
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
    /// �A�r���e�B�t�^�\��
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

        // �o�t�E�f�o�t���ʗp�̃t�B�[���h��p�ӂ�����R�����g�A�E�g���������Ă�������
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
    /// �A�r���e�B���D�\��
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

        // �o�t�E�f�o�t���ʗp�̃t�B�[���h��p�ӂ�����R�����g�A�E�g���������Ă�������
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
    /// �v���C���[�̍s���A�j���[�V�����p�̃v���p�e�B��ݒ�
    /// </summary>
    /// <param name="playerObject"></param>
    public void SetPlayerPropety(GameObject playerObject)
    {
        player.characterObject = playerObject;
        player.characterSide = CharacterSide.Right;
        player.defaultPosition = playerObject.transform.position;
    }

    /// <summary>
    /// �G�̍s���A�j���[�V�����p�̃v���p�e�B��ݒ�
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
