using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconInRecipe : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public GameObject namePlatePfb;
    public GameObject itemPfb;
    public bool isProduct;

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
        if (!isProduct)
        {
            isMouswOn = true;
            namePlate = Instantiate(namePlatePfb, transform);
            Vector3 pos = namePlate.transform.position;
            namePlate.transform.GetChild(0).GetComponent<Text>().text = itemName;
        }
    }

    private void OnMouseExit()
    {
        isMouswOn = false;
        Destroy(namePlate);
    }

    // �N���b�N���ꂽ�Ƃ��ɌĂяo����郁�\�b�h(y_y)
    public void OnPointerClick(PointerEventData eventData)
    {
        print("�f�ނ��|�|�|�|�[��");
    }
}
