using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttack : Attack
{
    public SuperAttack(AttackData attackData, Player player) : base(attackData, player) {}

    public override void Update()
    {
        var inRange = SceneManager.Instance.GetInCircle(_attackData.AttackRange, _player.transform.position);
        if (inRange.Count > 0)
            SetEnable();
        else
            SetDisable();
    }
}
