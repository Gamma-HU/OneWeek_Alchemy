using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ItemExplainManager : MonoBehaviour
{
    [SerializeField] GameObject explainObject;
    [SerializeField] Canvas canvas;
    [SerializeField] ItemExplainPropety itemExplainPropety;

    private GameObject displayedObject;
    private GameObject pastSelectedItem;

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hitInfo.collider != null)
            {
                //Debug.Log(hitInfo.collider.gameObject);
            }
            if (hitInfo.collider != null && hitInfo.collider.gameObject.GetComponent<Item>() != null)
            {
                ShowItemExplain(hitInfo.collider.gameObject);
            }
            else
            {
                DismissItemExplain();
            }

            if (displayedObject != null)
            {
                ItemExplainPropety.Corner[] corners = itemExplainPropety.cornerPriority;
                for (int i = 0; i < corners.Length; i++)
                {
                    Vector3 a = CornerToPosition(corners[i]);
                    Vector3 b = itemExplainPropety.offset;
                    Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, itemExplainPropety.defaultPosition[0].z) + new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
                    //Debug.Log(newPosition);
                    if (IsWithinCanvasBounds(newPosition))
                    {
                        displayedObject.transform.position = newPosition;
                        break;
                    }
                }
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            DismissItemExplain();
        }
    }

    public void ShowItemExplain(GameObject itemObject)
    {
        if (displayedObject == null || pastSelectedItem != itemObject)
        {
            if(displayedObject != null && pastSelectedItem != itemObject) {
                DismissItemExplain();
            }
            pastSelectedItem = itemObject;
            Item.ItemData itemData = itemObject.GetComponent<Item>().GetItemData();
            displayedObject = Instantiate(explainObject);
            ExplainText explainText = displayedObject.GetComponent<ExplainText>();
            displayedObject.transform.SetParent(canvas.transform);
            displayedObject.transform.localScale = Vector3.one;
            displayedObject.transform.position = Vector3.one * 100;
            explainText.itemNameText.text = itemData.itemName;
            explainText.explainText.text = itemData.GetItemInfo();
        }
    }

    public void DismissItemExplain()
    {
        if (displayedObject != null)
        {
            Destroy(displayedObject);
            displayedObject = null;
        }
    }

    private bool IsWithinCanvasBounds(Vector3 centerPosition)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector3[] canvasCorners = new Vector3[4];
        Vector3[] displayedObjectCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);
        for(int i = 0; i < 3; i++)
        {
            displayedObjectCorners[i] = centerPosition + itemExplainPropety.defaultPosition[i];
        }
        float borderX = -canvasCorners[0].x;
        float borderY = -canvasCorners[0].y;

        foreach (Vector3 corner in displayedObjectCorners)
        {
            if (Mathf.Abs(corner.x) > borderX || Mathf.Abs(corner.y) > borderY)
            {
                break;
            }
            if (corner == displayedObjectCorners[3])
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 CornerToPosition(ItemExplainPropety.Corner corner)
    {
        switch(corner)
        {
            case ItemExplainPropety.Corner.BottomLeft: 
                return new Vector3(-1, -1, 0);
            case ItemExplainPropety.Corner.TopLeft:
                return new Vector3(-1, 1, 0);
            case ItemExplainPropety.Corner.TopRight:
                return new Vector3(1, 1, 0);
            case ItemExplainPropety.Corner.BottomRight:
                return new Vector3(1, -1, 0);
            default:
                return Vector3.zero;
        }
    }
}
