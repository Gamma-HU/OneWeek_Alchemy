﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarTest : MonoBehaviour
{

    public Button button;
    public GaugeManager manager;
    public BattleAnimationManager bam;
    public GameObject player;

    private void Start()
    {
        bam.SetPlayerPropety(player);
    }

    public void OnClicked(float ratio)
    {
        manager.UpdateHPGauge(ratio);
    }

    public void Emerge()
    {
        Character character = player.GetComponent<Character>();
        //bam.SetPlayerPropety(player);
        bam.PlayEmergeAnimation(character.GetCharacterStatus());
    }

    public void Disappear()
    {
        Character character = player.GetComponent<Character>();
        //bam.SetPlayerPropety(player);
        bam.PlayDisappearAnimation(character.GetCharacterStatus());
    }

    public void Attack()
    {
        Character character = player.GetComponent<Character>();
        //bam.SetPlayerPropety(player);
        bam.PlayAttackAnimation(character.GetCharacterStatus());
    }

    public void Damaged()
    {
        Character character = player.GetComponent<Character>();
        //bam.SetPlayerPropety(player);
        bam.PlayDamagedAnimation(character.GetCharacterStatus());
    }

    public void Healed()
    {
        Character character = player.GetComponent<Character>();
        //bam.SetPlayerPropety(player);
        bam.PlayHealedAnimation(character.GetCharacterStatus());
    }

    public void Display(float amount)
    {
        Character character = player.GetComponent<Character>();
        //bam.ShowDamageIndicator(character.gameObject, amount);
    }
}
