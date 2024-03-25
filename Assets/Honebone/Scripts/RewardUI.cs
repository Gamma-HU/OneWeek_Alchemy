using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField]
    List<GameObject> rewardIconsObj;
    [SerializeField]
    List<RewardIcon> rewardIcons;

    [SerializeField]
    List<GameObject> noRewardsObj;

    [SerializeField]
    Animator anim;
    [SerializeField]
    float interval;


    DungeonData clearedDUngeon;
    private void Start()
    {
        clearedDUngeon = FindObjectOfType<GameManager>().GetSelectedDugeon();
    }
    public void ComleteDungeon()
    {
        anim.SetTrigger("Display");
    }
    public void StartDisplayRewards()
    {
        StartCoroutine(DisplayRewards());
    }
    IEnumerator DisplayRewards()
    {
        yield return new WaitForSeconds(interval);
        if(clearedDUngeon.rewardItems.Count > 0)
        {
            Item.ItemData item = clearedDUngeon.rewardItems[0].GetComponent<Item>().GetItemData();
            rewardIconsObj[0].SetActive(true);
            rewardIcons[0].SetReward(item.itemSprite, item.itemName);
        }
        else
        {
            noRewardsObj[0].SetActive(true);
        }
        yield return new WaitForSeconds(interval);

        if (clearedDUngeon.nextDungeons.Count > 0)
        {
            DungeonData dungeon1 = clearedDUngeon.nextDungeons[0];
            rewardIconsObj[1].SetActive(true);
            rewardIcons[1].SetReward(dungeon1.background, dungeon1.dungeonName);
        }
        else
        {
            noRewardsObj[1].SetActive(true);
        }
        yield return new WaitForSeconds(interval);
        if (clearedDUngeon.nextDungeons.Count > 1)
        {
            DungeonData dungeon2 = clearedDUngeon.nextDungeons[1];
            rewardIconsObj[2].SetActive(true);
            rewardIcons[2].SetReward(dungeon2.background, dungeon2.dungeonName);
        }

        yield return new WaitForSeconds(2f);
        FindObjectOfType<GameManager>().ReturnToAlchemyScene();
    }
}
