using System;
using SGGames.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class JuggernautEnemyAI : EnemyAI
{
    internal enum MoveDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }
    
    [SerializeField] private MoveDirection m_moveDirection;
    [SerializeField] private EnemyMovement m_movement;
    [SerializeField] private float m_restDuration;
    private bool m_isResting;
    private float m_restTimer;
    private Transform m_player;

    private void Start()
    {
        m_player = ServiceLocator.GetService<LevelManager>().CurrentPlayer.transform;
        m_movement.OnHitCollide += OnHitCollide;
        m_isResting = true;
    }

    private void OnDestroy()
    {
        m_movement.OnHitCollide -= OnHitCollide;
    }

    private void OnHitCollide(GameObject hit)
    {
        if (hit.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log($"Hit {hit.gameObject.name}");
            m_restTimer = 0;
            m_isResting = true;
            m_movement.SetCanMove(false);
            m_movement.SetMoveDirection(Vector2.zero);
        }
    }
    private void Update()
    {
        if (m_isResting)
        {
            m_restTimer += Time.deltaTime;
            if (m_restTimer >= m_restDuration)
            {
                m_isResting = false;
                StartCharge();
            }
        }
    }

    private void StartCharge()
    {
        m_movement.SetMoveDirection((m_player.position - transform.position).normalized);
        m_movement.SetCanMove(true);
    }
}
