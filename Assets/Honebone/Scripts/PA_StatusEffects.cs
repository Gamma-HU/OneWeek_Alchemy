using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StatusEffects : PassiveAbility
{
    [SerializeField]
    bool isBuff;
    protected int stack;

    private StEIconManager stEIconManager;
    StEIcon StEicon;

    private void Start()
    {
        stEIconManager = FindObjectOfType<StEIconManager>();
    }

    public void StEInit(int s,StEIcon icon)
    {
        stack = s;
        StEicon = icon;
        OnInit();
    }
    public void AddStack(int add)
    {
        if (add < 0) { Debug.Log("error：増加するスタック数が負の数になっています"); }
        stack += add;
        StEicon.SetStack(stack);
        OnAddStack(add);
    }
    public void ConsumeStack()
    {
        stack--;
        StEicon.SetStack(stack);
        if (stack == 0)
        {
            character.DisableStE(this);
            DisableStE();
        }
    }
    public void DisableStE()
    {
        AtTheEnd();
        Destroy(StEicon.gameObject);
        Destroy(gameObject);
    }

    public void SetStack(int stack)
    {
        if (stack > 0)
        {
            transform.parent.GetComponent<StEIcon>().stackText.text = stack.ToString();
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

    }

    public override void OnDie()
    {
        Destroy(StEicon.gameObject);
    }

    public virtual void OnInit() { }
    public virtual void OnAddStack(int add) { }
    public virtual void AtTheEnd() { }

    public bool GetIsBuff() { return isBuff; }
}
