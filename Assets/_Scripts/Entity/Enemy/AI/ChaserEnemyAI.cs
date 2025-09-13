using System;
using SGGames.Scripts.Core;
using UnityEngine;

public class ChaserEnemyAI : EnemyAI
{
    [SerializeField] private EnemyMovement m_movement;
    [SerializeField] private float m_frequencyCheckPlayer;
    private Transform m_player;
    private float m_timeSinceLastCheck;
    private void Start()
    {
        m_player = ServiceLocator.GetService<LevelManager>().CurrentPlayer.transform;
        var direction = (m_player.position - transform.position).normalized;
        m_movement.SetMoveDirection(direction);
        m_movement.SetCanMove(true);
    }
    
    private void Update()
    {
        if (Time.time - m_timeSinceLastCheck > m_frequencyCheckPlayer)
        {
            m_timeSinceLastCheck = Time.time;
            var direction = (m_player.position - transform.position).normalized;
            m_movement.SetMoveDirection(direction);
        }
    }
}
