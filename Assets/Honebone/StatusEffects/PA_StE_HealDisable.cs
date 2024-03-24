using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_HealDisable : PA_StatusEffects
{
    public override void OnInit()
    {
        characterStatus.RHeal_mul -= stack / 100f;
    }
    public override void OnAddStack(int add)
    {
        characterStatus.RHeal_mul -= add / 100f;
    }
    public override void OnHealed(int healedValue)
    {
        character.DisableStE(this);
        DisableStE();
    }
    public override void AtTheEnd()
    {
        characterStatus.RHeal_mul += stack / 100f;
    }
}
