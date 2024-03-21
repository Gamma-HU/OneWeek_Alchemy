using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StatusEffects : PassiveAbility
{
    protected int stack;
    public void StEInit(int s)
    {
        stack = s;
    }
    public void AddStack(int add)
    {
        if (stack + add <= 0) { add = stack * -1; }
        stack += add;
        OnAddStack(add);

        if (stack <= 0)
        {
            AtTheEnd();
            //”j‰ó
        }
    }
    public virtual void OnInit() { }
    public virtual void OnAddStack(int add) { }
    public virtual void AtTheEnd() { }
}
