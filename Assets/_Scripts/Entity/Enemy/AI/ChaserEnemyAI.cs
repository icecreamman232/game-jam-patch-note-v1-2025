using System;
using SGGames.Scripts.Core;
using UnityEngine;

public class ChaserEnemyAI : EnemyAI
{
    [SerializeField] private GameEvent m_gameEvent;
    [SerializeField] private EnemyMovement m_movement;
    [SerializeField] private float m_frequencyCheckPlayer;
    private Transform m_player;
    private float m_timeSinceLastCheck;

    private void Awake()
    {
        m_gameEvent.AddListener(OnGameEventChanged);
    }

    private void OnDestroy()
    {
        m_gameEvent.RemoveListener(OnGameEventChanged);
    }

    protected override void Start()
    {
        m_player = ServiceLocator.GetService<LevelManager>().CurrentPlayer.transform;
        var direction = (m_player.position - transform.position).normalized;
        m_movement.SetMoveDirection(direction);
        base.Start();
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
    
    private void OnGameEventChanged(GameEventType gameEventType)
    {
        if (gameEventType == GameEventType.GameStart)
        {
            m_movement.SetCanMove(true);
        }
    }
}
