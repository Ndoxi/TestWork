using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public event Action<float> OnPerformed;
    public event Action OnEnable;
    public event Action OnDisable;


    protected readonly Player _player;
    protected readonly AttackData _attackData;
    protected bool _enabled = true;
    private float _lastAttackTime = -1000f;

    public Attack(AttackData attackData, Player player)
    {
        _player = player;
        _attackData = attackData;
    }

    public virtual void PerformAttack()
    {
        if (!_enabled)
        {
            Log("Attack is disabled!");
            return;
        }

        if (Time.time - _lastAttackTime < _attackData.AttackSpeed)
        {
            Log("The attack is coolingdown!");
            return;
        }

        Enemie closestEnemie = SceneManager.Instance.GetClosestEnemy(_player.transform.position);
        if (closestEnemie == null)
        {
            Log("An enemy not found!");
            return;
        }

        var distance = Vector3.Distance(_player.transform.position, closestEnemie.transform.position);
        if (distance > _attackData.AttackRange)
        {
            Log("The nearest enemy has not been found!");
            return;
        }

        _player.transform.LookAt(closestEnemie.transform);
        _player.transform.transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - _player.transform.position);

        _lastAttackTime = Time.time;
        closestEnemie.Hp -= _attackData.Damage;
        _player.AnimatorController.SetTrigger(_attackData.AnimatorTag);
        OnPerformed?.Invoke(_attackData.AttackSpeed);
    }

    public virtual void Update() { }

    protected void SetEnable()
    {
        _enabled = true;
        OnEnable?.Invoke();
    }

    protected void SetDisable()
    {
        _enabled = false;
        OnDisable?.Invoke();
    }

    protected void Log(string message)
    {
        Debug.Log($"{_attackData.AttackType}: {message}");
    }
}
