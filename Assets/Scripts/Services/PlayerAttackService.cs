using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAttackService
{
    private readonly AttacksData _attacksData;
    private readonly SceneManager _sceneManager;
    private readonly Dictionary<PlayerAttackController.AttackType, Attack> _attacks;

    public PlayerAttackService(AttacksData attacksData, SceneManager sceneManager)
    {
        _attacks = new Dictionary<PlayerAttackController.AttackType, Attack>();
        _sceneManager = sceneManager;
        _attacksData = attacksData;
    }

    public Attack Get(PlayerAttackController.AttackType type)
    {
        return _attacks[type];
    }

    public Attack Create(PlayerAttackController.AttackType type, Player player)
    {
        if (_attacks.TryGetValue(type, out Attack attack))
            return attack;
        else
            return CreateInternal(type, player);
    }

    private Attack CreateInternal(PlayerAttackController.AttackType type, Player player)
    {
        Attack attack = null;

        switch (type)
        {
            case PlayerAttackController.AttackType.Basic:
                attack = new Attack(_attacksData.GetAttackData(PlayerAttackController.AttackType.Basic), player, _sceneManager);
                break;
            case PlayerAttackController.AttackType.Super:
                attack = new SuperAttack(_attacksData.GetAttackData(PlayerAttackController.AttackType.Super), player, _sceneManager);
                break;
        }

        _attacks.Add(type, attack);
        return attack;
    }
}
