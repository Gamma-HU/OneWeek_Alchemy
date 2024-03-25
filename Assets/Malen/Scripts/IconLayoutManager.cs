using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LayoutGroupManager : MonoBehaviour
{
    public LayoutGroup layoutGroup;
    public float animationDuration = 0.5f;
    public Vector2 moveAmount = new Vector2(100f, 100f); // X�������Y���̈ړ���

    private void Start()
    {
        // ���̃X�N���v�g���A�^�b�`�����Q�[���I�u�W�F�N�g�̎q�v�f�̐����Ď����A�ύX���������ꍇ�ɍĔz�u����
        CheckLayoutGroupChanges();
    }

    private void CheckLayoutGroupChanges()
    {
        // �q�v�f�̐����Ď����A�ύX���������ꍇ�ɍĔz�u����
        int previousChildCount = layoutGroup.transform.childCount;
        DOTween.Sequence().AppendCallback(() =>
        {
            int currentChildCount = layoutGroup.transform.childCount;
            if (previousChildCount != currentChildCount)
            {
                // LayoutGroup �̍Ĕz�u���A�j���[�V�����ōs��
                layoutGroup.DOKill(); // �A�j���[�V�������̏ꍇ�̓L�����Z��
                layoutGroup.enabled = false; // ���C�A�E�g�̎����������ꎞ�I�ɖ�����
                layoutGroup.enabled = true; // ���C�A�E�g�̍Čv�Z���g���K�[
                Vector3 moveVector = new Vector3(moveAmount.x, moveAmount.y, 0f);
                layoutGroup.transform.DOLocalMove(layoutGroup.transform.localPosition + moveVector, animationDuration).SetEase(Ease.OutCubic); // �ړ�����A�j���[�V���������s
            }
            previousChildCount = currentChildCount;
        }).SetUpdate(true); // Sequence �̍X�V�𖈃t���[���s���悤�ɐݒ�
    }
}
