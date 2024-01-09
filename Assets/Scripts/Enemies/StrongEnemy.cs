using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemie
{
    protected override void Die()
    {
        Factory.SpawnEnemy(EnemyType.Goblin, new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        Factory.SpawnEnemy(EnemyType.Goblin, new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        base.Die();
    }
}