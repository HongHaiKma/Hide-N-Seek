using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public override void OnCatchEnter()
    {
        base.OnCatchEnter();
        anim_Owner.SetTrigger(ConfigKeys.e_Catch);
    }
}
