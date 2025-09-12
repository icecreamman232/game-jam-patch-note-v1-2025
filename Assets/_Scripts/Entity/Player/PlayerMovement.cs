using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private BoxCollider2D m_collider;
    [SerializeField] private Vector2 m_moveDirection;

    private void Start()
    {
        ServiceLocator.GetService<InputManager>().OnMoveInputCallback += OnMoveInputCallback;
    }

    private void OnDestroy()
    {
        ServiceLocator.GetService<InputManager>().OnMoveInputCallback -= OnMoveInputCallback;
    }

    public void ResetMovement()
    {
        m_moveDirection = Vector2.zero;
    }

    private void OnMoveInputCallback(Vector2 input)
    {
        m_moveDirection = input;
    }

    private void Update()
    {
        if (IsHitCollide())
        {
            m_moveDirection = Vector2.zero;
            return;
        }
        transform.Translate(m_moveDirection * (m_speed * Time.deltaTime));
    }

    private bool IsHitCollide()
    {
        var result = Physics2D.BoxCast(transform.position, m_collider.size, 0f, m_moveDirection,0.1f, LayerMask.GetMask("Obstacle"));
        return result.collider != null;
    }
}
