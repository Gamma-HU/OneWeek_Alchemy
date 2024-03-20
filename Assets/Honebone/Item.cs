using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    string itemName;
    bool dragging;
    Rigidbody2D rb;

    AlchemySlot onSlot;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDragging(bool set)
    {
        dragging = set;
        if (dragging)//ドラッグ開始
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.angularVelocity = 0;
            if(onSlot != null)//錬金スロット上にあるなら
            {
                onSlot.ResetItem();
            }
        }
        else//ドラッグ終了
        {
            rb.velocity = Vector2.zero;

            if(onSlot != null)//錬金スロット上にあるなら
            {
                onSlot.SetItem(this);
                rb.MovePosition(onSlot.transform.position);
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void Update()
    {
        if (dragging) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(pos);
        }
    }

    public void ResetSlot()
    {
        onSlot = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Snap();
    }
    public void Snap()
    {
        float deg = Random.Range(0f, 360f);
        Vector2 dir = deg.UnitCircle();
        rb.AddForce(dir * 1000f);
        rb.AddTorque(Random.Range(500f,1500f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dragging && collision.CompareTag("AlchemySlot"))
        {
            onSlot = collision.GetComponent<AlchemySlot>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dragging && collision.CompareTag("AlchemySlot"))
        {
            onSlot = null;
        }
    }

    public string GetItemName() { return itemName; }
}
