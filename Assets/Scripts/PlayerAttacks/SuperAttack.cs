using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttack : Attack
{
    public SuperAttack(AttackData attackData, Player player, SceneManager sceneManager) 
        : base(attackData, player, sceneManager) { }

    public override void Update()
    {
        var inRange = _sceneManager.GetInCircle(_attackData.AttackRange, _player.transform.position);
        if (inRange.Count > 0)
            SetEnable();
        else
            SetDisable();
    }
}
