using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //[SerializeField] string itemName;
    //[SerializeField] bool canEquip;
    //[SerializeField] GameObject passiveAbility;
    [System.Serializable]
    public class ItemData
    {
        public int ID;
        public string itemName;
        public Sprite itemSprite;
        public bool canEquip;
        public bool weapon;
        public bool armor;

        public bool backpack;
        
        [Header("装備可能アイテムのみ")] public GameObject passiveAbility;
        public string GetItemInfo()
        {
            string s = ""; //string.Format("{0}\n");
            if (canEquip) { s += "[装備品]\n"; }
            else if(backpack){ s += "これを初めて錬金したとき、探索に持っていける装備品の数が増える\n"; }
            else { s += "[錬金素材]\n"; }
            if (weapon) { s += "<<武器>>\n"; }
            if (armor) { s += "<<防具>>\n"; }
            if (passiveAbility != null)
            {
                s += passiveAbility.GetComponent<PassiveAbility>().GetInfo();
            }
            return s;
        }

        public void SetID(int id) { ID = id; }
    }
    [SerializeField]
    ItemData itemData;
    bool dragging;
    Rigidbody2D rb;

    AlchemySceneManager alchemySceneManager;
    AlchemyManager alchemyManager;

    AlchemySlot onSlot_Alchemy;
    EquipmentSlot onSlot_Equipment;
    bool onTrash;

    public AudioClip SE_hold;

    void Start()
    {
        Init();//test
    }
    public void Init()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        SetOutline(false);

        rb = GetComponent<Rigidbody2D>();
        alchemySceneManager = FindObjectOfType<AlchemySceneManager>();
        alchemyManager = FindObjectOfType<AlchemyManager>();
    }

    public void SetOutline(bool set)
    {
        transform.GetChild(0).gameObject.SetActive(set);
    }
    public void SetDragging(bool set)
    {
        dragging = set;
        SetOutline(set);
        if (dragging)//ドラッグ開始
        {
            SEManager seManager = FindFirstObjectByType<SEManager>();
            seManager.PlaySE(SE_hold);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.angularVelocity = 0;
            if (onSlot_Alchemy != null && !itemData.canEquip)//錬金スロット上にあるなら
            {
                onSlot_Alchemy.ResetItem();
            }
            if (onSlot_Equipment != null && itemData.canEquip)//装備品が装備品スロット上にあるなら
            {
                onSlot_Equipment.ResetItem();
            }

            alchemySceneManager.SetDraggingItemText(string.Format("{0}\n{1}", itemData.itemName,itemData.GetItemInfo()));
        }
        else//ドラッグ終了
        {
            rb.velocity = Vector2.zero;
            alchemySceneManager.SetDraggingItemText("");
            if (onSlot_Alchemy != null && !itemData.canEquip)//錬金スロット上にあるなら
            {
                onSlot_Alchemy.SetItem(this);
                rb.MovePosition(onSlot_Alchemy.transform.position);
            }
            else if (onSlot_Equipment != null && itemData.canEquip)//装備品が装備品スロット上にあるなら
            {
                onSlot_Equipment.SetItem(this);
                rb.MovePosition(onSlot_Equipment.transform.position);
            }
            else if (onTrash)
            {
                Destroy(gameObject);
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            rb.velocity = Vector2.zero;
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
        onSlot_Alchemy = null;
        onSlot_Equipment = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Snap();
    }
    public void Snap()
    {
        float deg = Random.Range(0f, 360f);
        Vector2 dir = deg.UnitCircle();
        rb.AddForce(dir * 1000f);
        rb.AddTorque(Random.Range(500f,1500f));
        alchemySceneManager.PlaySE_Snap();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (dragging)
        {
            if (collision.CompareTag("AlchemySlot"))
            {
                onSlot_Alchemy = collision.GetComponent<AlchemySlot>();
            }
            if (collision.CompareTag("EquipmentSlot"))
            {
                onSlot_Equipment = collision.GetComponent<EquipmentSlot>();
            }
            if (collision.CompareTag("Trash"))
            {
                onTrash = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dragging)
        {
            if (collision.CompareTag("AlchemySlot"))
            {
                onSlot_Alchemy = null;
            }
            if (collision.CompareTag("EquipmentSlot"))
            {
                onSlot_Equipment = null;
            }
            if (collision.CompareTag("Trash"))
            {
                onTrash = false;
            }
        }
       
    }

    public string GetItemName() { return itemData.itemName; }
    public string GetItemInfo() { return itemData.GetItemInfo(); }
    public ItemData GetItemData() { return itemData; }
}
