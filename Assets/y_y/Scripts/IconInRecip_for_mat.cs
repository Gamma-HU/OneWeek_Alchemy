using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconInRecip_for_mat : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public GameObject namePlatePfb;

    GameObject namePlate;
    bool isMouswOn;
    Vector2 plateRelativePos = new Vector2(10, 10);

    private void Start()
    {

    }

    private void Update()
    {
        if (isMouswOn)
        {
            namePlate.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newPos = namePlate.transform.localPosition;
            newPos += plateRelativePos;
            namePlate.transform.localPosition = newPos;
        }
    }

    private void OnMouseEnter()
    {
        isMouswOn = true;
        namePlate = Instantiate(namePlatePfb, transform);
        Vector3 pos = namePlate.transform.position;
        namePlate.transform.GetChild(0).GetComponent<Text>().text = itemName;
    }

    private void OnMouseExit()
    {
        isMouswOn = false;
        Destroy(namePlate);
    }

    GameObject displayingItem;
    /// <summary>素材画面の方で生成した際に呼ばれる(honebone)</summary>
    public void Init_AsMaterialIcon(GameObject item)
    {
        displayingItem = item;
    }
    // クリックされたときに呼び出されるメソッド(y_y)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (displayingItem != null)
        {
            FindObjectOfType<AlchemySceneManager>().SpawnItem(displayingItem, Vector2.zero);
        }
    }
}
