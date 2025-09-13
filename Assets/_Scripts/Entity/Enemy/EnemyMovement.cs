using System;
using UnityEngine;

public enum EnemyMovementType
{
    Normal,
    Knockback,
}

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyMovementType m_movementType;
    [SerializeField] private float m_speed;
    [SerializeField] private BoxCollider2D m_collider;
    [SerializeField] private Vector2 m_moveDirection;
    [SerializeField] private float m_hitCollideDistance = 0.1f;

    private Vector2 m_knockBackDirection;
    private float m_knockBackSpeed;
    private Action m_movementUpdate;
    private bool m_canMove;
    public Action<GameObject> OnHitCollide;

    private void Start()
    {
        SetMovementType(EnemyMovementType.Normal);
    }
    
    public void ApplyKnockback(Vector2 knockBackDirection, float knockBackSpeed)
    {
        m_knockBackDirection = knockBackDirection;
        m_knockBackSpeed = knockBackSpeed;
        SetMovementType(EnemyMovementType.Knockback);
    }
    
    public void SetCanMove(bool canMove)
    {
        m_canMove = canMove;
    }
    
    public void SetMoveDirection(Vector2 moveDirection)
    {
        m_moveDirection = moveDirection;
        transform.up = moveDirection;
    }
    
    private void Update()
    {
        if (!m_canMove) return;
        
        m_movementUpdate?.Invoke();
    }

    private void UpdateNormalMovement()
    {
        if (IsHitCollide(out var hit))
        {
            m_moveDirection = Vector2.zero;
            OnHitCollide?.Invoke(hit);
            return;
        }
        transform.position += transform.up * (m_speed * Time.deltaTime);
    }

    private void UpdateKnockbackMovement()
    {
        if (IsHitCollide(out var hit))
        {
            m_moveDirection = Vector2.zero;
            OnHitCollide?.Invoke(hit);
            return;
        }
        transform.position += (Vector3)(m_knockBackDirection * (m_knockBackSpeed * Time.deltaTime));
        m_knockBackSpeed--;
        if (m_knockBackSpeed <= 0)
        {
            m_knockBackSpeed = 0;
            SetMovementType(EnemyMovementType.Normal);
        }
    }
    
    private void SetMovementType(EnemyMovementType movementType)
    {
        switch (movementType)
        {
            case EnemyMovementType.Normal:
                m_movementType = EnemyMovementType.Normal;
                m_movementUpdate = UpdateNormalMovement;
                break;
            case EnemyMovementType.Knockback:
                m_movementType = EnemyMovementType.Knockback;
                m_movementUpdate = UpdateKnockbackMovement;
                break;
        }
    }

    private bool IsHitCollide(out GameObject hit)
    {
        var result = Physics2D.BoxCast(transform.position, m_collider.size, 0f, m_moveDirection, m_hitCollideDistance, LayerMask.GetMask("Obstacle"));
        hit = result.collider?.gameObject;
        return result.collider != null;
    }
}
