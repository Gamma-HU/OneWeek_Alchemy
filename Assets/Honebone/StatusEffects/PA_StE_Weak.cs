using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_Weak : PA_StatusEffects
{
    public override void OnInit()
    {
        characterStatus.exDMG_mul -= stack / 100f;
    }
    public override void OnAddStack(int add)
    {
        characterStatus.exDMG_mul -= add / 100f;
    }
    public override void OnAttack(int DMG, bool missed)
    {
        character.DisableStE(this);
        DisableStE();
    }
    public override void AtTheEnd()
    {
        characterStatus.exDMG_mul += stack / 100f;
    }
}
