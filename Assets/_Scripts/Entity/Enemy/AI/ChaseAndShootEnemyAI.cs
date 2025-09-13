using System.Collections;
using SGGames.Scripts.Core;
using UnityEngine;

public class ChaseAndShootEnemyAI : EnemyAI
{
    [SerializeField] private EnemyWeaponHandler m_weaponHandler;
    [SerializeField] private float m_shootFrequency;
    [SerializeField] private GameEvent m_gameEvent;
    [SerializeField] private EnemyMovement m_movement;
    [SerializeField] private float m_frequencyCheckPlayer;
    private Transform m_player;
    private float m_timeSinceLastCheckPlayer;
    private float m_timeSinceLastShoot;

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
        if (Time.time - m_timeSinceLastCheckPlayer > m_frequencyCheckPlayer)
        {
            m_timeSinceLastCheckPlayer = Time.time;
            var direction = (m_player.position - transform.position).normalized;
            m_movement.SetMoveDirection(direction);
        }

        if (Time.time - m_timeSinceLastShoot > m_shootFrequency)
        {
            m_timeSinceLastShoot = Time.time;
            var direction = (m_player.position - transform.position).normalized;
            m_weaponHandler.Shoot(direction);
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
