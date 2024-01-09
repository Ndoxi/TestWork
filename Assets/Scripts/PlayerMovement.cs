using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        if (_player == null)
            _player = GetComponent<Player>();
    }

    public float Speed;

    private Vector3 ProcessMovement()
    {
        Vector3 moveDirection = MovementProcessor.Current.GetMoveDirection();
        Vector3 currentPosition = transform.position;

        return currentPosition + Speed * Time.deltaTime * moveDirection;
    }

    private void Update()
    {
        if (_player.IsDead)
            return;

        float speed = 0;
        if (MovementProcessor.Current.MoveInput != Vector2.zero)
        {
            Vector3 newPosition = ProcessMovement();
            speed = Vector3.Distance(transform.position, newPosition) / Time.deltaTime;
            transform.LookAt(newPosition);
            transform.position = newPosition;
        }
        _player.AnimatorController.SetFloat("Speed", speed);
    }
}