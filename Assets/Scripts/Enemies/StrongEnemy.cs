using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StrongEnemy : Enemie
{
    private EnemyFactory _enemyFactory;

    [Inject]
    private void Create(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    protected override void Die()
    {
        for (int i = 0; i < 2; i++)
        {
            _enemyFactory.Create(EnemyType.Goblin, new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        }

        base.Die();
    }
}