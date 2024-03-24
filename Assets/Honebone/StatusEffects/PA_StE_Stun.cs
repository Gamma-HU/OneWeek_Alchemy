using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_Stun : PA_StatusEffects
{
    public override void OnInit()
    {
        characterStatus.stun++;
    }
    public override void OnStun()
    {
        ConsumeStack();
    }
    public override void AtTheEnd()
    {
        characterStatus.stun--;
    }
}
