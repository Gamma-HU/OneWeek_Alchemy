using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnimScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void AnimStart()
    {
        animator.Play("PanelAnimation");
    }
}
