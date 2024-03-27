using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ClearUnlockedItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject framePfb_for_clearUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip SE_getReward;

    [Header("報酬の表示の間隔")]
    [SerializeField] private float interval;

    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            StartCoroutine(ActivateClearUI());
            test = false;
        }
    }

    public IEnumerator ActivateClearUI()
    {
        DungeonData currentDungeon = gameManager.GetComponent<GameManager>().GetSelectedDugeon();
        List<GameObject> rewardItems = currentDungeon.rewardItems;

        for (int i = 0; i < rewardItems.Count; i++)
        {
            GenerateRewardItems(rewardItems[i]);
            yield return new WaitForSeconds(interval);
        }
    }

    private void GenerateRewardItems(GameObject item)
    {
        GameObject framePfb = Instantiate(framePfb_for_clearUI, transform.position, Quaternion.identity);
        framePfb.transform.SetParent(this.transform, false);

        //画像とテキストを設定
        ItemData itemData = item.GetComponent<Item>().GetItemData();
        framePfb.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemData.itemSprite;
        framePfb.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = itemData.itemName;


        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_getReward);
    }


}
