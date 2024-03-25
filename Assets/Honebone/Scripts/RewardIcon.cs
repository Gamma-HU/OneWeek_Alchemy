using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardIcon : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    Text rewardName;
    public void SetReward(Sprite sprite, string s)
    {
        icon.sprite = sprite;
        rewardName.text = s;
    }
}
