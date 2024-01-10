using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Player))]
public class PlayerAttackController : MonoBehaviour
{
    public enum AttackType { Basic, Super }

    private Dictionary<AttackType, Attack> _attacks;
    private Player _player;
    private PlayerAttackService _attackService;

    [Inject]
    private void Construct(PlayerAttackService attackService)
    {
        _attackService = attackService;
    }

    private void Awake()
    {
        if (_player == null)
            _player = GetComponent<Player>();

        _attacks = new Dictionary<AttackType, Attack>
        {
            { AttackType.Basic, _attackService.Create(AttackType.Basic, _player) },
            { AttackType.Super, _attackService.Create(AttackType.Super, _player) }
        };
    }

    private void Update()
    {
        if (_player.IsDead)
            return;

        foreach (var item in _attacks)
        {
            item.Value.Update();
        }   
    }

    public void PerformAttack(AttackType type)
    {
        if (_player.IsDead)
            return;

        _attacks[type].PerformAttack();
    }
}
