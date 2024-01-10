using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFactory
{
    private readonly string PLAYER_PREFAB_KEY = "Characters/Player";

    private readonly Player _playerPrefab;
    private readonly DiContainer _container;

    public PlayerFactory(DiContainer container)
    {
        _container = container;
        _playerPrefab = Resources.Load<Player>(PLAYER_PREFAB_KEY);
    }

    public Player Create(Vector3 spawnPosition)
    {
        Player newPlayer = _container.InstantiatePrefabForComponent<Player>(_playerPrefab);
        newPlayer.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        return newPlayer;
    }
}
