using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconInRecipe : MonoBehaviour
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
}
