using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_Vulnerable : PA_StatusEffects
{
    public override void OnInit()
    {
        characterStatus.PROT_mul -= stack / 100f;
    }

    public override void OnAddStack(int add)
    {
        characterStatus.PROT_mul -= add / 100f;
    }
    public override void OnDamaged(int DMG, bool byOpponent)
    {
        if (byOpponent)
        {
            character.DisableStE(this);
            DisableStE();
        }
    }
    public override void AtTheEnd()
    {
        characterStatus.PROT_mul += stack / 100f;
    }
}
