using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemie : MonoBehaviour
{
    public enum EnemyType { Goblin, StrongGoblin }

    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;
    
    public Animator AnimatorController;
    public NavMeshAgent Agent;

    private SceneManager _sceneManager;
    private float lastAttackTime = 0;
    private bool isDead = false;

    [Inject]
    private void Construct(SceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    private void Start()
    {
        _sceneManager.AddEnemie(this);
        Agent.SetDestination(_sceneManager.Player.transform.position);
    }

    private void Update()
    {
        if(isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            Agent.isStopped = true;
            return;
        }

        var distance = Vector3.Distance(transform.position, _sceneManager.Player.transform.position);
     
        if (distance <= AttackRange)
        {
            Agent.isStopped = true;
            if (Time.time - lastAttackTime > AtackSpeed)
            {
                lastAttackTime = Time.time;
                _sceneManager.Player.TakeDamage(Damage);
                AnimatorController.SetTrigger("Attack");
            }
        }
        else
        {
            Agent.isStopped = false;
            Agent.SetDestination(_sceneManager.Player.transform.position);
        }
        AnimatorController.SetFloat("Speed", Agent.speed);
    }
    
    protected virtual void Die()
    {
        _sceneManager.RemoveEnemie(this);
        _sceneManager.Player.RestoreHealth(2);
        isDead = true;
        AnimatorController.SetTrigger("Die");
    }
}
