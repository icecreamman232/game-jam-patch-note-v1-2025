using SGGames.Scripts.Core;
using UnityEngine;

public class JuggernautEnemyAI : EnemyAI
{
    [SerializeField] private GameEvent m_gameEvent;
    [SerializeField] private EnemyMovement m_movement;
    [SerializeField] private float m_restDuration;
    private bool m_isResting;
    private float m_restTimer;
    private Transform m_player;


    private void Awake()
    {
        m_gameEvent.AddListener(OnGameEventChanged);
    }

    private void Start()
    {
        m_player = ServiceLocator.GetService<LevelManager>().CurrentPlayer.transform;
        m_movement.OnHitCollide += OnHitCollide;
    }

    private void OnDestroy()
    {
        m_gameEvent.RemoveListener(OnGameEventChanged);
        m_movement.OnHitCollide -= OnHitCollide;
    }

    private void OnHitCollide(GameObject hit)
    {
        if (hit.layer == LayerMask.NameToLayer("Obstacle"))
        {
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
    
    private void OnGameEventChanged(GameEventType gameEventType)
    {
        if (gameEventType == GameEventType.GameStart)
        {
            m_isResting = true;
        }
    }
}
