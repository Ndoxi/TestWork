using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public float Health => _health;
    public bool IsDead => _isDead;

    [SerializeField]
    private float _health;
    [Space, SerializeField]
    private PlayerAttackController _attackController;

    private bool _isDead;
    private SceneManager _sceneManager;
    public Animator AnimatorController;

    [Inject]
    private void Construct(SceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    private void Start()
    {
        _sceneManager.SetPlayer(this);
        _sceneManager.StartGame();
    }

    public void PerformAttack(PlayerAttackController.AttackType attackType)
    {
        _attackController.PerformAttack(attackType);
    }

    public void TakeDamage(float amount)
    {
        if (_isDead)
            return;

        _health -= amount;
        Debug.Log($"Player takes {amount} damage. Current health is {_health}.");

        if (Health <= 0)
        {
            _health = 0;
            Die();
            return;
        }
    }

    public void RestoreHealth(float amount)
    {
        if (_isDead)
            return;

        _health += amount;
        Debug.Log($"Player restores {amount} health. Current health is {_health}.");
    }

    private void Die()
    {
        _isDead = true;
        AnimatorController.SetTrigger("Die");
        _sceneManager.GameOver();
    }
}
