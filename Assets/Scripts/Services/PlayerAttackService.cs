using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAttackService
{
    private static readonly string RESOURCE_KEY = "AttacksData";
    private static AttacksData _attacksData;
    private static Dictionary<PlayerAttackController.AttackType, Attack> _attacks;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        _attacksData = Resources.Load<AttacksData>(RESOURCE_KEY);
        _attacks = new Dictionary<PlayerAttackController.AttackType, Attack>();
    }

    public static Attack Get(PlayerAttackController.AttackType type)
    {
        return _attacks[type];
    }

    public static Attack Create(PlayerAttackController.AttackType type, Player player)
    {
        if (_attacks.TryGetValue(type, out Attack attack))
            return attack;
        else
            return CreateInternal(type, player);
    }

    private static Attack CreateInternal(PlayerAttackController.AttackType type, Player player)
    {
        Attack attack = null;

        switch (type)
        {
            case PlayerAttackController.AttackType.Basic:
                attack = new Attack(_attacksData.GetAttackData(PlayerAttackController.AttackType.Basic), player);
                break;
            case PlayerAttackController.AttackType.Super:
                attack = new SuperAttack(_attacksData.GetAttackData(PlayerAttackController.AttackType.Super), player);
                break;
        }

        _attacks.Add(type, attack);
        return attack;
    }
}
