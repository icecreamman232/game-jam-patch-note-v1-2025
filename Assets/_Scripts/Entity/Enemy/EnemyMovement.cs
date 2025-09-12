using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private BoxCollider2D m_collider;
    [SerializeField] private Vector2 m_moveDirection;

    public Action<GameObject> OnHitCollide;
    
    public void SetMoveDirection(Vector2 moveDirection)
    {
        m_moveDirection = moveDirection;
        transform.up = moveDirection;
    }
    
    private void Update()
    {
        if (IsHitCollide(out var hit))
        {
            m_moveDirection = Vector2.zero;
            OnHitCollide?.Invoke(hit);
            return;
        }
        transform.position += transform.up * (m_speed * Time.deltaTime);
    }

    private bool IsHitCollide(out GameObject hit)
    {
        var result = Physics2D.BoxCast(transform.position, m_collider.size, 0f, m_moveDirection,0.1f, LayerMask.GetMask("Obstacle"));
        hit = result.collider?.gameObject;
        return result.collider != null;
    }
}
